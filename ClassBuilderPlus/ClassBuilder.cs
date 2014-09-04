using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBuilderPlus
{
    public enum TipoCampo { s, i };

    public enum Metodo { Id, OneToOne, ManyToOne, Property, Foreign}

    public class Classe
    {
        private List<Campo> lista = new List<Campo>();

        public string Ns { get; set; }
        public string NsDAO { get; set; }
        public string Asm { get; set; }
        public string ClassName {get; set;}
        public string TableName {get; set;}
        public string PathToSave { get; set; }
        public List<Campo> Lista { get{ return lista; } }
    }
    
    public class Campo
    {
        public string Name {get; set;}
        public string FieldName { get; set; }
        public string ConstrainedName { get; set; }
        public string DisplayName { get; set; }
        public Metodo Metodo { get; set; }
        public bool Required { get; set; }
        public string Fetch { get; set; }
        public bool PossuiLength { get; set; }
        public string Tipo { get; set; }
        public string DataType { get; set; }
        public string Classe { get; set; }
        public int Tamanho { get; set; }
        public int Minimo {get;set;}
    }

    public class ClassBuilder
    {
        public string buildClasse(Classe cls){
            string _c = "";

            string filename = String.Format("{0}\\{1}.cs", cls.PathToSave,cls.ClassName);

            using (StreamWriter writer = new StreamWriter(@filename))
            {
                writer.WriteLine("using System;");
                writer.WriteLine("using System.Collections.Generic;");
                writer.WriteLine("using System.ComponentModel.DataAnnotations;");
                writer.WriteLine("using System.Linq;");
                writer.WriteLine("using System.Text;");
                writer.WriteLine("using System.Threading.Tasks;");
                writer.WriteLine("");

                writer.WriteLine(string.Format("namespace {0}", cls.Ns));
                writer.WriteLine("{");
                writer.WriteLine(string.Format("    public class {0}", cls.ClassName));
                writer.WriteLine("    {");

                foreach (Campo campo in cls.Lista)
                {
                    if (!String.IsNullOrEmpty(campo.DisplayName))
                    {
                        writer.WriteLine(string.Format("        [Display(Name = \"{0}\")]", campo.DisplayName));
                    }

                    if (campo.Required && !String.IsNullOrEmpty(campo.DisplayName))
                    {
                        writer.WriteLine(string.Format("        [Required(ErrorMessage = \"O campo {0} deve ser preenchido.\")]", campo.DisplayName));
                    }

                    if ((campo.Tamanho > 0) && (campo.Minimo > 0))
                    {
                        writer.WriteLine(string.Format("        [StringLength({0}, MinimumLength = {1})]", campo.Tamanho, campo.Minimo));
                    }

                    if (campo.DataType.ToUpper() == "DATETIME")
                    {
                        writer.WriteLine("        [DataType(DataType.Date)]");
                        writer.WriteLine("        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = \"{0:dd/MM/yyyy}\")]");
                    } else
                    if (campo.Tipo.ToUpper() == "MultilineText")
                    {
                        writer.WriteLine(String.Format("        [DataType(DataType.MultilineText), MaxLength({0})]",campo.Tamanho));
                    }
                    if (campo.Metodo == Metodo.OneToOne)
                    {
                        writer.WriteLine(String.Format("        public virtual {0} {1} {{ get; set; }} ", campo.Classe, campo.Name));
                    }
                    else if (campo.Metodo == Metodo.Foreign)
                    {
                        writer.WriteLine(String.Format("        public virtual int {0} {{ get; set; }} ", campo.Name));
                        writer.WriteLine("");
                        writer.WriteLine(String.Format("        public virtual {0} {1} {{ get; set; }} ", campo.Classe, campo.ConstrainedName));
                    }
                    else {
                        writer.WriteLine(String.Format("        public virtual {0} {1} {{ get; set; }} ", campo.Tipo, campo.Name));
                    }
                    writer.WriteLine("");
                }
                writer.WriteLine("    }");
                writer.WriteLine("}");
            }
            return _c;
        }
   
        public string buildMapeamento(Classe cls){

            string filename = String.Format("{0}\\{1}.hbm.xml", cls.PathToSave,cls.ClassName);

            using (StreamWriter writer = new StreamWriter(@filename))
            {

                writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                writer.WriteLine("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\"");
                writer.WriteLine(String.Format("                   assembly=\"{0}\"", cls.Asm));
                writer.WriteLine(String.Format("                   namespace=\"{0}\">", cls.Ns));
                writer.WriteLine(String.Format("  <class name=\"{0}\" table=\"{1}\">", cls.ClassName, cls.TableName));

                string nulo = "";
                string fetch = "";
                foreach (Campo c in cls.Lista)
                {
                    nulo = (c.Required) ? " not-null=\"true\"" : "";
                    
                    if (!String.IsNullOrEmpty(c.Fetch)) {
                        fetch = String.Format(" fetch=\"{0}\" ",c.Fetch);
                    }

                    if (c.Metodo == Metodo.Id)
                    {
                        writer.WriteLine(String.Format("    <id name=\"{0}\" column=\"{1}\">", c.Name, c.FieldName));
                        writer.WriteLine("      <generator class=\"native\"/>");
                        writer.WriteLine("    </id>");
                    }
                    else if (c.Metodo == Metodo.OneToOne)
                    {
                        writer.WriteLine(String.Format("    <one-to-one name=\"{0}\" class=\"{1}\" />", c.Name, c.Classe));
                    }
                    else if (c.Metodo == Metodo.ManyToOne)
                    {
                        writer.WriteLine(String.Format("    <many-to-one name=\"{0}\" class=\"{1}\" {2} column=\"{3}\" {4} />", c.Name, c.Classe, fetch, c.FieldName, nulo));
                    }
                    else if (c.Metodo == Metodo.Property)
                    {
                        string tam = "";
                        if (c.PossuiLength)
                        {
                            tam = String.Format("length=\"{0}\"", c.Tamanho);
                        }
                        writer.WriteLine(String.Format("    <property name=\"{0}\" column=\"{1}\" {2}{3}/>", c.Name, c.FieldName, tam, nulo));
                    }
                    else if (c.Metodo == Metodo.Foreign) { 
                        writer.WriteLine(String.Format("    <id name=\"{0}\" column=\"{1}\">",c.Name, c.FieldName));
                        writer.WriteLine(              "      <generator class=\"foreign\">");
                        writer.WriteLine(              "        <param name=\"property\">");
                        writer.WriteLine(String.Format("          {0}",c.ConstrainedName));
                        writer.WriteLine(              "        </param>");
                        writer.WriteLine(              "      </generator>");
                        writer.WriteLine(              "    </id>");
                        writer.WriteLine(String.Format("    <one-to-one name=\"{0}\" class=\"{1}\" constrained=\"true\" cascade=\"all\"/>",c.ConstrainedName, c.Classe));
                    }
                }
                writer.WriteLine("  </class>");
                writer.WriteLine("</hibernate-mapping>");
            }
            string _c = "";
            return _c;
        }

        
        public string buildDAO(Classe cls)
        {
            string _c = "";

            string filename = String.Format("{0}\\{1}DAO.cs", cls.PathToSave, cls.ClassName);

            using (StreamWriter writer = new StreamWriter(@filename))
            {
                writer.WriteLine("using System;");

                writer.WriteLine("using Dardani.DAO.NH;");
                writer.WriteLine("using Dardani.EDU.Entities.Model;");
                writer.WriteLine("using Dardani.GER.Entities.Model;");
                writer.WriteLine("using NHibernate;");
                writer.WriteLine("using System.Collections.Generic;");
                writer.WriteLine("using System.Linq;");
                writer.WriteLine("using System.Text;");
                writer.WriteLine("using System.Threading.Tasks;");
                writer.WriteLine("");
                writer.WriteLine(string.Format("namespace {0}", cls.NsDAO)); // 
                writer.WriteLine("{");
                writer.WriteLine(string.Format("    public class {0}DAO : GenericDAO<{0}>", cls.ClassName));
                writer.WriteLine("    {");
                writer.WriteLine("");
                writer.WriteLine(string.Format("        public IEnumerable<{0}> GetListagem(string searchString = null)", cls.ClassName));
                writer.WriteLine("        {");
                writer.WriteLine(string.Format("            IQueryOver<{0}> q = Session.QueryOver<{0}>();",cls.ClassName));
                writer.WriteLine(string.Format("            IEnumerable<{0}> lista;",cls.ClassName));
                writer.WriteLine("");
                writer.WriteLine("            if (!String.IsNullOrEmpty(searchString))");
                writer.WriteLine("            {");
                writer.WriteLine(string.Format("                lista = q.List<{0}>()",cls.ClassName));
                writer.WriteLine("                    .Where(s => s.Descricao.ToLower()");
                writer.WriteLine("                    .Contains(searchString.ToLower())).ToList();");
                writer.WriteLine("            }");
                writer.WriteLine("            else");
                writer.WriteLine("            {");
                writer.WriteLine(string.Format("                lista = q.List<{0}>().ToList();",cls.ClassName));
                writer.WriteLine("            }");
                writer.WriteLine("            return lista;");
                writer.WriteLine("        }");
                writer.WriteLine("");
                writer.WriteLine("        public IEnumerable<ItemVO> BuidListaItemVO()");
                writer.WriteLine("        {");
                writer.WriteLine(string.Format("            IEnumerable<{0}> lista = Session.QueryOver<{0}>()",cls.ClassName));
                writer.WriteLine("                .OrderBy(x => x.Descricao).Asc.List();");
                writer.WriteLine("");
                writer.WriteLine("            List<ItemVO> retorno = new List<ItemVO>();");
                writer.WriteLine("            foreach (var x in lista)");
                writer.WriteLine("            {");
                writer.WriteLine("                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });");
                writer.WriteLine("            }");
                writer.WriteLine("            return retorno;");
                writer.WriteLine("        }");
                writer.WriteLine("    } // END CLASS");
                writer.WriteLine("} // END NAMESPACE");
            }
            return _c;
        }


    }
}

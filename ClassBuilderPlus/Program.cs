using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBuilderPlus
{
    class Program
    {
        static void Main(string[] args)
        {
            //lerExcel("AbastecimentoAgua", "EDU_ABASTECIMENTO_AGUA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("AbastecimentoEnergia", "EDU_ABASTECIMENTO_ENERGIA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("AbastecimentoEsgoto", "EDU_ABASTECIMENTO_ESGOTO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("AEE", "EDU_AEE", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("AtividadeComplementar", "EDU_ATIVIDADE_COMPLEMENTAR", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("CategoriaPrivada", "EDU_CATEGORIA_PRIVADA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("ConvenioPublico", "EDU_CONVENIO_PUBLICO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("Deficiencia", "EDU_DEFICIENCIA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("DependenciaEscola", "EDU_DEPENDENCIA_ESCOLA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("DestinoLixo", "EDU_DESTINO_LIXO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("Equipamento", "EDU_EQUIPAMENTO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("EscolarizacaoEspecial", "EDU_ESCOLARIZACAO_ESPECIAL", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("EstagioRegulamentacao", "EDU_ESTAGIO_REGULAMENTACAO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("FormaIngressoFederal", "EDU_FORMA_INGRESSO_FEDERAL", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("FormaOcupacao", "EDU_FORMA_OCUPACAO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("FuncaoDocente", "EDU_FUNCAO_DOCENTE", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("InfraestruturaCategoria", "EDU_INFRAESTRUTURA_CATEGORIA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("InfraestruturaItem", "EDU_INFRAESTRUTURA_ITEM", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("LinguaIndigena", "EDU_LINGUA_INDIGENA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("LocalEscola", "EDU_LOCAL_ESCOLA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("LocalizacaoDiferenciada", "EDU_LOCALIZACAO_DIFERENCIADA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("MantenedorPrivado", "EDU_MANTENEDOR_PRIVADO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("MaterialEspecifico", "EDU_MATERIAL_ESPECIFICO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("ModeloCertidao", "EDU_MODELO_CERTIDAO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");


            //lerExcel("Modalidade", "EDU_MODALIDADE", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("Serie", "EDU_SERIE", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TipoEnsino", "EDU_TIPO_ENSINO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("Etapa", "EDU_ETAPA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");

            //lerExcel("TipoAtendimento", "EDU_TIPO_ATENDIMENTO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TipoAEE", "EDU_TIPO_AEE", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TipoComplementar", "EDU_TIPO_COMPLEMENTAR", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            
            //lerExcel("Turma", "EDU_TURMA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TurmaAEE", "EDU_TURMA_AEE", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TurmaComplementar", "EDU_TURMA_COMPLEMENTAR", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");



            //lerExcel("Pais", "EDU_PAIS", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("Pessoa", "EDU_PESSOA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("PessoaDocumentacao", "EDU_PESSOA_DOCUMENTACAO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("PessoaEndereco", "EDU_PESSOA_ENDERECO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            
            //lerExcel("PessoaEspecialCategoria", "EDU_PESSOA_ESPECIAL_CATEGORIA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("PessoaEspecialItem", "EDU_PESSOA_ESPECIAL_ITEM", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("PosGraduacao", "EDU_POS_GRADUACAO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("RecursoINEP", "EDU_RECURSO_INEP", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("SituacaoCursoSuperior", "EDU_SITUACAO_CURSO_SUPERIOR", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("SituacaoDocumento", "EDU_SITUACAO_DOCUMENTO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TipoAdministracao", "EDU_TIPO_ADMINISTRACAO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TipoCertidao", "EDU_TIPO_CERTIDAO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TipoAgua", "EDU_TIPO_AGUA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TipoInstituicao", "EDU_TIPO_INSTITUICAO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TipoNacionalidade", "EDU_TIPO_NACIONALIDADE", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("TransportePublico", "EDU_TRANSPORTE_PUBLICO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("VeiculoCategoria", "EDU_VEICULO_CATEGORIA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("VeiculoTipo", "EDU_VEICULO_TIPO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("VinculoEmpregaticio", "EDU_VINCULO_EMPREGATICIO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("Zona", "EDU_ZONA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("", "EDU_", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("", "EDU_", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            
            
            //lerExcel("AlunoEndereco", "EDU_ALUNO_ENDERECO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("AlunoDocumentacao", "EDU_ALUNO_DOCUMENTACAO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("AlunoInformacoes", "EDU_ALUNO_INFORMACOES", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");

            //lerExcel("SituacaoFuncionamento", "EDU_SITUACAO_FUNCIONAMENTO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("Escola", "EDU_ESCOLA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("EscolaCompartilhamento", "EDU_ESCOLA_COMPARTILHAMENTO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("EscolaComputador", "EDU_ESCOLA_COMPUTADOR", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("EscolaEndereco", "EDU_ESCOLA_ENDERECO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("EscolaEquipamento", "EDU_ESCOLA_EQUIPAMENTO", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("EscolaInfraestruturaItem", "EDU_ESCOLA_INFRAESTRUTURA_ITEM", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("EscolaLocal", "EDU_ESCOLA_LOCAL", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("EscolaPrivada", "EDU_ESCOLA_PRIVADA", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");
            //lerExcel("EscolaPrivadaMantenedor", "EDU_ESCOLA_PRIVADA_MANTENEDOR", "Dardani.EDU.Entities.Model", "Dardani.EDU.Entities");

            //Console.ReadLine();
        }

        enum F { Observacoes, Metodo, Name, ConstrainedName, DisplayName, FieldName, Tipo, DataType, Required, PossuiLength, Tamanho, Minimo, Fetch, Classe };

        private static void lerExcel(string className, string tableName, string nS, string asm)
        {
            Classe c = new Classe()
            {
                PathToSave = "e:\\projetos_dardani\\repositorio_classes\\output",
                ClassName = className,
                TableName = tableName,
                Ns = nS,
                NsDAO = "Dardani.EDU.BO.NH",
                Asm = asm,
            };

            string con = String.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=e:\\projetos_dardani\\repositorio_classes\\{0}.xls;Extended Properties='Excel 8.0;HDR=Yes;'",className);
            using(OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection); 
                using(OleDbDataReader dr = command.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        string metodoString = dr[(int)F.Metodo].ToString();
                        Metodo metodo = traduzirMetodo(metodoString);
                        string name = dr[(int)F.Name].ToString();
                        string constrainedName = dr[(int)F.ConstrainedName].ToString();
                        string displayName = dr[(int)F.DisplayName].ToString();
                        string fieldName = dr[(int)F.FieldName].ToString();
                        string tipo = dr[(int)F.Tipo].ToString();
                        string dataType = dr[(int)F.DataType].ToString();

                        string requerido = dr[(int)F.Required].ToString().ToUpper();
                        bool required = (dr[(int)F.Required].ToString().ToUpper() == "TRUE") ? true : false;
                        bool possuiLength = (dr[(int)F.PossuiLength].ToString().ToUpper() == "TRUE") ? true : false;
                        int tamanho = String.IsNullOrEmpty(dr[(int)F.Tamanho].ToString()) ? 0 : Int16.Parse(dr[(int)F.Tamanho].ToString());
                        int minimo = String.IsNullOrEmpty(dr[(int)F.Minimo].ToString()) ? 0 : Int16.Parse(dr[(int)F.Minimo].ToString());
                        string fetch = dr[(int)F.Fetch].ToString();
                        string classe = dr[(int)F.Classe].ToString();

                        c.Lista.Add(new Campo() { 
                            Metodo = metodo,
                            Name = name, 
                            FieldName = fieldName, 
                            ConstrainedName = constrainedName,
                            Required = required, 
                            DisplayName = displayName,
                            Tipo = tipo,
                            DataType = dataType,
                            Tamanho = tamanho, 
                            Minimo = minimo, 
                            PossuiLength = possuiLength,
                            Classe = classe,
                            Fetch = fetch,
                        });
                        //Console.WriteLine("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\" ", name, displayName, fieldName, tipo, required, isId, possuiLength, tamanho, minimo);
                        //Console.WriteLine("\"{0}\" isId = \"{1}\" ", name, isId);
                    }
                }
            }

            ClassBuilder cb = new ClassBuilder();
            cb.buildMapeamento(c);
            cb.buildClasse(c);
            cb.buildDAO(c);
        }

        private static Metodo traduzirMetodo(string texto){
            Metodo retorno = Metodo.Property;
            switch (texto.ToUpper()) {
                case "ID" : retorno = Metodo.Id; break;
                case "P" : retorno = Metodo.Property; break;
                case "OTO" : retorno = Metodo.OneToOne; break;
                case "MTO": retorno = Metodo.ManyToOne; break;
                case "FOREIGN": retorno = Metodo.Foreign; break;
                default: retorno = Metodo.Property; break;
	        }
            return retorno;
        }
    }
}

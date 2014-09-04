using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class InfraestruturaCategoriaDAO : GenericDAO<InfraestruturaCategoria>
    {

        public IEnumerable<InfraestruturaCategoria> GetListagemFull()
        {
            IQueryOver<InfraestruturaCategoria> q = Session.QueryOver<InfraestruturaCategoria>();
            IEnumerable<InfraestruturaCategoria> lista;
            lista = q.List<InfraestruturaCategoria>().ToList();

            foreach (InfraestruturaCategoria ctg in lista) {
                IEnumerable<InfraestruturaItem> li = 
                    Session.QueryOver<InfraestruturaItem>()
                    .Where(x => x.InfraestruturaCategoria.Id == ctg.Id).List();
                ctg.Itens.Clear();
                foreach (InfraestruturaItem item in li)
                {
                    ctg.Itens.Add(item);
                }
            }
            return lista;
        }

        public IEnumerable<InfraestruturaCategoria> GetListagem(string searchString = null)
        {
            IQueryOver<InfraestruturaCategoria> q = Session.QueryOver<InfraestruturaCategoria>();
            IEnumerable<InfraestruturaCategoria> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<InfraestruturaCategoria>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<InfraestruturaCategoria>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

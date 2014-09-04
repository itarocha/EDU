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
    public class EstadoDAO : GenericDAO<Estado>
    {
        public Estado GetByValorEducacenso(int codigo)
        {
            Estado value = Session.QueryOver<Estado>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }


        /*
        public IEnumerable<Etapa> GetListagem(string searchString = null)
        {
            IQueryOver<Etapa> q = Session.QueryOver<Etapa>();
            IEnumerable<Etapa> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Etapa>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Etapa>().ToList();
            }
            return lista;
        }
        */
        /* DEVE MOSTRAR TIPO DE ENSINO E SÉRIE
        public IEnumerable<ItemVO> BuidListaItemVO()
        {
            IEnumerable<Etapa> lista = Session.QueryOver<Etapa>()
                .OrderBy(x => x.Descricao).Asc.List();

            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }
        */
    } // END CLASS
} // END NAMESPACE

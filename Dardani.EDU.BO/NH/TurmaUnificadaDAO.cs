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
    public class TurmaUnificadaDAO : GenericDAO<TurmaUnificada>
    {

        public TurmaUnificada GetByValorEducacenso(int codigo)
        {
            TurmaUnificada value = Session.QueryOver<TurmaUnificada>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<TurmaUnificada> GetListagem(string searchString = null)
        {
            IQueryOver<TurmaUnificada> q = Session.QueryOver<TurmaUnificada>();
            IEnumerable<TurmaUnificada> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TurmaUnificada>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<TurmaUnificada>().ToList();
            }
            return lista;
        }

        public IEnumerable<ItemVO> BuidListaItemVO()
        {
            IEnumerable<TurmaUnificada> lista = Session.QueryOver<TurmaUnificada>()
                .OrderBy(x => x.Descricao).Asc.List();

            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }
    } // END CLASS
} // END NAMESPACE

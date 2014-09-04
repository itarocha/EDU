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
    public class TipoAtendimentoDAO : GenericDAO<TipoAtendimento>
    {

        public TipoAtendimento GetByValorEducacenso(int codigo)
        {
            TipoAtendimento sf = Session.QueryOver<TipoAtendimento>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return sf;
        }

        public IEnumerable<TipoAtendimento> GetListagem(string searchString = null)
        {
            IQueryOver<TipoAtendimento> q = Session.QueryOver<TipoAtendimento>();
            IEnumerable<TipoAtendimento> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TipoAtendimento>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<TipoAtendimento>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

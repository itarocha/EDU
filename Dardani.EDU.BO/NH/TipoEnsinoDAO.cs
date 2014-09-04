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
    public class TipoEnsinoDAO : GenericDAO<TipoEnsino>
    {

        public IEnumerable<TipoEnsino> GetListagem(string searchString = null)
        {
            IQueryOver<TipoEnsino> q = Session.QueryOver<TipoEnsino>();
            IEnumerable<TipoEnsino> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TipoEnsino>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<TipoEnsino>().ToList();
            }
            return lista;
        }
    } // END CLASS
} // END NAMESPACE

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
    public class TipoAEEDAO : GenericDAO<TipoAEE>
    {

        public IEnumerable<TipoAEE> GetListagem(string searchString = null)
        {
            IQueryOver<TipoAEE> q = Session.QueryOver<TipoAEE>();
            IEnumerable<TipoAEE> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TipoAEE>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<TipoAEE>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

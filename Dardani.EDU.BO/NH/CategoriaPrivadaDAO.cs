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
    public class CategoriaPrivadaDAO : GenericDAO<CategoriaPrivada>
    {

        public IEnumerable<CategoriaPrivada> GetListagem(string searchString = null)
        {
            IQueryOver<CategoriaPrivada> q = Session.QueryOver<CategoriaPrivada>();
            IEnumerable<CategoriaPrivada> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<CategoriaPrivada>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<CategoriaPrivada>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

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
    public class LocalEscolaDAO : GenericDAO<LocalEscola>
    {

        public IEnumerable<LocalEscola> GetListagem(string searchString = null)
        {
            IQueryOver<LocalEscola> q = Session.QueryOver<LocalEscola>();
            IEnumerable<LocalEscola> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<LocalEscola>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<LocalEscola>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

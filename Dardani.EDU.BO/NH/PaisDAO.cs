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
    public class PaisDAO : GenericDAO<Pais>
    {

        public Pais GetByValorEducacenso(int codigo)
        {
            Pais value = Session.QueryOver<Pais>()
                .Where(x => x.Codigo == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<Pais> GetListagem(string searchString = null)
        {
            IQueryOver<Pais> q = Session.QueryOver<Pais>();
            IEnumerable<Pais> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Pais>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Pais>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

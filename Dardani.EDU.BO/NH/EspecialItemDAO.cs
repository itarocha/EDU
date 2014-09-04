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
    public class EspecialItemDAO : GenericDAO<EspecialItem>
    {

        public EspecialItem GetByValorEducacenso(int codigo)
        {
            EspecialItem value = Session.QueryOver<EspecialItem>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<EspecialItem> GetListagem(string searchString = null)
        {
            IQueryOver<EspecialItem> q = Session.QueryOver<EspecialItem>();
            IEnumerable<EspecialItem> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<EspecialItem>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<EspecialItem>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

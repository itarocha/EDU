using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
//using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class AEEDAO : GenericDAO<AEE>
    {
        public AEE GetByValorEducacenso(int codigo)
        {
            AEE value = Session.QueryOver<AEE>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<AEE> GetListagem(string searchString = null)
        {
            IQueryOver<AEE> q = Session.QueryOver<AEE>();
            IEnumerable<AEE> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<AEE>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<AEE>().ToList();
            }
            return lista;
        }
    } // END CLASS
} // END NAMESPACE

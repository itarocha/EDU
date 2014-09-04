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
    public class AnoLetivoDAO : GenericDAO<AnoLetivo>
    {
        public AnoLetivo GetByAno(int ano)
        {
            AnoLetivo value = Session.QueryOver<AnoLetivo>()
                .Where(x => x.Ano == ano)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<AnoLetivo> GetListagem()
        {
            IEnumerable<AnoLetivo> lista = Session.QueryOver<AnoLetivo>()
                .OrderBy(x => x.Ano).Desc.List();
            
            return lista;
        }
    } // END CLASS
} // END NAMESPACE

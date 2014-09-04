using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class TipoDiaDAO : GenericDAO<TipoDia>
    {
        public IEnumerable<TipoDia> GetListagem()
        {
            IEnumerable<TipoDia> lista = Session.QueryOver<TipoDia>()
                .OrderBy(x => x.Descricao).Desc.List();
            
            return lista;
        }
    } // END CLASS
} // END NAMESPACE

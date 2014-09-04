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
    public class TipoEventoDAO : GenericDAO<TipoEvento>
    {
        public IEnumerable<TipoEvento> GetListagem()
        {
            IEnumerable<TipoEvento> lista = Session.QueryOver<TipoEvento>()
                .OrderBy(x => x.Descricao).Asc.List();
            
            return lista;
        }
    } // END CLASS
} // END NAMESPACE

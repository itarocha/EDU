using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
//using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Transform;

namespace Dardani.EDU.BO.NH
{
    public class UsuarioAcaoDAO : GenericDAO<UsuarioAcao>
    {
        public IEnumerable<Usuario> GetByUsuarioId(int usuarioId)
        {
            IEnumerable<Usuario> value = Session.QueryOver<UsuarioAcao>()
                .Where(x => x.Id == usuarioId)
                .Select(x => x.Usuario)
                .List<Usuario>();
            return value;
        }

        /*
        public IEnumerable<AnoLetivo> GetListagem()
        {
            IEnumerable<AnoLetivo> lista = Session.QueryOver<AnoLetivo>()
                .OrderBy(x => x.Ano).Desc.List();
            
            return lista;
        }
         * */
    } // END CLASS
} // END NAMESPACE

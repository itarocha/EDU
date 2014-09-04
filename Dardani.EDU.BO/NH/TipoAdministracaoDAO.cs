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
    public class TipoAdministracaoDAO : GenericDAO<TipoAdministracao>
    {

        public IEnumerable<TipoAdministracao> GetListagem(string searchString = null)
        {
            IQueryOver<TipoAdministracao> q = Session.QueryOver<TipoAdministracao>();
            IEnumerable<TipoAdministracao> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TipoAdministracao>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<TipoAdministracao>().ToList();
            }
            return lista;
        }

        public TipoAdministracao GetByValorEducacenso(int codigo)
        {
            TipoAdministracao sf = Session.QueryOver<TipoAdministracao>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return sf;
        }
    } // END CLASS
} // END NAMESPACE

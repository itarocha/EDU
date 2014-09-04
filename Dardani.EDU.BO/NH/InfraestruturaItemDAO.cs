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
    public class InfraestruturaItemDAO : GenericDAO<InfraestruturaItem>
    {

        public IEnumerable<InfraestruturaItem> GetListagem(string searchString = null)
        {
            IQueryOver<InfraestruturaItem> q = Session.QueryOver<InfraestruturaItem>();
            IEnumerable<InfraestruturaItem> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<InfraestruturaItem>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<InfraestruturaItem>().ToList();
            }
            return lista;
        }

        public InfraestruturaItem GetByValorEducacenso(int codigo)
        {
            InfraestruturaItem sf = Session.QueryOver<InfraestruturaItem>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return sf;
        }

    } // END CLASS
} // END NAMESPACE

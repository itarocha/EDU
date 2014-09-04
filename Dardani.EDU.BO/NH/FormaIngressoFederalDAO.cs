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
    public class FormaIngressoFederalDAO : GenericDAO<FormaIngressoFederal>
    {

        public IEnumerable<FormaIngressoFederal> GetListagem(string searchString = null)
        {
            IQueryOver<FormaIngressoFederal> q = Session.QueryOver<FormaIngressoFederal>();
            IEnumerable<FormaIngressoFederal> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<FormaIngressoFederal>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<FormaIngressoFederal>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

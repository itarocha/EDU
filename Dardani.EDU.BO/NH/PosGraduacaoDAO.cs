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
    public class PosGraduacaoDAO : GenericDAO<PosGraduacao>
    {

        public IEnumerable<PosGraduacao> GetListagem(string searchString = null)
        {
            IQueryOver<PosGraduacao> q = Session.QueryOver<PosGraduacao>();
            IEnumerable<PosGraduacao> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<PosGraduacao>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<PosGraduacao>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

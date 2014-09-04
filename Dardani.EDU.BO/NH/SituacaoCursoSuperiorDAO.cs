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
    public class SituacaoCursoSuperiorDAO : GenericDAO<SituacaoCursoSuperior>
    {

        public IEnumerable<SituacaoCursoSuperior> GetListagem(string searchString = null)
        {
            IQueryOver<SituacaoCursoSuperior> q = Session.QueryOver<SituacaoCursoSuperior>();
            IEnumerable<SituacaoCursoSuperior> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<SituacaoCursoSuperior>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<SituacaoCursoSuperior>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

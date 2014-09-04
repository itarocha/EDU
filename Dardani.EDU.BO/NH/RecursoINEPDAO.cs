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
    public class RecursoINEPDAO : GenericDAO<RecursoINEP>
    {

        public RecursoINEP GetByValorEducacenso(int codigo)
        {
            RecursoINEP value = Session.QueryOver<RecursoINEP>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<RecursoINEP> GetListagem(string searchString = null)
        {
            IQueryOver<RecursoINEP> q = Session.QueryOver<RecursoINEP>();
            IEnumerable<RecursoINEP> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<RecursoINEP>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<RecursoINEP>().ToList();
            }
            return lista;
        }
    } // END CLASS
} // END NAMESPACE

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
    public class EstagioRegulamentacaoDAO : GenericDAO<EstagioRegulamentacao>
    {

        public EstagioRegulamentacao GetByValorEducacenso(int codigo)
        {
            EstagioRegulamentacao value = Session.QueryOver<EstagioRegulamentacao>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<EstagioRegulamentacao> GetListagem(string searchString = null)
        {
            IQueryOver<EstagioRegulamentacao> q = Session.QueryOver<EstagioRegulamentacao>();
            IEnumerable<EstagioRegulamentacao> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<EstagioRegulamentacao>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<EstagioRegulamentacao>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

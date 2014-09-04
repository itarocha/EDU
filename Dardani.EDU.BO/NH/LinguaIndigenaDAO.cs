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
    public class LinguaIndigenaDAO : GenericDAO<LinguaIndigena>
    {

        public LinguaIndigena GetByValorEducacenso(int codigo)
        {
            LinguaIndigena retorno = Session.QueryOver<LinguaIndigena>()
                .Where(x => x.Codigo == codigo)
                .List().FirstOrDefault();
            return retorno;
        }


        public IEnumerable<LinguaIndigena> GetListagem(string searchString = null)
        {
            IQueryOver<LinguaIndigena> q = Session.QueryOver<LinguaIndigena>();
            IEnumerable<LinguaIndigena> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<LinguaIndigena>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<LinguaIndigena>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

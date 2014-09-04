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
    public class FormaOcupacaoDAO : GenericDAO<FormaOcupacao>
    {

        public FormaOcupacao GetByValorEducacenso(int codigo)
        {
            FormaOcupacao value = Session.QueryOver<FormaOcupacao>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }


        public IEnumerable<FormaOcupacao> GetListagem(string searchString = null)
        {
            IQueryOver<FormaOcupacao> q = Session.QueryOver<FormaOcupacao>();
            IEnumerable<FormaOcupacao> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<FormaOcupacao>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<FormaOcupacao>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

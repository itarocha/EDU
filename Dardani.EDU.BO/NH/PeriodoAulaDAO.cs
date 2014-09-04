using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Petra.Util.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class PeriodoAulaDAO : GenericDAO<PeriodoAula>
    {
        public IEnumerable<PeriodoAula> GetListagem(string searchString = null)
        {
            IQueryOver<PeriodoAula> q = Session.QueryOver<PeriodoAula>();
            IEnumerable<PeriodoAula> lista;

            /*
            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<PeriodoAula>().Where(s => s.Descricao.ToLower().Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<PeriodoAula>().ToList();
            }
             */
            lista = q.List<PeriodoAula>().OrderBy(x => x.HoraInicio).OrderBy(x => x.HoraTermino).ToList(); 

            return lista;
        }

        public IEnumerable<ItemVO> BuidListaItemVO()
        {
            /*
            IEnumerable<PeriodoAula> lista = Session.QueryOver<PeriodoAula>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
            */

            return null;
        }


    }
}

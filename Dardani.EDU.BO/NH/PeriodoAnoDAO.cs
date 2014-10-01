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
    public class PeriodoAnoDAO : GenericDAO<PeriodoAno>
    {
        public IEnumerable<PeriodoAno> GetListagem(string searchString = null)
        {
            IQueryOver<PeriodoAno> q = Session.QueryOver<PeriodoAno>();
            IEnumerable<PeriodoAno> lista;

            lista = q.List<PeriodoAno>().OrderBy(x => x.Descricao).ToList(); 

            return lista;
        }

        /*
        public bool PossuiHorarioPeriodo(int id)
        {
            Int64 qtd = Session.CreateQuery("SELECT count(distinct tb.Id) as qtd " +
                                            "FROM HorarioPeriodo as tb " +
                                            "WHERE tb.PeriodoAula.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return (qtd > 0);
        }
        */

        public bool PodeExcluir(int id, out string mensagemRetorno)
        {
            mensagemRetorno = "";
            /*
            if (this.PossuiHorarioPeriodo(id))
            {
                mensagemRetorno = "Período está sendo utilizado em Horários";
                return false;
            }
             */ 
            return true;
        }

    }
}

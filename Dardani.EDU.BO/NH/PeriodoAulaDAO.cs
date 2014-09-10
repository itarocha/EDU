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

        public bool PossuiHorarioPeriodo(int id)
        {
            Int64 qtd = Session.CreateQuery("SELECT count(distinct tb.Id) as qtd " +
                                            "FROM HorarioPeriodo as tb " +
                                            "WHERE tb.PeriodoAula.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return (qtd > 0);
        }
        /*
        public bool PossuiTurnoPeriodo(int id)
        {
            Int64 qtd = Session.CreateQuery("SELECT count(distinct tb.Id) as qtd " +
                                            "FROM TurnoPeriodo as tb " +
                                            "WHERE tb.PeriodoAula.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return (qtd > 0);
        }
        */
        public bool PodeExcluir(int id, out string mensagemRetorno)
        {
            mensagemRetorno = "";
            if (this.PossuiHorarioPeriodo(id))
            {
                mensagemRetorno = "Período está sendo utilizado em Horários";
                return false;
            }
            /*
            if (this.PossuiTurnoPeriodo(id))
            {
                mensagemRetorno = "Período está sendo utilizado em Turnos";
                return false;
            }
             */ 
            return true;
        }

    }
}

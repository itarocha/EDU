using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.Entities.VO;
using Petra.Util.Model;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class HorarioPeriodoDAO : GenericDAO<HorarioPeriodo>
    {
        public IEnumerable<HorarioPeriodoVO> GetByHorarioId(int horarioId)
        {
            IEnumerable<HorarioPeriodoVO> model =
                Session.CreateQuery(
                "SELECT " +
                "tb.Id as Id, " +
                "pa.Id as PeriodoAulaId, " +
                "pa.HoraInicio as HoraInicio, " +
                "pa.HoraTermino as HoraTermino, " +
                "hor.Id as HorarioId, " +
                "hor.Descricao as HorarioDescricao " +
                "FROM HorarioPeriodo tb " +
                "INNER JOIN tb.Horario hor " +
                "INNER JOIN tb.PeriodoAula pa " +
                "WHERE tb.Horario.Id = :horarioId " +
                "ORDER BY pa.HoraInicio, pa.HoraTermino "
                )
                .SetParameter("horarioId", horarioId)
                .SetResultTransformer(Transformers.AliasToBean(typeof(HorarioPeriodoVO)))
                .List<HorarioPeriodoVO>();

            return model;
        }

        public HorarioPeriodoVO GetVOById(int id)
        {
        	HorarioPeriodoVO model =
        		Session.CreateQuery(
                "SELECT " +
                "tb.Id as Id, " +
                "pa.Id as PeriodoAulaId, " +
                "pa.HoraInicio as HoraInicio, " +
                "pa.HoraTermino as HoraTermino, " +
                "hor.Id as HorarioId, " +
                "hor.Descricao as HorarioDescricao " +
                "FROM HorarioPeriodo tb " +
                "INNER JOIN tb.Horario hor " +
                "INNER JOIN tb.PeriodoAula pa " +
        		"WHERE tb.Id = :id")
        		.SetParameter("id",id)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(HorarioPeriodoVO)))
        		.UniqueResult<HorarioPeriodoVO>();
        	return model;
        }
    }
}

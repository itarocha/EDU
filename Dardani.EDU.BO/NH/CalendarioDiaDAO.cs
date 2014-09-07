using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.Entities.VO;
using NHibernate;
using NHibernate.Transform;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class CalendarioDiaDAO : GenericDAO<CalendarioDia>
    {
        public IEnumerable<CalendarioDia> GetListagem()
        {
            IEnumerable<CalendarioDia> lista = Session.QueryOver<CalendarioDia>()
                .OrderBy(x => x.DataEvento).Asc.List();
            
            return lista;
        }

        public CalendarioDia GetByCalendarioAndDia(int calendarioId, DateTime dataIni)
        {

            CalendarioDia model = Session.QueryOver<CalendarioDia>()
                .Where(x => x.Calendario.Id == calendarioId)
                .And(x => x.DataEvento == dataIni).SingleOrDefault();

            return model;
        }

        public CalendarioDiaVO GetVOByCalendarioAndDia(int calendarioId, DateTime dataIni)
        {
            CalendarioDiaVO model =
                Session.CreateQuery(
                "SELECT " +
                "tb.Id as Id, " +
                "tb.Calendario.Id as CalendarioId, " +
                "tb.TipoDia.Id as TipoDiaId, " +
                "tb.TipoDia.Id as TipoDiaId, " +
                "tb.TipoDia.Cor as TipoDiaCor, " +
                "tb.TipoDia.FlagLetivo as TipoDiaFlagLetivo, " +
                "tb.DataEvento as DataEvento " +
                "FROM CalendarioDia tb " +
                //"INNER JOIN tb.Turma t "+
                "INNER JOIN tb.TipoDia td " +
                "WHERE tb.Calendario.Id = :calendarioId " +
                "AND tb.DataEvento = :dataIni " 
                )
                .SetParameter("calendarioId", calendarioId)
                .SetParameter("dataIni", dataIni)
                .SetResultTransformer(Transformers.AliasToBean(typeof(CalendarioDiaVO)))
                .UniqueResult<CalendarioDiaVO>();

            return model;
        }


        public IEnumerable<CalendarioDiaVO> GetByListagemByCalendarioAndPeriodo(int calendarioId, DateTime dataIni, DateTime dataFim)
        {
        	IEnumerable<CalendarioDiaVO> model =
        		Session.CreateQuery(
    			"SELECT "+       		  
    		    "tb.Id as Id, "+
                "tb.Calendario.Id as CalendarioId, " +
                "tb.TipoDia.Id as TipoDiaId, " +
                "tb.TipoDia.Cor as TipoDiaCor, " +
                "tb.TipoDia.FlagLetivo as TipoDiaFlagLetivo, " +
                "tb.DataEvento as DataEvento " +
            	"FROM CalendarioDia tb "+
                //"INNER JOIN tb.Turma t "+
                "INNER JOIN tb.TipoDia td "+
        		"WHERE tb.Calendario.Id = :calendarioId "+
        		"AND tb.DataEvento BETWEEN :dataIni AND :dataFim "+
                "ORDER BY tb.DataEvento "
                )
        		.SetParameter("calendarioId",calendarioId)
        		.SetParameter("dataIni",dataIni)
        		.SetParameter("dataFim",dataFim)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(CalendarioDiaVO)))
        		.List<CalendarioDiaVO>();
        	
        	return model;
        }
        
    } // END CLASS
} // END NAMESPACE

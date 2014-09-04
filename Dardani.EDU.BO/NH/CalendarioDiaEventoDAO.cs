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
    public class CalendarioDiaEventoDAO : GenericDAO<CalendarioDiaEvento>
    {
        public IEnumerable<CalendarioDiaEvento> GetListagem()
        {
            IEnumerable<CalendarioDiaEvento> lista = Session.QueryOver<CalendarioDiaEvento>()
                .OrderBy(x => x.DataEvento).Asc.List();
            
            return lista;
        }
        
        public IEnumerable<CalendarioDiaEventoVO> GetByListagemByCalendarioAndPeriodo(int calendarioId, DateTime dataIni, DateTime dataFim)
        {
        	IEnumerable<CalendarioDiaEventoVO> model =
        		Session.CreateQuery(
    			"SELECT "+       		  
    		    "tb.Id as Id, "+
                "tb.Calendario.Id as CalendarioId, " +
                "tb.TipoEvento.Id as TipoEventoId, " +
                "tb.TipoEvento.Descricao as TipoEventoDescricao, " +
                "tb.TipoEvento.Simbolo as TipoEventoSimbolo, " +
                "tb.DataEvento as DataEvento " +
            	"FROM CalendarioDiaEvento tb "+
                //"INNER JOIN tb.Turma t "+
                "INNER JOIN tb.TipoEvento te "+
        		"WHERE tb.Calendario.Id = :calendarioId "+
        		"AND tb.DataEvento BETWEEN :dataIni AND :dataFim "+
                "ORDER BY tb.DataEvento "
                )
        		.SetParameter("calendarioId",calendarioId)
        		.SetParameter("dataIni",dataIni)
        		.SetParameter("dataFim",dataFim)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(CalendarioDiaEventoVO)))
        		.List<CalendarioDiaEventoVO>();
        	
        	return model;
        }
        
    } // END CLASS
} // END NAMESPACE

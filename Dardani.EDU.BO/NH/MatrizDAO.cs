using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dardani.EDU.Entities.VO;
using NHibernate.Transform;

namespace Dardani.EDU.BO.NH
{
    public class MatrizDAO : GenericDAO<Matriz>
    {
        public MatrizVO GetMatrizVOById(int id)
        {
        	MatrizVO model = 
        	Session.CreateQuery(
        		"SELECT "+
                "mt.Id as Id, "+
                "a.Id as AnoLetivoId, "+
                "m.Id as ModalidadeId, "+
                "e.Id as EtapaId, "+
                "mt.DiasLetivos as DiasLetivos, "+
                "mt.CargaHorariaAula as CargaHorariaAula, "+
                "mt.CargaHorariaSemanal as CargaHorariaSemanal, "+
                "mt.NumeroSemanasLetivas as NumeroSemanasLetivas "+
        		"FROM Matriz mt "+
        	    "INNER JOIN mt.AnoLetivo a "+
        	    "INNER JOIN mt.Etapa e "+
                "INNER JOIN e.Modalidade m " +
                "WHERE mt.Id = :id")
        		.SetParameter("id",id)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(MatrizVO)))
        		.UniqueResult<MatrizVO>();
        	
            if (model != null) {
            	MatrizDisciplinaDAO mdao = new MatrizDisciplinaDAO();
            	
            	model.Disciplinas = mdao.GetMatrizDisciplinaVOByMatriz(model.Id);
            }
            
            return model;
        }

        public IEnumerable<MatrizVO> GetListagemMatrizVOByEtapa(int etapaId)
        {
            IEnumerable<MatrizVO> model =
            Session.CreateQuery(
                "SELECT " +
                "mt.Id as Id, " +
                "a.Id as AnoLetivoId, " +
                "m.Id as ModalidadeId, " +
                "e.Id as EtapaId, " +
                "mt.DiasLetivos as DiasLetivos, " +
                "mt.CargaHorariaAula as CargaHorariaAula, " +
                "mt.CargaHorariaSemanal as CargaHorariaSemanal, " +
                "mt.NumeroSemanasLetivas as NumeroSemanasLetivas " +
                "FROM Matriz mt " +
                "INNER JOIN mt.AnoLetivo a " +
                "INNER JOIN mt.Etapa e " +
                "INNER JOIN e.Modalidade m " +
                "WHERE e.Id = :etapaId ")
                //.SetParameter("anoLetivoId", anoLetivoId)
                //.SetParameter("modalidadeId", modalidadeId)
                .SetParameter("etapaId", etapaId)
                .SetResultTransformer(Transformers.AliasToBean(typeof(MatrizVO)))
                .List<MatrizVO>();

            return model;
        }


        public MatrizVO GetMatrizVOByAnoLetivoModalidadeEtapa(int anoLetivoId, int modalidadeId, int etapaId)
        {
        	
        	MatrizVO model = 
        	Session.CreateQuery(
        		"SELECT "+
                "mt.Id as Id, "+
                "a.Id as AnoLetivoId, "+
                "m.Id as ModalidadeId, "+
                "e.Id as EtapaId, "+
                "mt.DiasLetivos as DiasLetivos, "+
                "mt.CargaHorariaAula as CargaHorariaAula, "+
                "mt.CargaHorariaSemanal as CargaHorariaSemanal, "+
                "mt.NumeroSemanasLetivas as NumeroSemanasLetivas "+
        		"FROM Matriz mt "+
        	    "INNER JOIN mt.AnoLetivo a "+
        	    "INNER JOIN mt.Etapa e "+
                "INNER JOIN e.Modalidade m " +
                "WHERE a.Id = :anoLetivoId " +
            	"AND m.Id = :modalidadeId "+
            	"AND e.Id = :etapaId ")
        		.SetParameter("anoLetivoId",anoLetivoId)
        		.SetParameter("modalidadeId",modalidadeId)
        		.SetParameter("etapaId",etapaId)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(MatrizVO)))
        		.UniqueResult<MatrizVO>();
        	
        	return model;
        }

    } // END CLASS
} // END NAMESPACE

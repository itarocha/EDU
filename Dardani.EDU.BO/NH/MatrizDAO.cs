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
        	    "INNER JOIN mt.Modalidade m "+
        	    "INNER JOIN mt.Etapa e "+
        	    "WHERE mt.Id = :id")
        		.SetParameter("id",id)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(MatrizVO)))
        		.UniqueResult<MatrizVO>();
        	
        	/*
            MatrizVO avo = null;
            Matriz mt = null;
            AnoLetivo a = null;
            Modalidade m = null;
            Etapa e = null;

            MatrizVO model =
            Session.QueryOver<Matriz>(() => mt)
                .Inner.JoinQueryOver<AnoLetivo>(() => mt.AnoLetivo, () => a)
                .Inner.JoinQueryOver<Modalidade>(() => mt.Modalidade, () => m)
                .Inner.JoinQueryOver<Etapa>(() => mt.Etapa, () => e)
                .SelectList(list => list
                    .Select(() => mt.Id).WithAlias(() => avo.Id)
                    .Select(() => a.Id).WithAlias(() => avo.AnoLetivoId)
                    .Select(() => m.Id).WithAlias(() => avo.ModalidadeId)
                    .Select(() => e.Id).WithAlias(() => avo.EtapaId)
                    .Select(() => mt.DiasLetivos).WithAlias(() => avo.DiasLetivos)
                    .Select(() => mt.CargaHorariaAula).WithAlias(() => avo.CargaHorariaAula)
                    .Select(() => mt.CargaHorariaSemanal).WithAlias(() => avo.CargaHorariaSemanal)
                    .Select(() => mt.NumeroSemanasLetivas).WithAlias(() => avo.NumeroSemanasLetivas)
                ).Where(() => mt.Id == id)
                .TransformUsing(Transformers.AliasToBean<MatrizVO>())
                .SingleOrDefault<MatrizVO>();
            */
            if (model != null) {
            	MatrizDisciplinaDAO mdao = new MatrizDisciplinaDAO();
            	
            	model.Disciplinas = mdao.GetMatrizDisciplinaVOByMatriz(model.Id);
            }
            
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
        	    "INNER JOIN mt.Modalidade m "+
        	    "INNER JOIN mt.Etapa e "+
                "WHERE a.Id = :anoLetivoId "+
            	"AND m.Id = :modalidadeId "+
            	"AND e.Id = :etapaId ")
        		.SetParameter("anoLetivoId",anoLetivoId)
        		.SetParameter("modalidadeId",modalidadeId)
        		.SetParameter("etapaId",etapaId)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(MatrizVO)))
        		.UniqueResult<MatrizVO>();
        	
        	return model;
        	
        	/*
            MatrizVO avo = null;
            Matriz mt = null;
            AnoLetivo a = null;
            Modalidade m = null;
            Etapa e = null;

            MatrizVO model =
            Session.QueryOver<Matriz>(() => mt)
                .Inner.JoinQueryOver<AnoLetivo>(() => mt.AnoLetivo, () => a)
                .Inner.JoinQueryOver<Modalidade>(() => mt.Modalidade, () => m)
                .Inner.JoinQueryOver<Etapa>(() => mt.Etapa, () => e)
                .SelectList(list => list
                    .Select(() => mt.Id).WithAlias(() => avo.Id)
                    .Select(() => a.Id).WithAlias(() => avo.AnoLetivoId)
                    .Select(() => m.Id).WithAlias(() => avo.ModalidadeId)
                    .Select(() => e.Id).WithAlias(() => avo.EtapaId)
                    .Select(() => mt.DiasLetivos).WithAlias(() => avo.DiasLetivos)
                    .Select(() => mt.CargaHorariaAula).WithAlias(() => avo.CargaHorariaAula)
                    .Select(() => mt.CargaHorariaSemanal).WithAlias(() => avo.CargaHorariaSemanal)
                    .Select(() => mt.NumeroSemanasLetivas).WithAlias(() => avo.NumeroSemanasLetivas)
                ).Where(() => a.Id == anoLetivoId)
            	.And(() => m.Id == modalidadeId )
            	.And(() => e.Id == etapaId )
                .TransformUsing(Transformers.AliasToBean<MatrizVO>())
                .SingleOrDefault<MatrizVO>();
            return model;
            */
        }

    } // END CLASS
} // END NAMESPACE

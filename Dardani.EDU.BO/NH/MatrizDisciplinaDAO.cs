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
    public class MatrizDisciplinaDAO : GenericDAO<MatrizDisciplina>
    {
        public MatrizDisciplinaVO GetMatrizDisciplinaVOById(int id)
        {
        	MatrizDisciplinaVO model = Session.CreateQuery(
        		"SELECT "+
                "md.Id as Id, "+
                "m.Id as MatrizId, "+
                "d.Id as DisciplinaId, "+
                "d.Descricao as DisciplinaDescricao, "+
                "d.DescricaoAbreviada as DisciplinaDescricaoAbreviada, "+
                "md.CargaHorariaSemanal as CargaHorariaSemanal, "+
                "md.FlagTipoAvaliacao as FlagTipoAvaliacao, "+
                "md.FlagCategoria as FlagCategoria, "+
                "md.FlagAceitaDispensa as FlagAceitaDispensa, "+
                "md.FlagReprova as FlagReprova "+
        		"FROM MatrizDisciplina md "+
        		"INNER JOIN md.Disciplina d "+
        		"INNER JOIN md.Matriz m "+
        	    "WHERE md.Id = :id")
        		.SetParameter("id",id)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(MatrizDisciplinaVO)))
        		.UniqueResult<MatrizDisciplinaVO>();
        	
        	return model;
        }

        public IEnumerable<MatrizDisciplinaVO> GetMatrizDisciplinaVOByMatriz(int matrizId)
        {
        	IEnumerable<MatrizDisciplinaVO> model = Session.CreateQuery(
        		"SELECT "+
                "md.Id as Id, "+
                "m.Id as MatrizId, "+
                "d.Id as DisciplinaId, "+
                "d.Descricao as DisciplinaDescricao, "+
                "d.DescricaoAbreviada as DisciplinaDescricaoAbreviada, "+
                "md.CargaHorariaSemanal as CargaHorariaSemanal, "+
                "md.FlagTipoAvaliacao as FlagTipoAvaliacao, "+
                "md.FlagCategoria as FlagCategoria, "+
                "md.FlagAceitaDispensa as FlagAceitaDispensa, "+
                "md.FlagReprova as FlagReprova "+
        		"FROM MatrizDisciplina md "+
        		"INNER JOIN md.Disciplina d "+
        		"INNER JOIN md.Matriz m " +
        	    "WHERE m.Id = :matrizId "+
        	    "ORDER BY d.Descricao ")
        		.SetParameter("matrizId",matrizId)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(MatrizDisciplinaVO)))
        		.List<MatrizDisciplinaVO>();
        	
        	return model;
        }
        public IEnumerable<MatrizDisciplinaVO> GetMatrizDisciplinaVOByModaliadeEtapa(int modalidadeId, int etapaId)
        {
            //MatrizDisciplina z;
            //z.Matriz.Modalidade

            IEnumerable<MatrizDisciplinaVO> model = Session.CreateQuery(
                "SELECT " +
                "md.Id as Id, " +
                "m.Id as MatrizId, " +
                "d.Id as DisciplinaId, " +
                "d.Descricao as DisciplinaDescricao, " +
                "d.DescricaoAbreviada as DisciplinaDescricaoAbreviada, " +
                "md.CargaHorariaSemanal as CargaHorariaSemanal, " +
                "md.FlagTipoAvaliacao as FlagTipoAvaliacao, " +
                "md.FlagCategoria as FlagCategoria, " +
                "md.FlagAceitaDispensa as FlagAceitaDispensa, " +
                "md.FlagReprova as FlagReprova " +
                "FROM MatrizDisciplina md " +
                "INNER JOIN md.Disciplina d " +
                "INNER JOIN md.Matriz m " +
                "WHERE  m.Modalidade.Id = :modalidadeId " +
                "AND m.Etapa.Id = :etapaId " +
                "ORDER BY d.Descricao ")
                .SetParameter("modalidadeId", modalidadeId)
                .SetParameter("etapaId", etapaId)
                .SetResultTransformer(Transformers.AliasToBean(typeof(MatrizDisciplinaVO)))
                .List<MatrizDisciplinaVO>();

            return model;
        }

    } // END CLASS
} // END NAMESPACE

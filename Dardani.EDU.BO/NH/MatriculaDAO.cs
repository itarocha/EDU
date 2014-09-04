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
    public class MatriculaDAO : GenericDAO<Matricula>
    {

        public Matricula GetByValorEducacenso(string codigoAluno, string codigoTurma)
        {
            Matricula value = Session.QueryOver<Matricula>()
                .Where(x => x.Pessoa.CodigoINEP == codigoAluno)
                .Where(x => x.Turma.CodigoINEP == codigoTurma)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<MatriculaVO> GetListaMatriculasPorTurma(int turmaId)
        {
            IEnumerable<MatriculaVO> model =
            Session.CreateQuery(
            "SELECT " +
            "m.Id as Id, "+
            "p.Id as PessoaId, "+
            "p.Nome as PessoaNome, "+
            "p.NumeroNIS as PessoaNumeroNIS, "+
            "p.DataNascimento as PessoaDataNascimento, "+
            "al.Id as AnoLetivoId, "+
            "al.Ano as AnoLetivoAno, "+
            "t.Id as TurmaId, "+
            "t.Nome as TurmaNome, "+
            "ee.Id as EscolarizacaoEspecialId, "+
            "ee.Descricao as EscolarizacaoEspecialDescricao, "+
            "tp.Id as TransportePublicoId, "+
            "tp.Descricao as TransportePublicoDescricao, "+
            "tu.Id as TurmaUnificadaId, "+
            "tu.Descricao as TurmaUnificadaDescricao, "+
            "m.FlagRematricular as FlagRematricular "+
            "FROM Matricula m " +
            "INNER JOIN m.Pessoa p " +
            "INNER JOIN m.AnoLetivo al " +
            "INNER JOIN m.Turma t " +
            "INNER JOIN m.EscolarizacaoEspecial ee " +
            "INNER JOIN m.TransportePublico tp " +
            "INNER JOIN m.TurmaUnificada tu " +
            "WHERE t.Id = :turmaId " +
            "ORDER BY p.Nome ")
            .SetParameter("turmaId", turmaId)
            .SetResultTransformer(Transformers.AliasToBean(typeof(MatriculaVO)))
            .List<MatriculaVO>();

            return model;
        }


        public IEnumerable<MatriculaVO> GetListaMatriculasParaRematricular(int turmaId)
        {
        	IEnumerable<MatriculaVO> model =

        		Session.CreateQuery(
        		"SELECT "+
                "m.Id as Id, "+
                "p.Id as PessoaId, "+
                "p.Nome as PessoaNome, "+
                "p.NumeroNIS as PessoaNumeroNIS, "+
                "m.FlagRematricular as FlagRematricular "+
        		"FROM Matricula m "+
        	    "INNER JOIN m.Pessoa p "+
        	    "INNER JOIN m.Turma t "+
        	    "WHERE t.Id = :turmaId "+
        	    "AND m.FlagRematricular = :flagRematricular "+
        	    "ORDER BY p.Nome")
        		.SetParameter("turmaId", turmaId)
        		.SetParameter("flagRematricular","S")
        		.SetResultTransformer(Transformers.AliasToBean(typeof(MatriculaVO)))
        		.List<MatriculaVO>();

			        	
			/*        	
            MatriculaVO mvo = null;
            Matricula m = null;

            Pessoa p = null;
            AnoLetivo al = null;
            Turma t = null;
            //EscolarizacaoEspecial ee = null;
            //TransportePublico tp = null;
            //TurmaUnificada tu = null;

            var q =

            Session.QueryOver<Matricula>(() => m)
                .Inner.JoinQueryOver<Pessoa>(() => m.Pessoa, () => p)
                //.Inner.JoinQueryOver<AnoLetivo>(() => m.AnoLetivo, () => al)
                //.Inner.JoinQueryOver<Turma>(() => m.Turma, () => t)
                //.Inner.JoinQueryOver<EscolarizacaoEspecial>(() => m.EscolarizacaoEspecial, () => ee)
                //.Inner.JoinQueryOver<TransportePublico>(() => m.TransportePublico, () => tp)
                //.Inner.JoinQueryOver<TurmaUnificada>(() => m.TurmaUnificada, () => tu)
                .SelectList(list => list
                    .Select(() => m.Id).WithAlias(() => mvo.Id)
                    .Select(() => p.Id).WithAlias(() => mvo.PessoaId)
                    .Select(() => p.Nome).WithAlias(() => mvo.PessoaNome)
                    .Select(() => p.NumeroNIS).WithAlias(() => mvo.PessoaNumeroNIS)
                    .Select(() => p.DataNascimento).WithAlias(() => mvo.PessoaDataNascimento)
                    //.Select(() => al.Id).WithAlias(() => mvo.AnoLetivoId)
                    //.Select(() => al.Ano).WithAlias(() => mvo.AnoLetivoAno)
                    //.Select(() => t.Id).WithAlias(() => mvo.TurmaId)
                    //.Select(() => t.Nome).WithAlias(() => mvo.TurmaNome)
                    //.Select(() => ee.Id).WithAlias(() => mvo.EscolarizacaoEspecialId)
                    //.Select(() => ee.Descricao).WithAlias(() => mvo.EscolarizacaoEspecialDescricao)
                    //.Select(() => tp.Id).WithAlias(() => mvo.TransportePublicoId)
                    //.Select(() => tp.Descricao).WithAlias(() => mvo.TransportePublicoDescricao)
                    //.Select(() => tu.Id).WithAlias(() => mvo.TurmaUnificadaId)
                    //.Select(() => tu.Descricao).WithAlias(() => mvo.TurmaUnificadaDescricao)
                    .Select(() => m.FlagRematricular).WithAlias(() => mvo.FlagRematricular)
                )
                .Where(() => m.Turma.Id == turmaId)
                .And(() => m.FlagRematricular == "S");
            

            IEnumerable<MatriculaVO> retorno =
               q.TransformUsing(Transformers.AliasToBean<MatriculaVO>())
               .List<MatriculaVO>()
               .OrderBy(x => x.PessoaNome);
			*/
            return model;
        }


        public MatriculaVO GetVOById(int id)
        {
            MatriculaVO model =
            Session.CreateQuery(
            "SELECT " +
            "m.Id as Id, "+
            "p.Id as PessoaId, "+
            "p.Nome as PessoaNome, "+
            "p.NumeroNIS as PessoaNumeroNIS, "+
            "p.DataNascimento as PessoaDataNascimento, "+
            "al.Id as AnoLetivoId, "+
            "al.Ano as AnoLetivoAno, "+
            "t.Id as TurmaId, "+
            "t.Nome as TurmaNome, "+
            "ee.Id as EscolarizacaoEspecialId, "+
            "ee.Descricao as EscolarizacaoEspecialDescricao, "+
            "tp.Id as TransportePublicoId, "+
            "tp.Descricao as TransportePublicoDescricao, "+
            "tu.Id as TurmaUnificadaId, "+
            "tu.Descricao as TurmaUnificadaDescricao, "+
            "m.FlagRematricular as FlagRematricular "+
            "FROM Matricula m " +
            "INNER JOIN m.Pessoa p " +
            "INNER JOIN m.AnoLetivo al " +
            "INNER JOIN m.Turma t " +
            "INNER JOIN m.EscolarizacaoEspecial ee " +
            "INNER JOIN m.TransportePublico tp " +
            "INNER JOIN m.TurmaUnificada tu " +
            "WHERE m.Id = :id " +
            "ORDER BY p.Nome ")
            .SetParameter("id", id)
            .SetResultTransformer(Transformers.AliasToBean(typeof(MatriculaVO)))
            .UniqueResult<MatriculaVO>();

            return model;
        	
        	/*
            MatriculaVO mvo = null;
            Matricula m = null;

            Pessoa p = null;
            AnoLetivo al = null;
            Turma t = null;
            EscolarizacaoEspecial ee = null;
            TransportePublico tp = null;
            TurmaUnificada tu = null;

            MatriculaVO model =

            Session.QueryOver<Matricula>(() => m)
                .Inner.JoinQueryOver<Pessoa>(() => m.Pessoa, () => p)
                .Inner.JoinQueryOver<AnoLetivo>(() => m.AnoLetivo, () => al)
                .Inner.JoinQueryOver<Turma>(() => m.Turma, () => t)
                .Inner.JoinQueryOver<EscolarizacaoEspecial>(() => m.EscolarizacaoEspecial, () => ee)
                .Inner.JoinQueryOver<TransportePublico>(() => m.TransportePublico, () => tp)
                .Inner.JoinQueryOver<TurmaUnificada>(() => m.TurmaUnificada, () => tu)
                .SelectList(list => list
                    .Select(() => m.Id).WithAlias(() => mvo.Id)
                    .Select(() => p.Id).WithAlias(() => mvo.PessoaId)
                    .Select(() => p.Nome).WithAlias(() => mvo.PessoaNome)
                    .Select(() => p.NumeroNIS).WithAlias(() => mvo.PessoaNumeroNIS)
                    .Select(() => p.DataNascimento).WithAlias(() => mvo.PessoaDataNascimento)
                    //.Select(() => p).WithAlias(() => mvo.Pessoa)
                    .Select(() => al.Id).WithAlias(() => mvo.AnoLetivoId)
                    .Select(() => al.Ano).WithAlias(() => mvo.AnoLetivoAno)
                    .Select(() => t.Id).WithAlias(() => mvo.TurmaId)
                    .Select(() => t.Nome).WithAlias(() => mvo.TurmaNome)
                    .Select(() => ee.Id).WithAlias(() => mvo.EscolarizacaoEspecialId)
                    .Select(() => ee.Descricao).WithAlias(() => mvo.EscolarizacaoEspecialDescricao)
                    .Select(() => tp.Id).WithAlias(() => mvo.TransportePublicoId)
                    .Select(() => tp.Descricao).WithAlias(() => mvo.TransportePublicoDescricao)
                    .Select(() => tu.Id).WithAlias(() => mvo.TurmaUnificadaId)
                    .Select(() => tu.Descricao).WithAlias(() => mvo.TurmaUnificadaDescricao)
                    .Select(() => m.FlagRematricular).WithAlias(() => mvo.FlagRematricular)
                ).Where(() => m.Id == id)
                .TransformUsing(Transformers.AliasToBean<MatriculaVO>())
                .SingleOrDefault<MatriculaVO>();

            return model;
            */
        }

    } // END CLASS
} // END NAMESPACE

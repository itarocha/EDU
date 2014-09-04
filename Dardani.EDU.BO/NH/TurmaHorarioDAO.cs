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
    public class TurmaHorarioDAO : GenericDAO<TurmaHorario>
    {

        public IEnumerable<TurmaHorarioVO> GetListagemByTurma(int turmaId)
        {
            TurmaHorario th;
            //th.PeriodoAula

            IEnumerable<TurmaHorarioVO> model = Session.CreateQuery(
                "SELECT "+
                "th.Id as Id, "+
                "th.FlagDiaSemana as FlagDiaSemana, " +
                "d.Id as DisciplinaId, " +
                "d.Descricao as DisciplinaDescricao, " +
                "p.Id as PessoaId, " +
                "p.Nome as PessoaNome, " +
                "t.Id as TurmaId, " +
                "pa.Id as PeriodoAulaId "+
                "FROM TurmaHorario th "+
                "INNER JOIN th.Disciplina d "+
                "INNER JOIN th.Pessoa p "+
                "INNER JOIN th.Turma t "+
                "INNER JOIN th.PeriodoAula pa " +
                "WHERE t.Id = :turmaId " +
                "ORDER BY pa.Id, th.FlagDiaSemana "
            )
            .SetParameter("turmaId", turmaId)
            .SetResultTransformer(Transformers.AliasToBean(typeof(TurmaHorarioVO)))
            .List<TurmaHorarioVO>();

            return model;
        }


    } // END CLASS
} // END NAMESPACE

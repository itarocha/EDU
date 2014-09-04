using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TurmaHorarioMap : ClassMap<TurmaHorario>
    {
        public TurmaHorarioMap()
        {
            Table("EDU_TURMA_HORARIO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TURMA_HORARIO");
            References(x => x.Turma).Column("ID_TURMA").Not.Nullable();
            References(x => x.PeriodoAula).Column("ID_PERIODO_AULA").Not.Nullable();
            References(x => x.Disciplina).Column("ID_DISCIPLINA").Not.Nullable();
            References(x => x.Pessoa).Column("ID_PESSOA").Not.Nullable();
            Map(x => x.FlagDiaSemana).Column("FL_DIA_SEMANA").Not.Nullable();
        }
    }
}

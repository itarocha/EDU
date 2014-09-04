using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class HorarioPeriodoMap : ClassMap<HorarioPeriodo>
    {
        public HorarioPeriodoMap()
        {
            Table("EDU_HORARIO_PERIODO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_HORARIO_PERIODO");
            References(x => x.Horario).Column("ID_HORARIO").Not.Nullable();
            References(x => x.PeriodoAula).Column("ID_PERIODO_AULA").Not.Nullable();
        }
    }
}

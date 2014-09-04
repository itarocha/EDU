using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class PeriodoAulaMap : ClassMap<PeriodoAula>
    {
        public PeriodoAulaMap()
        {
            Table("EDU_PERIODO_AULA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_PERIODO_AULA");
            Map(x => x.HoraInicio).Column("DS_HORA_INICIO").Length(5).Not.Nullable();
            Map(x => x.HoraTermino).Column("DS_HORA_TERMINO").Length(5).Not.Nullable();
        }
    }
}

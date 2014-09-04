using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class CalendarioDiaMap : ClassMap<CalendarioDia>
    {
        public CalendarioDiaMap()
        {
            Table("EDU_CALENDARIO_DIA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_CALENDARIO_DIA");
            Map(x => x.DataEvento).Column("DT_EVENTO").Not.Nullable();
            References(x => x.Calendario).Column("ID_CALENDARIO").Not.Nullable();
            References(x => x.TipoDia).Column("ID_TIPO_DIA").Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class CalendarioDiaEventoMap : ClassMap<CalendarioDiaEvento>
    {
        public CalendarioDiaEventoMap()
        {
            Table("EDU_CALENDARIO_DIA_EVENTO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_CALENDARIO_DIA_EVENTO");
            Map(x => x.DataEvento).Column("DT_EVENTO").Not.Nullable();
            References(x => x.Calendario).Column("ID_CALENDARIO").Not.Nullable();
            References(x => x.TipoEvento).Column("ID_TIPO_EVENTO").Not.Nullable();
        }
    }
}

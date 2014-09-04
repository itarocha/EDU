using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class CalendarioMap : ClassMap<Calendario>
    {
        public CalendarioMap()
        {
            Table("EDU_CALENDARIO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_CALENDARIO");
            Map(x => x.Descricao).Column("DS_CALENDARIO").Length(32).Not.Nullable();
            Map(x => x.DataInicio).Column("DT_INICIO").Not.Nullable();
            Map(x => x.DataTermino).Column("DT_TERMINO").Not.Nullable();
            Map(x => x.DataResultado).Column("DT_RESULTADO").Not.Nullable();
            Map(x => x.DiasLetivos).Column("NM_DIAS_LETIVOS").Not.Nullable();

            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.AnoLetivo).Column("ID_ANO_LETIVO").Not.Nullable();
        }
    }
}

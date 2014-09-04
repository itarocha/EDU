using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class AnoLetivoMap : ClassMap<AnoLetivo>
    {
        public AnoLetivoMap()
        {
            Table("EDU_ANO_LETIVO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_AEE"); // MUDAR PARA ID_ANO_LETIVO
            Map(x => x.Ano).Column("VL_ANO").Not.Nullable();
            Map(x => x.FlagStatus).Column("FL_STATUS").Length(1).Not.Nullable();
        }
    }
}

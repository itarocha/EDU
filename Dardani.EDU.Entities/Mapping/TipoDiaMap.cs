using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TipoDiaMap : ClassMap<TipoDia>
    {
        public TipoDiaMap()
        {
            Table("EDU_TIPO_DIA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TIPO_DIA");
            Map(x => x.Descricao).Column("DS_TIPO_DIA").Length(64).Not.Nullable();
            Map(x => x.Cor).Column("DS_COR").Length(16).Not.Nullable();
            Map(x => x.FlagLetivo).Column("FL_LETIVO").Length(1).Not.Nullable();
        }
    }
}

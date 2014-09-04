using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class ZonaMap : ClassMap<Zona>
    {
        public ZonaMap()
        {
            Table("EDU_ZONA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ZONA");
            Map(x => x.Descricao).Column("DS_ZONA").Length(32).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

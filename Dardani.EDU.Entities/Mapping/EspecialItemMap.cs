using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EspecialItemMap : ClassMap<EspecialItem>
    {
        public EspecialItemMap()
        {
            Table("EDU_ESPECIAL_ITEM");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESPECIAL_ITEM");
            Map(x => x.Descricao).Column("DS_ESPECIAL_ITEM").Length(64).Not.Nullable();
            References(x => x.EspecialCategoria).Column("ID_ESPECIAL_CATEGORIA").Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class AEEMap : ClassMap<AEE>
    {
        public AEEMap()
        {
            Table("EDU_AEE");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_AEE");
            Map(x => x.Descricao).Column("DS_AEE").Length(64).Not.Nullable();
            Map(x => x.FlagPossui).Column("FL_POSSUI").Length(1).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
            
        }
    }
}

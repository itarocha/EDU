using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class InfraestruturaItemMap : ClassMap<InfraestruturaItem>
    {
        public InfraestruturaItemMap()
        {
            Table("EDU_INFRAESTRUTURA_ITEM");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_INFRAESTRUTURA_ITEM");
            Map(x => x.Descricao).Column("DS_INFRAESTRUTURA_CATEGORIA").Length(128).Not.Nullable();
            References(x => x.InfraestruturaCategoria).Column("ID_INFRAESTRUTURA_CATEGORIA").Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

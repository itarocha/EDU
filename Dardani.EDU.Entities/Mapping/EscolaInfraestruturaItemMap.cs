using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaInfraestruturaItemMap : ClassMap<EscolaInfraestruturaItem>
    {
        public EscolaInfraestruturaItemMap()
        {
            Table("EDU_ESCOLA_INFRAESTRUTURA_ITEM");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESCOLA_INFRAESTRUTURA_ITEM");
            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.InfraestruturaItem).Column("ID_INFRAESTRUTURA_ITEM").Not.Nullable();
        }
    }
}

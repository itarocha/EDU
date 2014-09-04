using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaLocalMap : ClassMap<EscolaLocal>
    {
        public EscolaLocalMap()
        {
            Table("EDU_ESCOLA_LOCAL");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESCOLA_LOCAL");
            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.LocalEscola).Column("ID_LOCAL_ESCOLA").Not.Nullable();
        }
    }
}

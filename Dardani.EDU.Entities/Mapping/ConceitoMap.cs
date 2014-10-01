using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class ConceitoMap : ClassMap<Conceito>
    {
        public ConceitoMap()
        {
            Table("EDU_CONCEITO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_CONCEITO");
            Map(x => x.Descricao).Column("DS_CONCEITO").Length(64).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

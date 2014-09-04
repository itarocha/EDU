using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class LocalEscolaMap : ClassMap<LocalEscola>
    {
        public LocalEscolaMap()
        {
            Table("EDU_LOCAL_ESCOLA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_LOCAL_ESCOLA");
            Map(x => x.Descricao).Column("DS_LOCAL_ESCOLA").Length(64).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

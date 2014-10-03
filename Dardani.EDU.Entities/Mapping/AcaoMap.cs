using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class AcaoMap : ClassMap<Acao>
    {
        public AcaoMap()
        {
            Table("GER_ACAO");
            Id(x => x.Id).GeneratedBy.Assigned().Column("ID_ACAO").Length(64);
            Map(x => x.Descricao).Column("DS_ACAO").Length(128).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

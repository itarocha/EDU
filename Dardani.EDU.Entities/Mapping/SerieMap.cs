using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class SerieMap : ClassMap<Serie>
    {
        public SerieMap()
        {
            Table("EDU_SERIE");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_SERIE");
            Map(x => x.Descricao).Column("DS_SERIE").Length(128).Not.Nullable();
            Map(x => x.Abreviatura).Column("DS_ABREVIATURA").Length(32).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

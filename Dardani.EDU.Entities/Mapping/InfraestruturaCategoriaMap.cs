using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class InfraestruturaCategoriaMap : ClassMap<InfraestruturaCategoria>
    {
        public InfraestruturaCategoriaMap()
        {
            Table("EDU_INFRAESTRUTURA_CATEGORIA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_INFRAESTRUTURA_CATEGORIA");
            Map(x => x.Descricao).Column("DS_INFRAESTRUTURA_CATEGORIA").Length(128).Not.Nullable();
            Map(x => x.FlagMuitos).Column("FL_MUITOS").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

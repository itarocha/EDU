using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EspecialCategoriaMap : ClassMap<EspecialCategoria>
    {
        public EspecialCategoriaMap()
        {
            Table("EDU_ESPECIAL_CATEGORIA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESPECIAL_CATEGORIA");
            Map(x => x.Descricao).Column("DS_ESPECIAL_CATEGORIA").Length(64).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

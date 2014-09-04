using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class MaterialEspecificoMap : ClassMap<MaterialEspecifico>
    {
        public MaterialEspecificoMap()
        {
            Table("EDU_MATERIAL_ESPECIFICO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_MATERIAL_ESPECIFICO");
            Map(x => x.Descricao).Column("DS_MATERIAL_ESPECIFICO").Length(128).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

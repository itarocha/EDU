using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class LinguaIndigenaMap : ClassMap<LinguaIndigena>
    {
        public LinguaIndigenaMap()
        {
            Table("EDU_LINGUA_INDIGENA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_LINGUA_INDIGENA");
            Map(x => x.Descricao).Column("DS_LINGUA_INDIGENA").Length(128).Not.Nullable();
            Map(x => x.Codigo).Column("VL_CODIGO").Not.Nullable();
            Map(x => x.FlagIndigena).Column("FL_LINGUA_INDIGENA").Length(1).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class PaisMap : ClassMap<Pais>
    {
        public PaisMap()
        {
            Table("EDU_PAIS");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_PAIS");
            Map(x => x.Descricao).Column("DS_PAIS").Length(64).Not.Nullable();
            Map(x => x.Codigo).Column("VL_CODIGO").Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class PosGraduacaoMap : ClassMap<PosGraduacao>
    {
        public PosGraduacaoMap()
        {
            Table("EDU_POS_GRADUACAO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_POS_GRADUACAO");
            Map(x => x.Descricao).Column("DS_POS_GRADUACAO").Length(64).Not.Nullable();
            Map(x => x.FlagPosGraduacao).Column("FL_POS_GRADUACAO").Length(1).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

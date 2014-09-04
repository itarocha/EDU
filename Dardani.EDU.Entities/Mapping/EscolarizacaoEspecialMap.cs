using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolarizacaoEspecialMap : ClassMap<EscolarizacaoEspecial>
    {
        public EscolarizacaoEspecialMap()
        {
            Table("EDU_ESCOLARIZACA_ESPECIAL");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESCOLARIZACA_ESPECIAL");
            Map(x => x.Descricao).Column("DS_ESCOLARIZACA_ESPECIAL").Length(64).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagRecebe).Column("FL_RECEBE").Length(1).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TransportePublicoMap : ClassMap<TransportePublico>
    {
        public TransportePublicoMap()
        {
            Table("EDU_TRANSPORTE_PUBLICO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TRANSPORTE_PUBLICO");
            Map(x => x.Descricao).Column("DS_TRANSPORTE_PUBLICO").Length(64).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagUtiliza).Column("FL_UTILIZA").Length(1).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

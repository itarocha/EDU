using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TipoCertidaoMap : ClassMap<TipoCertidao>
    {
        public TipoCertidaoMap()
        {
            Table("EDU_TIPO_CERTIDAO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TIPO_CERTIDAO");
            Map(x => x.Descricao).Column("DS_DESCRICAO").Length(64).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

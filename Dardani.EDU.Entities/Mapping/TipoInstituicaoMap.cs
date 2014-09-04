using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TipoInstituicaoMap : ClassMap<TipoInstituicao>
    {
        public TipoInstituicaoMap()
        {
            Table("EDU_TIPO_INSTITUICAO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TIPO_INSTITUICAO");
            Map(x => x.Descricao).Column("DS_DESCRICAO").Length(64).Not.Nullable();
            //Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

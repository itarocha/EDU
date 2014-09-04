using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TipoNacionalidadeMap : ClassMap<TipoNacionalidade>
    {
        public TipoNacionalidadeMap()
        {
            Table("EDU_TIPO_NACIONALIDADE");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TIPO_NACIONALIDADE");
            Map(x => x.Descricao).Column("DS_TIPO_NACIONALIDADE").Length(64).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagBrasileira).Column("FL_BRASILEIRA").Length(1).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

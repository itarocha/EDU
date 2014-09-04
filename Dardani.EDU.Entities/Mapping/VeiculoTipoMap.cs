using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class VeiculoTipoMap : ClassMap<VeiculoTipo>
    {
        public VeiculoTipoMap()
        {
            Table("EDU_VEICULO_TIPO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_VEICULO_TIPO");
            Map(x => x.Descricao).Column("DS_VEICULO_TIPO").Length(128).Not.Nullable();
            References(x => x.VeiculoCategoria).Column("ID_VEICULO_CATEGORIA").Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

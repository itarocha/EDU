using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class VeiculoCategoriaMap : ClassMap<VeiculoCategoria>
    {
        public VeiculoCategoriaMap()
        {
            Table("EDU_VEICULO_CATEGORIA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_VEICULO_CATEGORIA");
            Map(x => x.Descricao).Column("DS_VEICULO_CATEGORIA").Length(64).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

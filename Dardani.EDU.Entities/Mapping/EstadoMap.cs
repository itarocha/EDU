using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EstadoMap : ClassMap<Estado>
    {
        public EstadoMap()
        {
            Table("EDU_ESTADO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESTADO");
            Map(x => x.Descricao).Column("DS_ESTADO").Length(64).Not.Nullable();
            Map(x => x.Sigla).Column("DS_SIGLA").Length(2).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

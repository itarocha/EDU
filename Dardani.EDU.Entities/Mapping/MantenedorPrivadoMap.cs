using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class MantenedorPrivadoMap : ClassMap<MantenedorPrivado>
    {
        public MantenedorPrivadoMap()
        {
            Table("EDU_MANTENEDOR_PRIVADO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_MANTENEDOR_PRIVADO");
            Map(x => x.Descricao).Column("DS_MANTENEDOR_PRIVADO").Length(128).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

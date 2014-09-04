using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EstadoCivilMap : ClassMap<EstadoCivil>
    {
        public EstadoCivilMap()
        {
            Table("EDU_ESTADO_CIVIL");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESTADO_CIVIL");
            Map(x => x.Descricao).Column("DS_ESTADO_CIVIL").Length(128).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
        }
    }
}

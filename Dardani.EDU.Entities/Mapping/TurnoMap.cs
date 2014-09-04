using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TurnoMap : ClassMap<Turno>
    {
        public TurnoMap()
        {
            Table("EDU_TURNO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TURNO");
            Map(x => x.Descricao).Column("DS_TURNO").Length(64).Not.Nullable();
        }
    }
}

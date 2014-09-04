using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaTurnoMap : ClassMap<EscolaTurno>
    {
        public EscolaTurnoMap()
        {
            Table("EDU_ESCOLA_TURNO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESCOLA_TURNO");
            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.Turno).Column("ID_TURNO").Not.Nullable();
        }
    }
}

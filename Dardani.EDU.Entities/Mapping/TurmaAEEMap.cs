using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TurmaAEEMap : ClassMap<TurmaAEE>
    {
        public TurmaAEEMap()
        {
            Table("EDU_TURMA_AEE");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TURMA_AEE");
            References(x => x.Turma).Column("ID_TURMA").Not.Nullable();
            References(x => x.TipoAEE).Column("ID_TIPO_AEE").Not.Nullable();
        }
    }
}

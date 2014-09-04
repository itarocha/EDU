using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TurmaComplementarMap : ClassMap<TurmaComplementar>
    {
        public TurmaComplementarMap()
        {
            Table("EDU_TURMA_COMPLEMENTAR");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TURMA_COMPLEMENTAR");
            References(x => x.Turma).Column("ID_TURMA").Not.Nullable();
            References(x => x.TipoComplementar).Column("ID_TIPO_COMPLEMENTAR").Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class PeriodoAnoMap : ClassMap<PeriodoAno>
    {
        public PeriodoAnoMap()
        {
            Table("EDU_PERIODO_ANO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_PERIODO_ANO");
            Map(x => x.Descricao).Column("DS_PERIODO_ANO").Length(64).Not.Nullable();
            Map(x => x.Descricao).Column("DS_SIGLA_PERIODO_ANO").Length(8).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

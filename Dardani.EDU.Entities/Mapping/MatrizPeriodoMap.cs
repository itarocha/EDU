using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class MatrizPeriodoMap : ClassMap<MatrizPeriodo>
    {
        public MatrizPeriodoMap()
        {
            Table("EDU_MATRIZ_PERIODO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_MATRIZ_PERIODO");
            References(x => x.Matriz).Column("ID_MATRIZ").Not.Nullable();
            References(x => x.PeriodoAno).Column("ID_PERIODO_ANO").Not.Nullable();
            Map(x => x.DataInicio).Column("DT_INICIO").Not.Nullable();
            Map(x => x.DataTermino).Column("DT_TERMINO").Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class MatrizPeriodoDisciplinaMap : ClassMap<MatrizPeriodoDisciplina>
    {
        public MatrizPeriodoDisciplinaMap()
        {
            Table("EDU_MATRIZ_PERIODO_DISCIPLINA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_MATRIZ_PERIODO_DISCIPLINA");

            References(x => x.Matriz).Column("ID_MATRIZ").Not.Nullable();
            References(x => x.PeriodoAno).Column("ID_PERIODO_ANO").Not.Nullable();
            References(x => x.Disciplina).Column("ID_DISCIPLINA").Not.Nullable();
            References(x => x.ConceitoNivelMinimo).Column("ID_CONCEITO_NIVEL").Not.Nullable();

            Map(x => x.NotaMaxima).Column("VL_NOTA_MAXIMA");
            Map(x => x.NotaMinima).Column("VL_NOTA_MINIMA");
        }
    }
}

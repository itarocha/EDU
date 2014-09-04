using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class MatriculaMap : ClassMap<Matricula>
    {
        public MatriculaMap()
        {
            Table("EDU_MATRICULA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_MATRICULA");
            References(x => x.Pessoa).Column("ID_PESSOA").Not.Nullable();
            References(x => x.AnoLetivo).Column("ID_ANO_LETIVO").Not.Nullable();
            References(x => x.Turma).Column("ID_TURMA").Not.Nullable();
            References(x => x.EscolarizacaoEspecial).Column("ID_ESCOLARIZACAO_ESPECIAL").Not.Nullable();
            References(x => x.TransportePublico).Column("ID_TRANSPORTE_PUBLICO").Not.Nullable();
            References(x => x.TurmaUnificada).Column("ID_TURMA_UNIFICADA").Not.Nullable();
            Map(x => x.FlagRematricular).Column("FL_REMATRICULAR").Length(1).Not.Nullable();
        }
    }
}

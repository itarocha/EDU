using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class MatrizDisciplinaMap : ClassMap<MatrizDisciplina>
    {
        public MatrizDisciplinaMap()
        {
            Table("EDU_MATRIZ_DISCIPLINA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_MATRIZ_DISCIPLINA");
            References(x => x.Matriz).Column("ID_MATRIZ").Not.Nullable();
            References(x => x.Disciplina).Column("ID_DISCIPLINA").Not.Nullable();
            References(x => x.Conceito).Column("ID_CONCEITO");
            Map(x => x.CargaHorariaSemanal).Column("NM_CARGA_SEMANAL").Not.Nullable();
            Map(x => x.FlagTipoAvaliacao).Column("FL_TIPO_AVALIACAO").Length(1).Not.Nullable();
            Map(x => x.FlagCategoria).Column("FL_CATEGORIA").Length(1).Not.Nullable();
            Map(x => x.FlagAceitaDispensa).Column("FL_ACEITA_DISPENSA").Length(1).Not.Nullable();
            Map(x => x.FlagReprova).Column("FL_REPROVA").Length(1).Not.Nullable();
        }
    }
}

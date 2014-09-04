using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class DisciplinaMap : ClassMap<Disciplina>
    {
        public DisciplinaMap()
        {
            Table("EDU_DISCIPLINA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_DISCIPLINA");
            Map(x => x.Descricao).Column("DS_DISCIPLINA").Length(64).Not.Nullable();
            Map(x => x.DescricaoAbreviada).Column("DS_ABREVIATURA").Length(8);
            References(x => x.DisciplinaEducacenso).Column("ID_DISCIPLINA_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

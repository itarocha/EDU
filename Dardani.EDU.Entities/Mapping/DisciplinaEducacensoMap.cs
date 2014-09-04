using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class DisciplinaEducacensoMap : ClassMap<DisciplinaEducacenso>
    {
        public DisciplinaEducacensoMap()
        {
            Table("EDU_DISCIPLINA_EDUCACENSO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_DISCIPLINA_EDUCACENSO");
            Map(x => x.Descricao).Column("DS_DISCIPLINA_EDUCACENSO").Length(128).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

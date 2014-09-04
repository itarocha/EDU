using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class RecursoINEPMap : ClassMap<RecursoINEP>
    {
        public RecursoINEPMap()
        {
            Table("EDU_RECURSO_INEP");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_RECURSO_INEP");
            Map(x => x.Descricao).Column("DS_RECURSO_INEP").Length(64).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

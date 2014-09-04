using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class SituacaoCursoSuperiorMap : ClassMap<SituacaoCursoSuperior>
    {
        public SituacaoCursoSuperiorMap()
        {
            Table("EDU_SITUACAO_CURSO_SUPERIOR");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_SITUACAO_CURSO_SUPERIOR");
            Map(x => x.Descricao).Column("DS_SITUACAO_CURSO_SUPERIOR").Length(64).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

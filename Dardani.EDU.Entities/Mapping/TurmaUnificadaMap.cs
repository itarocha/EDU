using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TurmaUnificadaMap : ClassMap<TurmaUnificada>
    {
        public TurmaUnificadaMap()
        {
            Table("EDU_TURMA_UNIFICADA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TURMA_UNIFICADA");
            Map(x => x.Descricao).Column("DS_TURMA_UNIFICADA").Length(64).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagPossui).Column("FL_POSSUI").Length(1).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();

        }
    }
}

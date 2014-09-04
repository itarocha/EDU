using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class AtividadeComplementarMap : ClassMap<AtividadeComplementar>
    {
        public AtividadeComplementarMap()
        {
            Table("EDU_ATIVIDADE_COMPLEMENTAR");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ATIVIDADE_COMPLEMENTAR");
            Map(x => x.Descricao).Column("DS_ATIVIDADE_COMPLEMENTAR").Length(64).Not.Nullable();
            Map(x => x.FlagPossui).Column("FL_POSSUI").Length(1).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
            
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class SituacaoDocumentoMap : ClassMap<SituacaoDocumento>
    {
        public SituacaoDocumentoMap()
        {
            Table("EDU_SITUACAO_DOCUMENTO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_SITUACAO_DOCUMENTO");
            Map(x => x.Descricao).Column("DS_SITUACAO_DOCUMENTO").Length(64).Not.Nullable();
            Map(x => x.FlagPossui).Column("FL_POSSUI").Length(1).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

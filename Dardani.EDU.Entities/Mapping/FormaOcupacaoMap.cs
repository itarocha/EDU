using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class FormaIngressoFederalMap : ClassMap<FormaIngressoFederal>
    {
        public FormaIngressoFederalMap()
        {
            Table("EDU_FORMA_INGRESSO_FEDERAL");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_FORMA_INGRESSO_FEDERAL");
            Map(x => x.Descricao).Column("DS_FORMA_INGRESSO_FEDERAL").Length(256).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class ConceitoNivelMap : ClassMap<ConceitoNivel>
    {
        public ConceitoNivelMap()
        {
            Table("EDU_CONCEITO_NIVEL");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_CONCEITO_NIVEL");
            Map(x => x.Descricao).Column("DS_CONCEITO_NIVEL").Length(64).Not.Nullable();
            References(x => x.Conceito).Column("ID_CONCEITO").Not.Nullable();
            Map(x => x.Peso).Column("NM_PESO").Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaEtapaMap : ClassMap<EscolaEtapa>
    {
        public EscolaEtapaMap()
        {
            Table("EDU_ESCOLA_ETAPA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESCOLA_ETAPA");
            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.EtapaEscola).Column("ID_ETAPA_ESCOLA").Not.Nullable();
        }
    }
}

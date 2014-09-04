using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaCompartilhamentoMap : ClassMap<EscolaCompartilhamento>
    {
        public EscolaCompartilhamentoMap()
        {
            Table("EDU_ESCOLA_COMPARTILHAMENTO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESCOLA_COMPARTILHAMENTO");
            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.EscolaCompartilhada).Column("ID_ESCOLA_COMPARTILHADA").Not.Nullable();
        }
    }
}

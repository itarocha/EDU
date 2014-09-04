using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaModalidadeMap : ClassMap<EscolaModalidade>
    {
        public EscolaModalidadeMap()
        {
            Table("EDU_ESCOLA_MODALIDADE");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESCOLA_MODALIDADE");
            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.Modalidade).Column("ID_MODALIDADE").Not.Nullable();
        }
    }
}

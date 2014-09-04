using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class PessoaRecursoINEPMap : ClassMap<PessoaRecursoINEP>
    {
        public PessoaRecursoINEPMap()
        {
            Table("EDU_PESSOA_RECURSO_INEP");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_PESSOA_RECURSO_INEP");
            References(x => x.Pessoa).Column("ID_PESSOA").Not.Nullable();
            References(x => x.RecursoINEP).Column("ID_RECURSO_INEP").Not.Nullable();
        }
    }
}

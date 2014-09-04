using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class PessoaDisciplinaMap : ClassMap<PessoaDisciplina>
    {
        public PessoaDisciplinaMap()
        {
            Table("EDU_PESSOA_DISCIPLINA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_PESSOA_DISCIPLINA");
            References(x => x.Pessoa).Column("ID_PESSOA").Not.Nullable();
            References(x => x.Disciplina).Column("ID_DISCIPLINA").Not.Nullable();
        }
    }
}

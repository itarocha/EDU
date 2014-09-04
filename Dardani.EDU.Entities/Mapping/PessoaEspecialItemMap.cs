using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class PessoaEspecialItemMap : ClassMap<PessoaEspecialItem>
    {
        public PessoaEspecialItemMap()
        {
            Table("EDU_PESSOA_ESPECIAL_ITEM");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_PESSOA_ESPECIAL_ITEM");
            References(x => x.Pessoa).Column("ID_PESSOA").Not.Nullable();
            References(x => x.EspecialItem).Column("ID_ESPECIAL_ITEM").Not.Nullable();
        }
    }
}

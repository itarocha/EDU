using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class SexoMap : ClassMap<Sexo>
    {
        public SexoMap()
        {
            Table("EDU_SEXO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_SEXO");
            Map(x => x.Descricao).Column("DS_SEXO").Length(64).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
        }
    }
}

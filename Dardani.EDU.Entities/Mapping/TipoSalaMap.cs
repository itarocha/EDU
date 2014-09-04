using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TipoSalaMap : ClassMap<TipoSala>
    {
        public TipoSalaMap()
        {
            Table("EDU_TIPO_SALA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TIPO_SALA");
            Map(x => x.Descricao).Column("DS_TIPO_SALA").Length(64).Not.Nullable();
            Map(x => x.FlagSalaAula).Column("FL_SALA_AULA").Length(1).Not.Nullable();
        }
    }
}

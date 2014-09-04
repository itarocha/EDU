using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class SalaMap : ClassMap<Sala>
    {
        public SalaMap()
        {
            Table("EDU_SALA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_SALA");
            Map(x => x.Descricao).Column("DS_SALA").Length(32).Not.Nullable();
            Map(x => x.Metragem).Column("VL_METRAGEM").Precision(12).Scale(2).Not.Nullable();
            Map(x => x.Capacidade).Column("NM_CAPACIDADE").Not.Nullable();
            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.TipoSala).Column("ID_TIPO_SALA").Not.Nullable();
        }
    }
}

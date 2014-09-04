using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaPrivadaMantenedorMap : ClassMap<EscolaPrivadaMantenedor>
    {
        public EscolaPrivadaMantenedorMap()
        {
            Table("EDU_ESCOLA_PRIVADA_MANTENEDOR");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESCOLA_PRIVADA_MANTENEDOR");
            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.MantenedorPrivado).Column("ID_MANTENEDOR_PRIVADO").Not.Nullable();
        }
    }
}

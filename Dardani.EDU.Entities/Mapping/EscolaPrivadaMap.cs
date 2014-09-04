using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaPrivadaMap : ClassMap<EscolaPrivada>
    {
        public EscolaPrivadaMap()
        {
            Table("EDU_ESCOLA_PRIVADA");
            Id(x => x.Id).GeneratedBy.Foreign("Escola");
            HasOne(x => x.Escola).Constrained();
            References(x => x.CategoriaPrivada).Column("ID_CATEGORIA_PRIVADA").Not.Nullable();
            References(x => x.ConvenioPublico).Column("ID_CONVENIO_PUBLICO").Not.Nullable();
            Map(x => x.CNPJ).Column("DS_CNPJ").Length(12);
            Map(x => x.CNPJMantenedora).Column("DS_CNPJ_MANTENEDORA").Length(12);
        }
    }
}

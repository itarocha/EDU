using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class MatrizMap : ClassMap<Matriz>
    {
        public MatrizMap()
        {
            Table("EDU_MATRIZ");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_MATRIZ");

            References(x => x.AnoLetivo).Column("ID_ANO_LETIVO").Not.Nullable();
            References(x => x.Modalidade).Column("ID_MODALIDAE").Not.Nullable();
            References(x => x.Etapa).Column("ID_ETAPA").Not.Nullable();

            Map(x => x.DiasLetivos).Column("NM_DIAS_LETIVOS").Not.Nullable();
            Map(x => x.CargaHorariaSemanal).Column("NM_CARGA_SEMANAL").Not.Nullable();
            Map(x => x.CargaHorariaAula).Column("NM_CARGA_AULA").Not.Nullable();
            Map(x => x.NumeroSemanasLetivas).Column("NM_SEMANAS_LETIVAS").Not.Nullable();
        }
    }
}

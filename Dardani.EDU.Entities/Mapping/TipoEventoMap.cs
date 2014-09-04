using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class TipoEventoMap : ClassMap<TipoEvento>
    {
        public TipoEventoMap()
        {
            Table("EDU_TIPO_EVENTO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_TIPO_EVENTO");
            Map(x => x.Descricao).Column("DS_TIPO_EVENTO").Length(64).Not.Nullable();
            Map(x => x.Simbolo).Column("DS_SIMBOLO").Length(32).Not.Nullable();
            Map(x => x.FlagEscolar).Column("FL_ESCOLAR").Length(1).Not.Nullable();
        }
    }
}

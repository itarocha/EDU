using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EtapaMap : ClassMap<Etapa>
    {
        public EtapaMap()
        {
            Table("EDU_ETAPA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ETAPA");
            References(x => x.Modalidade).Column("ID_MODALIDAE").Not.Nullable();
            References(x => x.TipoEnsino).Column("ID_TIPO_ENSINO").Not.Nullable();
            References(x => x.Serie).Column("ID_SERIE").Not.Nullable();
            Map(x => x.Sequencia).Column("VL_SEQUENCIA").Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

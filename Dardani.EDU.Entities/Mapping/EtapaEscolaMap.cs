using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EtapaEscolaMap : ClassMap<EtapaEscola>
    {
        public EtapaEscolaMap()
        {
            Table("EDU_ETAPA_ESCOLA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ETAPA_ESCOLA");
            Map(x => x.Descricao).Column("DS_ETAPA_ESCOLA").Length(128).Not.Nullable();
            References(x => x.Modalidade).Column("ID_MODALIDAE").Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

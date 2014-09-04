using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaEquipamentoMap : ClassMap<EscolaEquipamento>
    {
        public EscolaEquipamentoMap()
        {
            Table("EDU_ESCOLA_EQUIPAMENTO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESCOLA_EQUIPAMENTO");
            References(x => x.Escola).Column("ID_ESCOLA").Not.Nullable();
            References(x => x.Equipamento).Column("ID_EQUIPAMENTO").Not.Nullable();
            Map(x => x.Quantidade).Column("VL_QUANTIDADE").Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class ModalidadeMap : ClassMap<Modalidade>
    {
        public ModalidadeMap()
        {
            Table("EDU_MODALIDADE");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_MODALIDADE");
            Map(x => x.Descricao).Column("DS_MODALIDADE").Length(128).Not.Nullable();
            Map(x => x.Abreviatura).Column("DS_ABREVIATURA").Length(8).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

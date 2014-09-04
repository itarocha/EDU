using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class MunicipioMap : ClassMap<Municipio>
    {
        public MunicipioMap()
        {
            Table("EDU_MUNICIPIO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_MUNICIPIO");
            References(x => x.Estado).Column("ID_ESTADO").Not.Nullable();
            Map(x => x.Descricao).Column("DS_MUNICIPIO").Length(128).Not.Nullable();
            Map(x => x.ValorEducacenso).Column("VL_EDUCACENSO").Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

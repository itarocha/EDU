using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class FuncaoDocenteMap : ClassMap<FuncaoDocente>
    {
        public FuncaoDocenteMap()
        {
            Table("EDU_FUNCAO_DOCENTE");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_FUNCAO_DOCENTE");
            Map(x => x.Descricao).Column("DS_FUNCAO_DOCENTE").Length(64).Not.Nullable();
            Map(x => x.FlagPadrao).Column("FL_PADRAO").Length(1).Not.Nullable();
            Map(x => x.FlagAtivo).Column("FL_ATIVO").Length(1).Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class MatriculaVeiculoTipoMap : ClassMap<MatriculaVeiculoTipo>
    {
        public MatriculaVeiculoTipoMap()
        {
            Table("EDU_MATRICULA_VEICULO_TIPO");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_MATRICULA_VEICULO_TIPO");
            References(x => x.Matricula).Column("ID_MATRICULA").Not.Nullable();
            References(x => x.VeiculoTipo).Column("ID_VEICULO_TIPO").Not.Nullable();
        }
    }
}

using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaComputadorMap : ClassMap<EscolaComputador>
    {
        public EscolaComputadorMap()
        {
            Table("EDU_ESCOLA_COMPUTADOR");
            Id(x => x.Id).GeneratedBy.Foreign("Escola");
            HasOne(x => x.Escola).Constrained();
            Map(x => x.QuantidadeUsoAdministrativo).Column("VL_QUANTIDADE_USO_ADMINISTRATIVO").Not.Nullable();
            Map(x => x.QuantidadeUsoAluno).Column("VL_QUANTIDADE_USO_ALUNO").Not.Nullable();
            Map(x => x.FlagAcessoInternet).Column("FL_ACESSO_INTERNET").Length(1).Not.Nullable();
            Map(x => x.FlagBandaLarga).Column("FL_BANDA_LARGA").Length(1).Not.Nullable();
        }
    }
}

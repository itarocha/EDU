using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class EscolaMap : ClassMap<Escola>
    {
        public EscolaMap()
        {
            Table("EDU_ESCOLA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_ESCOLA");
            Map(x => x.Nome).Column("DS_NOME").Length(128).Not.Nullable();
            References(x => x.SituacaoFuncionamento).Column("ID_SITUACAO_FUNCIONAMENTO").Not.Nullable();
            Map(x => x.CodigoINEP).Column("DS_CODIGO_INEP").Length(32).Not.Nullable();
            Map(x => x.NomeGestor).Column("DS_NOME_GESTOR").Length(64).Not.Nullable();
            Map(x => x.FlagGestorDiretor).Column("FL_GESTOR_DIRETOR").Length(1).Not.Nullable();
            Map(x => x.Email).Column("DS_EMAIL").Length(64);
            Map(x => x.CodigoOrgaoRegional).Column("VL_CODIGO_REGIONAL").Length(8);
            Map(x => x.NomeOrgaoRegional).Column("DS_CODIGO_REGIONAL").Length(128);
            References(x => x.TipoAdministracao).Column("ID_TIPO_ADMINISTRACAO").Not.Nullable();
            References(x => x.EstagioRegulamentacao).Column("ID_ESTAGIO_REGULAMENTACAO").Not.Nullable();
            Map(x => x.QuantidadeFuncionarios).Column("VL_QUANTIDADE_FUNCIONARIOS").Not.Nullable();
            Map(x => x.FlagAlimentacaoEscolar).Column("FL_ALIMENTACAO_ESCOLAR").Length(1).Not.Nullable();
        }
    }
     
}

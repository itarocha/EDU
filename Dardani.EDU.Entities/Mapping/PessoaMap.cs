using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class PessoaMap : ClassMap<Pessoa>
    {
        public PessoaMap()
        {
            Table("EDU_PESSOA");
            Id(x => x.Id).GeneratedBy.Native().Column("ID_PESSOA");
            Map(x => x.Nome).Column("DS_NOME").Length(64).Not.Nullable();
            Map(x => x.DataNascimento).Column("DT_NASCIMENTO").Not.Nullable();
            References(x => x.Sexo).Column("ID_SEXO").Not.Nullable();
            References(x => x.Raca).Column("ID_RACA").Not.Nullable();
            
            Map(x => x.NumeroNIS).Column("DS_NUMERO_NIS").Length(11);
            Map(x => x.CodigoINEP).Column("DS_CODIGO_INEP").Length(32);
            Map(x => x.NomeMae).Column("DS_NOME_MAE").Length(64).Not.Nullable();
            Map(x => x.NomePai).Column("DS_NOME_PAI").Length(64);
            Map(x => x.NomeResponsavel).Column("DS_NOME_RESPONSAVEL").Length(64);
            Map(x => x.EmailResponsavel).Column("DS_EMAIL_RESPONSAVEL").Length(64);

            References(x => x.EstadoCivil).Column("ID_ESTADO_CIVIL");
            
            Map(x => x.NomeConjuge).Column("DS_NOME_CONJUGE").Length(64);
            References(x => x.TipoNacionalidade).Column("ID_TIPO_NACIONALIDADE").Not.Nullable();
            References(x => x.PaisOrigem).Column("ID_PAIS");
            References(x => x.UFNascimento).Column("ID_ESTADO");
            References(x => x.CidadeNascimento).Column("ID_MUNICIPIO");
            Map(x => x.FlagDeficiencia).Column("FL_DEFICIENCIA").Length(1).Not.Nullable();
            Map(x => x.FlagTipoPessoa).Column("FL_TIPO_PESSOA").Length(1).Not.Nullable();

            HasOne(x => x.Endereco);
            HasOne(x => x.Documentacao);

            
        }
    }
     
}

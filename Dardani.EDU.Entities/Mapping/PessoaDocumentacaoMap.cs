using Dardani.EDU.Entities.Model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.Entities.Mapping
{
    public class PessoaDocumentacaoMap : ClassMap<PessoaDocumentacao>
    {
        public PessoaDocumentacaoMap()
        {
            Table("EDU_PESSOA_DOCUMENTACAO");
            Id(x => x.Id).GeneratedBy.Foreign("Pessoa");
            HasOne(x => x.Pessoa).Constrained();

            References(x => x.SituacaoDocumento).Column("ID_SITUACAO_DOCUMENTO"); //.Not.Nullable();
            References(x => x.ModeloCertidao).Column("ID_MODELO_CERTIDAO"); //.Not.Nullable();
            References(x => x.TipoCertidao).Column("ID_TIPO_CERTIDAO"); //.Not.Nullable();

            Map(x => x.CertidaoNumero).Column("DS_CERTIDAO_NUMERO").Length(16);
            Map(x => x.CertidaoTermo).Column("DS_CERTIDAO_TERMO").Length(16);
            Map(x => x.CertidaoFolha).Column("DS_CERTIDAO_FOLHA").Length(16);
            Map(x => x.CertidaoLivro).Column("DS_CERTIDAO_LIVRO").Length(16);
            Map(x => x.CertidaoNomeCartorio).Column("DS_CERTIDAO_CARTORIO").Length(64);
            Map(x => x.CertidaoDataEmissao).Column("DT_CERTIDAO_EMISSAO");
            References(x => x.CertidaoCidade).Column("ID_CERTIDAO_MUNICIPIO"); //.Not.Nullable();
            References(x => x.CertidaoUF).Column("ID_CERTIDAO_ESTADO"); //.Not.Nullable();
            Map(x => x.CertidaoNumeroNovo).Column("DS_CERTIDAO_NUMERO_NOVO").Length(32);

            Map(x => x.RGNumero).Column("DS_RG_NUMERO").Length(32);
            Map(x => x.RGComplemento).Column("DS_RG_COMPLEMENTO").Length(16);
            Map(x => x.RGOrgao).Column("DS_RG_ORGAO").Length(16);
            Map(x => x.RGDataEmissao).Column("DT_RG_EMISSAO");
            References(x => x.RGUF).Column("ID_RG_ESTADO");

            Map(x => x.CPFNumero).Column("DS_CPF_NUMERO").Length(11);
            Map(x => x.DocumentoEstrangeiroNumero).Column("DS_DOCUMENTO_ESTRANGEIRO_NUMERO").Length(32);
            Map(x => x.CNHNumero).Column("DS_CNH_NUMERO").Length(32);
            Map(x => x.CNHCategoria).Column("DS_CNH_CATEGORIA").Length(8);
            Map(x => x.CNHDataEmissao).Column("DT_CNH_EMISSAO");
            Map(x => x.CNHDataValidade).Column("DT_CNH_VALIDADE");
            References(x => x.CNHUF).Column("ID_CNH_ESTADO");
        }
    }
}

using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Transform;
using Dardani.EDU.Entities.VO;

namespace Dardani.EDU.BO.NH
{
    public class PessoaDocumentacaoDAO : GenericDAO<PessoaDocumentacao>
    {
        public PessoaDocumentacaoVO GetPessoaDocumentacaoVOById(int id)
        {
        	PessoaDocumentacaoVO model = Session.CreateQuery(
			"SELECT "+
            "tb.Id as Id, "+
            "tb.Id as PessoaId, "+
            "mc.Id as ModeloCertidaoId, "+
            "mc.Descricao as ModeloCertidaoDescricao, "+
            "sd.Id as SituacaoDocumentoId, "+
            "sd.Descricao as SituacaoDocumentoDescricao, "+
            "tc.Id as TipoCertidaoId, "+
            "tc.Descricao as TipoCertidaoDescricao, "+
            "tb.CertidaoNumero  as CertidaoNumero, "+
            "tb.CertidaoTermo as CertidaoTermo, "+
            "tb.CertidaoFolha as CertidaoFolha, "+
            "tb.CertidaoLivro as CertidaoLivro, "+
            "tb.CertidaoNomeCartorio as CertidaoNomeCartorio, "+
            "tb.CertidaoDataEmissao as CertidaoDataEmissao, "+
            "cc.Id as CertidaoCidadeId, "+
            "cuf.Id as CertidaoUFId, "+
            "tb.CertidaoNumeroNovo as CertidaoNumeroNovo, "+
            "tb.RGNumero as RGNumero, "+
            "tb.RGComplemento as RGComplemento, "+
            "tb.RGOrgao as RGOrgao, "+
            "tb.RGDataEmissao as RGDataEmissao, "+
            "rguf.Id as RGUFId, "+
            "tb.CPFNumero as CPFNumero, "+
            "tb.DocumentoEstrangeiroNumero as DocumentoEstrangeiroNumero, "+
            "tb.CNHNumero as CNHNumero, "+
            "tb.CNHCategoria as CNHCategoria, "+
            "tb.CNHDataEmissao as CNHDataEmissao, "+
            "tb.CNHDataValidade as CNHDataValidade, "+
            "cnhuf.Id as CNHUFId "+
            "FROM PessoaDocumentacao as tb "+
            "LEFT JOIN tb.SituacaoDocumento as sd "+
            "LEFT JOIN tb.ModeloCertidao as mc "+
            "LEFT JOIN tb.TipoCertidao as tc "+
            "LEFT JOIN tb.CertidaoCidade as cc "+
            "LEFT JOIN tb.CertidaoUF as cuf "+
            "LEFT JOIN tb.RGUF as rguf "+
            "LEFT JOIN tb.CNHUF as cnhuf "+
            "WHERE tb.Id = :id"
        	).SetParameter("id",id)
        	.SetResultTransformer(Transformers.AliasToBean(typeof(PessoaDocumentacaoVO)))
        	.UniqueResult<PessoaDocumentacaoVO>();
        	return model;
        	
        	/*
            PessoaDocumentacaoVO avo = null;
            PessoaDocumentacao a = null;
            SituacaoDocumento sd = null;
            ModeloCertidao mc = null;
            TipoCertidao tc = null;
            Municipio cc = null;

            Estado cuf = null;
            Estado rguf = null;
            Estado cnhuf = null;

            PessoaDocumentacaoVO model =
            Session.QueryOver<PessoaDocumentacao>(() => a)
                .Left.JoinQueryOver<SituacaoDocumento>(() => a.SituacaoDocumento, () => sd)
                .Left.JoinQueryOver<ModeloCertidao>(() => a.ModeloCertidao, () => mc)
                .Left.JoinQueryOver<TipoCertidao>(() => a.TipoCertidao, () => tc)
                .Left.JoinQueryOver<Municipio>(() => a.CertidaoCidade, () => cc)
                .Left.JoinQueryOver<Estado>(() => a.CertidaoUF, () => cuf)
                .Left.JoinQueryOver<Estado>(() => a.RGUF, () => rguf)
                .Left.JoinQueryOver<Estado>(() => a.CNHUF, () => cnhuf)

                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => a.Id).WithAlias(() => avo.PessoaId)

                    .Select(() => mc.Id).WithAlias(() => avo.ModeloCertidaoId)
                    .Select(() => mc.Descricao).WithAlias(() => avo.ModeloCertidaoDescricao)

                    .Select(() => sd.Id).WithAlias(() => avo.SituacaoDocumentoId)
                    .Select(() => sd.Descricao).WithAlias(() => avo.SituacaoDocumentoDescricao)

                    .Select(() => tc.Id).WithAlias(() => avo.TipoCertidaoId)
                    .Select(() => tc.Descricao).WithAlias(() => avo.TipoCertidaoDescricao)

                    .Select(() => a.CertidaoNumero ).WithAlias(() => avo.CertidaoNumero)
                    .Select(() => a.CertidaoTermo).WithAlias(() => avo.CertidaoTermo)
                    .Select(() => a.CertidaoFolha).WithAlias(() => avo.CertidaoFolha)
                    .Select(() => a.CertidaoLivro).WithAlias(() => avo.CertidaoLivro)
                    .Select(() => a.CertidaoNomeCartorio).WithAlias(() => avo.CertidaoNomeCartorio)
                    .Select(() => a.CertidaoDataEmissao).WithAlias(() => avo.CertidaoDataEmissao)
                    .Select(() => cc.Id).WithAlias(() => avo.CertidaoCidadeId)
                    .Select(() => cuf.Id).WithAlias(() => avo.CertidaoUFId)
                    .Select(() => a.CertidaoNumeroNovo).WithAlias(() => avo.CertidaoNumeroNovo)
                    
                    .Select(() => a.RGNumero).WithAlias(() => avo.RGNumero)
                    .Select(() => a.RGComplemento).WithAlias(() => avo.RGComplemento)
                    .Select(() => a.RGOrgao).WithAlias(() => avo.RGOrgao)
                    .Select(() => a.RGDataEmissao).WithAlias(() => avo.RGDataEmissao)
                    .Select(() => rguf.Id).WithAlias(() => avo.RGUFId)

                    .Select(() => a.CPFNumero).WithAlias(() => avo.CPFNumero)
                    .Select(() => a.DocumentoEstrangeiroNumero).WithAlias(() => avo.DocumentoEstrangeiroNumero)
                    .Select(() => a.CNHNumero).WithAlias(() => avo.CNHNumero)
                    .Select(() => a.CNHCategoria).WithAlias(() => avo.CNHCategoria)
                    .Select(() => a.CNHDataEmissao).WithAlias(() => avo.CNHDataEmissao)
                    .Select(() => a.CNHDataValidade).WithAlias(() => avo.CNHDataValidade)
                    .Select(() => cnhuf.Id).WithAlias(() => avo.CNHUFId)
                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<PessoaDocumentacaoVO>())
                .SingleOrDefault<PessoaDocumentacaoVO>();

            return model;
            */
        }
    } // END CLASS
} // END NAMESPACE

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
    public class PessoaEnderecoDAO : GenericDAO<PessoaEndereco>
    {
        public PessoaEnderecoVO GetPessoaEnderecoVOById(int id)
        {
        	PessoaEnderecoVO model = Session.CreateQuery(
        		"SELECT "+
                "tb.Id as Id, "+
                "tb.Id as PessoaId, "+
                "tb.Logradouro  as Logradouro, "+
                "tb.Numero as Numero, "+
                "tb.Complemento as Complemento, "+
                "tb.Bairro as Bairro, "+
                "tb.CEP as CEP, "+
                "cdd.Id as CidadeId, "+
                "uf.Id as UFId, "+
                "z.Id as ZonaId, "+
                "z.Descricao as ZonaDescricao, "+
                "tb.Telefone as Telefone, "+
                "tb.Fax as Fax, "+
                "tb.Email as Email "+
        		"FROM PessoaEndereco tb "+
                "LEFT JOIN tb.Zona z "+
                "LEFT JOIN tb.UF uf "+
        		"LEFT JOIN tb.Cidade cdd "+
        	    "WHERE tb.Id = :id")
                .SetParameter("id", id)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(PessoaEnderecoVO)))
        		.UniqueResult<PessoaEnderecoVO>();
        	
        	return model;
        	
        	/*
            PessoaEnderecoVO avo = null;
            PessoaEndereco a = null;
            Estado uf = null;
            Municipio cdd = null;
            Zona z = null;

            PessoaEnderecoVO model =
            Session.QueryOver<PessoaEndereco>(() => a)
                .Left.JoinQueryOver<Zona>(() => a.Zona, () => z)
                .Left.JoinQueryOver<Estado>(() => a.UF, () => uf)
                .Left.JoinQueryOver<Municipio>(() => a.Cidade, () => cdd)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => a.Id).WithAlias(() => avo.PessoaId)
                    .Select(() => a.Logradouro ).WithAlias(() => avo.Logradouro)
                    .Select(() => a.Numero).WithAlias(() => avo.Numero)
                    .Select(() => a.Complemento).WithAlias(() => avo.Complemento)
                    .Select(() => a.Bairro).WithAlias(() => avo.Bairro)
                    .Select(() => a.CEP).WithAlias(() => avo.CEP)
                    .Select(() => cdd.Id).WithAlias(() => avo.CidadeId)
                    .Select(() => uf.Id).WithAlias(() => avo.UFId)
                    .Select(() => z.Id).WithAlias(() => avo.ZonaId)
                    .Select(() => z.Descricao).WithAlias(() => avo.ZonaDescricao)
                    .Select(() => a.Telefone).WithAlias(() => avo.Telefone)
                    .Select(() => a.Fax).WithAlias(() => avo.Fax)
                    .Select(() => a.Email).WithAlias(() => avo.Email)
                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<PessoaEnderecoVO>())
                .SingleOrDefault<PessoaEnderecoVO>();

            return model;
            */
        }


    } // END CLASS
} // END NAMESPACE

using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dardani.EDU.Entities.VO;
using NHibernate.Transform;

namespace Dardani.EDU.BO.NH
{
    public class EscolaEnderecoDAO : GenericDAO<EscolaEndereco>
    {
        public EscolaEnderecoVO GetEscolaEnderecoVOById(int id)
        {
            EscolaEnderecoVO model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, "+
                    "tb.Id as EscolaId, "+
                    "tb.Logradouro as Logradouro, "+
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
                    "tb.Email as Email, "+
                    "tb.Latitude as Latitude, "+
                    "tb.Longitude as Longitude "+
                    "FROM EscolaEndereco tb " +
                    "LEFT JOIN tb.Zona z " +
                    "LEFT JOIN tb.UF uf " +
                    "LEFT JOIN tb.Cidade cdd " +
                    "WHERE tb.Id = :id "
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaEnderecoVO)))
                .UniqueResult<EscolaEnderecoVO>();

            return model;
            /*
            EscolaEnderecoVO avo = null;
            EscolaEndereco a = null;
            Estado uf = null;
            Municipio cdd = null;
            Zona z = null;

            EscolaEnderecoVO model =
            Session.QueryOver<EscolaEndereco>(() => a)
                .Left.JoinQueryOver<Zona>(() => a.Zona, () => z)
                .Left.JoinQueryOver<Estado>(() => a.UF, () => uf)
                .Left.JoinQueryOver<Municipio>(() => a.Cidade, () => cdd)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => a.Id).WithAlias(() => avo.EscolaId)
                    .Select(() => a.Logradouro).WithAlias(() => avo.Logradouro)
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

                    .Select(() => a.Latitude).WithAlias(() => avo.Latitude)
                    .Select(() => a.Longitude).WithAlias(() => avo.Longitude)

                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<EscolaEnderecoVO>())
                .SingleOrDefault<EscolaEnderecoVO>();

            return model;
            */
        }

    } // END CLASS
} // END NAMESPACE

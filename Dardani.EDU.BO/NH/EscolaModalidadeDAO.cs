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
    public class EscolaInfraestruturaItemDAO : GenericDAO<EscolaInfraestruturaItem>
    {

        public IEnumerable<EscolaInfraestruturaItemVO> GetListaEscolaInfraestruturaItemVO(int id)
        {
            IEnumerable<EscolaInfraestruturaItemVO> model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, "+
                    "ie.Id as InfraestruturaItemId, "+
                    "ie.Descricao as InfraestruturaItemDescricao, "+
                    "e.Id as EscolaId " +
                    "FROM EscolaInfraestruturaItem tb " +
                    "INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.InfraestruturaItem ie " +
                    "WHERE e.Id = :id " +
                    "ORDER BY ie.Descricao"
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaInfraestruturaItemVO)))
                .List<EscolaInfraestruturaItemVO>();

            return model;
            /*
            EscolaInfraestruturaItemVO avo = null;
            EscolaInfraestruturaItem a = null;
            Escola e = null;
            InfraestruturaItem ie = null;

            var _qlista =

            Session.QueryOver<EscolaInfraestruturaItem>(() => a)
                .Inner.JoinQueryOver<Escola>(() => a.Escola, () => e)
                .Inner.JoinQueryOver<InfraestruturaItem>(() => a.InfraestruturaItem, () => ie)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => ie.Id).WithAlias(() => avo.InfraestruturaItemId)
                    .Select(() => ie.Descricao).WithAlias(() => avo.InfraestruturaItemDescricao)
                    .Select(() => e.Id).WithAlias(() => avo.EscolaId)
                ).Where(() => e.Id == id);

            IEnumerable<EscolaInfraestruturaItemVO> retorno =
               _qlista
               .TransformUsing(Transformers.AliasToBean<EscolaInfraestruturaItemVO>())
               .List<EscolaInfraestruturaItemVO>()
               .OrderBy(x => x.InfraestruturaItemDescricao);
            return retorno;
             */ 
        }

        public EscolaInfraestruturaItemVO GetEscolaInfraestruturaItemVOById(int id)
        {

            EscolaInfraestruturaItemVO model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, " +
                    "ie.Id as InfraestruturaItemId, " +
                    "ie.Descricao as InfraestruturaItemDescricao, " +
                    "e.Id as EscolaId " +
                    "FROM EscolaInfraestruturaItem tb " +
                    "INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.InfraestruturaItem ie " +
                    "WHERE tb.Id = :id "
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaInfraestruturaItemVO)))
                .UniqueResult<EscolaInfraestruturaItemVO>();

            return model;

            /*
            EscolaInfraestruturaItemVO avo = null;
            EscolaInfraestruturaItem a = null;
            Escola e = null;
            InfraestruturaItem ie = null;

            EscolaInfraestruturaItemVO model =
            Session.QueryOver<EscolaInfraestruturaItem>(() => a)
                .Inner.JoinQueryOver<Escola>(() => a.Escola, () => e)
                .Inner.JoinQueryOver<InfraestruturaItem>(() => a.InfraestruturaItem, () => ie)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => ie.Id).WithAlias(() => avo.InfraestruturaItemId)
                    .Select(() => ie.Descricao).WithAlias(() => avo.InfraestruturaItemDescricao)
                    .Select(() => e.Id).WithAlias(() => avo.EscolaId)
                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<EscolaInfraestruturaItemVO>())
                .SingleOrDefault<EscolaInfraestruturaItemVO>();
            return model;
             */ 
        }

    } // END CLASS
} // END NAMESPACE

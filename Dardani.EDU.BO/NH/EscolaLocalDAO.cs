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
    public class EscolaLocalDAO : GenericDAO<EscolaLocal>
    {

        public IEnumerable<EscolaLocalVO> GetListaEscolaLocalVO(int id)
        {
            IEnumerable<EscolaLocalVO> model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, "+
                    "le.Id as LocalEscolaId, "+
                    "le.Descricao as LocalEscolaDescricao, "+
                    "e.Id as EscolaId " +
                    "FROM EscolaLocal tb " +
                    "INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.LocalEscola le " +
                    "WHERE e.Id = :id " +
                    "ORDER BY eq.Descricao"
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaLocalVO)))
                .List<EscolaLocalVO>();

            return model;

            /*
            EscolaLocalVO avo = null;
            EscolaLocal a = null;
            Escola e = null;
            LocalEscola le = null;

            var _qlista =

            Session.QueryOver<EscolaLocal>(() => a)
                .Inner.JoinQueryOver<Escola>(() => a.Escola, () => e)
                .Inner.JoinQueryOver<LocalEscola>(() => a.LocalEscola, () => le)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => le.Id).WithAlias(() => avo.LocalEscolaId)
                    .Select(() => le.Descricao).WithAlias(() => avo.LocalEscolaDescricao)
                    .Select(() => e.Id).WithAlias(() => avo.EscolaId)
                ).Where(() => e.Id == id);

            IEnumerable<EscolaLocalVO> retorno =
               _qlista
               .TransformUsing(Transformers.AliasToBean<EscolaLocalVO>())
               .List<EscolaLocalVO>()
               .OrderBy(x => x.LocalEscolaDescricao);
            return retorno;
             */ 
        }

        public EscolaLocalVO GetEscolaLocalVOById(int id)
        {
            EscolaLocalVO model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, " +
                    "le.Id as LocalEscolaId, " +
                    "le.Descricao as LocalEscolaDescricao, " +
                    "e.Id as EscolaId " +
                    "FROM EscolaLocal tb " +
                    "INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.LocalEscola le " +
                    "WHERE tb.Id = :id "
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaLocalVO)))
                .UniqueResult<EscolaLocalVO>();

            return model;


            /*
            EscolaLocalVO avo = null;
            EscolaLocal a = null;
            Escola e = null;
            LocalEscola le = null;

            EscolaLocalVO model =
            Session.QueryOver<EscolaLocal>(() => a)
                .Inner.JoinQueryOver<Escola>(() => a.Escola, () => e)
                .Inner.JoinQueryOver<LocalEscola>(() => a.LocalEscola, () => le)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => le.Id).WithAlias(() => avo.LocalEscolaId)
                    .Select(() => le.Descricao).WithAlias(() => avo.LocalEscolaDescricao)
                    .Select(() => e.Id).WithAlias(() => avo.EscolaId)
                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<EscolaLocalVO>())
                .SingleOrDefault<EscolaLocalVO>();
            return model;
             */ 
        }

    } // END CLASS
} // END NAMESPACE

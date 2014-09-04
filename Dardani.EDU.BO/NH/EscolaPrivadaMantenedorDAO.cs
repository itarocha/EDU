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
    public class EscolaPrivadaMantenedorDAO : GenericDAO<EscolaPrivadaMantenedor>
    {

        public IEnumerable<EscolaPrivadaMantenedorVO> GetListaEscolaPrivadaMantenedorVO(int id)
        {
            IEnumerable<EscolaPrivadaMantenedorVO> model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, " +
                    "mp.Id as MantenedorPrivadoId, " +
                    "mp.Descricao as MantenedorPrivadoDescricao, " +
                    "e.Id as EscolaId " +
                    "FROM EscolaPrivadaMantenedor tb " +
                    "INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.MantenedorPrivado mp " +
                    "WHERE e.Id = :id " +
                    "ORDER BY mp.Descricao"
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaPrivadaMantenedorVO)))
                .List<EscolaPrivadaMantenedorVO>();

            return model;

            /*
            EscolaPrivadaMantenedorVO avo = null;
            EscolaPrivadaMantenedor a = null;
            Escola e = null;
            MantenedorPrivado mp = null;

            var _qlista =

            Session.QueryOver<EscolaPrivadaMantenedor>(() => a)
                .Inner.JoinQueryOver<Escola>(() => a.Escola, () => e)
                .Inner.JoinQueryOver<MantenedorPrivado>(() => a.MantenedorPrivado, () => mp)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => mp.Id).WithAlias(() => avo.MantenedorPrivadoId)
                    .Select(() => mp.Descricao).WithAlias(() => avo.MantenedorPrivadoDescricao)
                    .Select(() => e.Id).WithAlias(() => avo.EscolaId)
                ).Where(() => e.Id == id);

            IEnumerable<EscolaPrivadaMantenedorVO> retorno =
               _qlista
               .TransformUsing(Transformers.AliasToBean<EscolaPrivadaMantenedorVO>())
               .List<EscolaPrivadaMantenedorVO>()
               .OrderBy(x => x.MantenedorPrivadoDescricao);
            return retorno;
             */ 
        }

        public EscolaPrivadaMantenedorVO GetEscolaPrivadaMantenedorVOById(int id)
        {

            EscolaPrivadaMantenedorVO model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, " +
                    "mp.Id as MantenedorPrivadoId, " +
                    "mp.Descricao as MantenedorPrivadoDescricao, " +
                    "e.Id as EscolaId " +
                    "FROM EscolaPrivadaMantenedor tb " +
                    "INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.MantenedorPrivado mp " +
                    "WHERE tb.Id = :id " 
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaPrivadaMantenedorVO)))
                .UniqueResult<EscolaPrivadaMantenedorVO>();

            return model;


            /*
            EscolaPrivadaMantenedorVO avo = null;
            EscolaPrivadaMantenedor a = null;
            Escola e = null;
            MantenedorPrivado mp = null;

            EscolaPrivadaMantenedorVO model =
            Session.QueryOver<EscolaPrivadaMantenedor>(() => a)
                .Inner.JoinQueryOver<Escola>(() => a.Escola, () => e)
                .Inner.JoinQueryOver<MantenedorPrivado>(() => a.MantenedorPrivado, () => mp)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => mp.Id).WithAlias(() => avo.MantenedorPrivadoId)
                    .Select(() => mp.Descricao).WithAlias(() => avo.MantenedorPrivadoDescricao)
                    .Select(() => e.Id).WithAlias(() => avo.EscolaId)
                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<EscolaPrivadaMantenedorVO>())
                .SingleOrDefault<EscolaPrivadaMantenedorVO>();
            return model;
             */ 
        }

    } // END CLASS
} // END NAMESPACE

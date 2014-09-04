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
    public class EscolaPrivadaDAO : GenericDAO<EscolaPrivada>
    {

        public EscolaPrivadaVO GetEscolaPrivadaVOById(int id)
        {

            EscolaPrivadaVO model =
                Session.CreateQuery("SELECT " +

                    "tb.Id as Id, "+
                    "tb.Id as EscolaId, "+
                    "pv.Id as CategoriaPrivadaId, "+
                    "pv.Descricao as CategoriaPrivadaDescricao, "+
                    "cnv.Id as ConvenioPublicoId, "+
                    "cnv.Descricao as ConvenioPublicoDescricao, "+
                    "tb.CNPJ as CNPJ, "+
                    "tb.CNPJMantenedora as CNPJMantenedora " +
                    "FROM EscolaPrivada tb " +
                    "LEFT JOIN tb.CategoriaPrivada pv " +
                    "LEFT JOIN tb.ConvenioPublico cnv " +
                    "WHERE tb.Id = :id "
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaPrivadaVO)))
                .UniqueResult<EscolaPrivadaVO>();

            return model;
            /*
            EscolaPrivadaVO avo = null;
            EscolaPrivada a = null;
            CategoriaPrivada pv = null;
            ConvenioPublico cnv = null;

            EscolaPrivadaVO model =
            Session.QueryOver<EscolaPrivada>(() => a)
                .Left.JoinQueryOver<CategoriaPrivada>(() => a.CategoriaPrivada, () => pv)
                .Left.JoinQueryOver<ConvenioPublico>(() => a.ConvenioPublico, () => cnv)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => a.Id).WithAlias(() => avo.EscolaId)
                    .Select(() => pv.Id).WithAlias(() => avo.CategoriaPrivadaId)
                    .Select(() => pv.Descricao).WithAlias(() => avo.CategoriaPrivadaDescricao)
                    .Select(() => cnv.Id).WithAlias(() => avo.ConvenioPublicoId)
                    .Select(() => cnv.Descricao).WithAlias(() => avo.ConvenioPublicoDescricao)
                    .Select(() => a.CNPJ).WithAlias(() => avo.CNPJ)
                    .Select(() => a.CNPJMantenedora).WithAlias(() => avo.CNPJMantenedora)
                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<EscolaPrivadaVO>())
                .SingleOrDefault<EscolaPrivadaVO>();

            return model;
            */
        }

    } // END CLASS
} // END NAMESPACE

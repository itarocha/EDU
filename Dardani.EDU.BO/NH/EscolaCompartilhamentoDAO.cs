using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
//using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dardani.EDU.Entities.VO;
using NHibernate.Transform;

namespace Dardani.EDU.BO.NH
{
    public class EscolaCompartilhamentoDAO : GenericDAO<EscolaCompartilhamento>
    {

        public IEnumerable<EscolaCompartilhamentoVO> GetListaEscolaCompartilhamentoVO(int id)
        {
            IEnumerable<EscolaCompartilhamentoVO> model = 
                Session.CreateQuery("SELECT "+
                    "tb.Id as Id, "+
                    "ec.Id as EscolaCompartilhadaId, "+
                    "ec.Nome as EscolaCompartilhadaNome, "+
                    "e.Id as EscolaId "+
                    "FROM EscolaCompartilhamento tb "+
                    "INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.EscolaCompartilhada ec " +
                    "WHERE e.Id = :id " +
                    "ORDER BY ec.Nome"
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaCompartilhamentoVO)))
                .List<EscolaCompartilhamentoVO>();

            return model;

/*
            EscolaCompartilhamentoVO avo = null;
            EscolaCompartilhamento a = null;
            Escola e = null;
            Escola ec = null;

            var _qlista =

            Session.QueryOver<EscolaCompartilhamento>(() => a)
                .Inner.JoinQueryOver<Escola>(() => a.Escola, () => e)
                .Inner.JoinQueryOver<Escola>(() => a.EscolaCompartilhada, () => ec)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => ec.Id).WithAlias(() => avo.EscolaCompartilhadaId)
                    .Select(() => ec.Nome).WithAlias(() => avo.EscolaCompartilhadaNome)
                    .Select(() => e.Id).WithAlias(() => avo.EscolaId)
                ).Where(() => e.Id == id);

            IEnumerable<EscolaCompartilhamentoVO> retorno =
               _qlista
               .TransformUsing(Transformers.AliasToBean<EscolaCompartilhamentoVO>())
               .List<EscolaCompartilhamentoVO>()
               .OrderBy(x => x.EscolaCompartilhadaNome);
            return retorno;
  */          
        }

        public EscolaCompartilhamentoVO GetEscolaCompartilhamentoVOById(int id)
        {

            EscolaCompartilhamentoVO model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, " +
                    "ec.Id as EscolaCompartilhadaId, " +
                    "ec.Nome as EscolaCompartilhadaNome, " +
                    "e.Id as EscolaId " +
                    "FROM EscolaCompartilhamento tb " +
                    "INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.EscolaCompartilhada ec " +
                    "WHERE tb.Id = :id " +
                    "ORDER BY ec.Nome"
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaCompartilhamentoVO)))
                .UniqueResult<EscolaCompartilhamentoVO>();

            return model;
            
            /*
            EscolaCompartilhamentoVO avo = null;
            EscolaCompartilhamento a = null;
            Escola e = null;
            Escola ec = null;

            EscolaCompartilhamentoVO model =
            Session.QueryOver<EscolaCompartilhamento>(() => a)
                .Inner.JoinQueryOver<Escola>(() => a.Escola, () => e)
                .Inner.JoinQueryOver<Escola>(() => a.EscolaCompartilhada, () => ec)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => ec.Id).WithAlias(() => avo.EscolaCompartilhadaId)
                    .Select(() => ec.Nome).WithAlias(() => avo.EscolaCompartilhadaNome)
                    .Select(() => e.Id).WithAlias(() => avo.EscolaId)
                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<EscolaCompartilhamentoVO>())
                .SingleOrDefault<EscolaCompartilhamentoVO>();

            return model;
             */ 
        }

    } // END CLASS
} // END NAMESPACE

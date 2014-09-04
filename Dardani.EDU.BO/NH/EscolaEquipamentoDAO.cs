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
    public class EscolaEquipamentoDAO : GenericDAO<EscolaEquipamento>
    {

        public IEnumerable<EscolaEquipamentoVO> GetListaEscolaEquipamentoVO(int id)
        {
            IEnumerable<EscolaEquipamentoVO> model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, "+
                    "eq.Id as EquipamentoId, "+
                    "eq.Descricao as EquipamentoDescricao, "+
                    "e.Id as EscolaId, "+
                    "tb.Quantidade as Quantidade "+
                    "FROM EscolaEquipamento tb " +
                    "INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.Equipamento eq " +
                    "WHERE e.Id = :id " +
                    "ORDER BY eq.Descricao"
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaEquipamentoVO)))
                .List<EscolaEquipamentoVO>();

            return model;

            /*
            EscolaEquipamentoVO avo = null;
            EscolaEquipamento a = null;
            Escola e = null;
            Equipamento eq = null;

            var _qlista =

            Session.QueryOver<EscolaEquipamento>(() => a)
                .Inner.JoinQueryOver<Escola>(() => a.Escola, () => e)
                .Inner.JoinQueryOver<Equipamento>(() => a.Equipamento, () => eq)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => eq.Id).WithAlias(() => avo.EquipamentoId)
                    .Select(() => eq.Descricao).WithAlias(() => avo.EquipamentoDescricao)
                    .Select(() => e.Id).WithAlias(() => avo.EscolaId)
                    .Select(() => a.Quantidade).WithAlias(() => avo.Quantidade)
                ).Where(() => e.Id == id);

            IEnumerable<EscolaEquipamentoVO> retorno =
               _qlista
               .TransformUsing(Transformers.AliasToBean<EscolaEquipamentoVO>())
               .List<EscolaEquipamentoVO>()
               .OrderBy(x => x.EquipamentoDescricao);
            return retorno;
             */ 
        }

        public EscolaEquipamentoVO GetEscolaEquipamentoVOById(int id)
        {
            EscolaEquipamentoVO model =
                Session.CreateQuery("SELECT " +
                    "tb.Id as Id, " +
                    "eq.Id as EquipamentoId, " +
                    "eq.Descricao as EquipamentoDescricao, " +
                    "e.Id as EscolaId, " +
                    "tb.Quantidade as Quantidade " +
                    "FROM EscolaEquipamento tb " +
                    "INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.Equipamento eq " +
                    "WHERE tb.Id = :id " +
                    "ORDER BY eq.Descricao"
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaEquipamentoVO)))
                .UniqueResult<EscolaEquipamentoVO>();

            return model;


            /*
            EscolaEquipamentoVO avo = null;
            EscolaEquipamento a = null;
            Escola e = null;
            Equipamento eq = null;

            EscolaEquipamentoVO model =
            Session.QueryOver<EscolaEquipamento>(() => a)
                .Inner.JoinQueryOver<Escola>(() => a.Escola, () => e)
                .Inner.JoinQueryOver<Equipamento>(() => a.Equipamento, () => eq)
                .SelectList(list => list
                    .Select(() => a.Id).WithAlias(() => avo.Id)
                    .Select(() => eq.Id).WithAlias(() => avo.EquipamentoId)
                    .Select(() => eq.Descricao).WithAlias(() => avo.EquipamentoDescricao)
                    .Select(() => e.Id).WithAlias(() => avo.EscolaId)
                    .Select(() => a.Quantidade).WithAlias(() => avo.Quantidade)
                ).Where(() => a.Id == id)
                .TransformUsing(Transformers.AliasToBean<EscolaEquipamentoVO>())
                .SingleOrDefault<EscolaEquipamentoVO>();
            return model;
             */ 
        }

    } // END CLASS
} // END NAMESPACE

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

    public class EscolaTurnoDAO : GenericDAO<EscolaTurno>
    {

        public IEnumerable<EscolaTurnoVO> GetListaVOByEscolaId(int escolaId)
        {
            // TODO ERRO!!!
            IEnumerable<EscolaTurnoVO> model = 
                Session.CreateQuery(
                    "SELECT "+
                    "tb.Id as Id, "+
                    "tb.Escola.Id as EscolaId, "+
                    //"e.Nome as EscolaNome, "+
                    "t.Descricao as TurnoDescricao, "+
                    "t.HoraInicial as HoraInicial, "+
                    "t.HoraFinal as HoraFinal "+
                    "FROM EscolaTurno tb "+
                    //"INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.Turno t " +
                    "WHERE tb.Escola.Id = :escolaId " +
                    "ORDER BY t.HoraInicial, t.HoraFinal"
                )
                .SetParameter("escolaId", escolaId)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaTurnoVO)))
                .List<EscolaTurnoVO>();

            return model;
        }

        public EscolaTurnoVO GetEscolaTurnoVOById(int id)
        {
            EscolaTurnoVO model =
                Session.CreateQuery(
                    "SELECT " +
                    "tb.Id as Id, " +
                    "tb.Escola.Id as EscolaId, " +
                    //"e.Nome as EscolaNome, " +
                    "t.Descricao as TurnoDescricao, " +
                    "t.HoraInicial as HoraInicial, " +
                    "t.HoraFinal as HoraFinal " +
                    "FROM EscolaTurno tb " +
                    //"INNER JOIN tb.Escola e " +
                    "INNER JOIN tb.Turno t " +
                    "WHERE tb.Id = :id "
                )
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaTurnoVO)))
                .UniqueResult<EscolaTurnoVO>();

            return model;
        }

    } // END CLASS
} // END NAMESPACE

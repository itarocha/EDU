using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.Entities.VO;
//using Petra.Util.Model;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class CalendarioDAO : GenericDAO<Calendario>
    {

        public Calendario GetByEscolaAno(int escolaId, int ano)
        {
            Calendario value = Session.QueryOver<Calendario>()
                .Where(x => x.Escola.Id == escolaId)
                .And(x => x.AnoLetivo.Ano == ano)
                .List().FirstOrDefault();
            return value;
        }

        public CalendarioVO GetCalendarioVO(int id, int escolaId, int anoLetivoId)
        {
            CalendarioVO model = Session.CreateQuery("SELECT "+
                "c.Id as Id, "+
                "e.Id as EscolaId, "+
                "c.Descricao as Descricao, "+
                "c.DataInicio as DataInicio, "+
                "c.DataTermino as DataTermino, "+
                "c.DataResultado as DataResultado, "+
                "c.DiasLetivos as DiasLetivos "+
                "FROM Calendario c "+
                "INNER JOIN c.Escola e "+
                "INNER JOIN c.AnoLetivo al "+
                "WHERE c.Id = :id "+ 
                "AND e.Id = :escolaId "+
                "AND al.Id = :anoLetivoId ")
                .SetParameter("id",id)
                .SetParameter("escolaId", escolaId)
                .SetParameter("anoLetivoId",anoLetivoId)
                .SetResultTransformer(Transformers.AliasToBean(typeof(CalendarioVO)))
                .UniqueResult<CalendarioVO>();
            return model;
        }

        public IEnumerable<Calendario> GetListagemByEscolaAno(int escolaId, int ano)
        {
            IQueryOver<Calendario> q = Session.QueryOver<Calendario>();
            IEnumerable<Calendario> lista;

            lista = q.List<Calendario>()
                .Where(x => x.Escola.Id == escolaId)
                .Where(x => x.AnoLetivo.Ano == ano)
                .ToList();

            return lista;
        }

    }
}

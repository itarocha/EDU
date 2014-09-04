using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.Entities.VO;
using Petra.Util.Model;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class HorarioDAO : GenericDAO<Horario>
    {

        public Horario GetByEscolaHorario(string horaIni, string horaFim)
        {
            Horario value = Session.QueryOver<Horario>()
                .Where(x => x.HoraInicial == horaIni)
                .And(x => x.HoraFinal == horaFim)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<Horario> GetListagemByEscolaId(int escolaId)
        {
            IQueryOver<Horario> q = Session.QueryOver<Horario>();
            IEnumerable<Horario> lista;

            lista = q.List<Horario>()/*.Where(x => x.Escola.Id == escolaId)*/.ToList();

            return lista;
        }


        public IEnumerable<Horario> GetListagem(string searchString = null)
        {
            IQueryOver<Horario> q = Session.QueryOver<Horario>();
            IEnumerable<Horario> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Horario>().Where(s => s.Descricao.ToLower().Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Horario>().ToList();
            }
            return lista;
        }

        public HorarioVO GetVOById(int id)
        {
        	HorarioVO model = Session.CreateQuery(
        		"SELECT "+
                "tb.Id as Id, "+
                //"e.Id as EscolaId, "+
                "tb.Descricao as Descricao, "+
                "tb.HoraInicial as HoraInicial, "+
                "tb.HoraFinal as HoraFinal "+
                //"t.Sequencia as Sequencia "+
                "FROM Horario tb " +
        		//"INNER JOIN t.Escola e "+
        		"WHERE tb.Id = :id")
        		.SetParameter("id",id)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(HorarioVO)))
        		.UniqueResult<HorarioVO>();
        	
        	return model;
        	
        }

    }
}

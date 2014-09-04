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
    public class TurnoDAO : GenericDAO<Turno>
    {

        public IEnumerable<Turno> GetListagemByEscolaId(int escolaId)
        {
            IQueryOver<Turno> q = Session.QueryOver<Turno>();
            IEnumerable<Turno> lista;

            lista = q.List<Turno>()/*.Where(x => x.Escola.Id == escolaId)*/.ToList();

            return lista;
        }


        public IEnumerable<Turno> GetListagem(string searchString = null)
        {
            IQueryOver<Turno> q = Session.QueryOver<Turno>();
            IEnumerable<Turno> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Turno>().Where(s => s.Descricao.ToLower().Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Turno>().ToList();
            }
            return lista;
        }

        public TurnoVO GetVOById(int id)
        {
        	TurnoVO model = Session.CreateQuery(
        		"SELECT "+
                "t.Id as Id, "+
                //"e.Id as EscolaId, "+
                "t.Descricao as Descricao "+
                //"t.Sequencia as Sequencia "+
        		"FROM Turno t "+
        		//"INNER JOIN t.Escola e "+
        		"WHERE t.Id = :id")
        		.SetParameter("id",id)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(TurnoVO)))
        		.UniqueResult<TurnoVO>();
        	
        	return model;
        	
        }

    }
}

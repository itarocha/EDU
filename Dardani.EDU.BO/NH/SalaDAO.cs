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
    public class SalaDAO : GenericDAO<Sala>
    {
        public IEnumerable<Sala> GetListagemByEscolaId(int escolaId)
        {
            IQueryOver<Sala> q = Session.QueryOver<Sala>();
            IEnumerable<Sala> lista;

            lista = q.List<Sala>().Where(s => s.Escola.Id == escolaId).ToList();
            return lista;
        }

        public SalaVO GetVOById(int id)
        {
        	SalaVO model = Session.CreateQuery(
        		    "SELECT "+
                    "s.Id as Id, "+
                    "s.Descricao as Descricao, "+
                    "s.Metragem as Metragem, "+
                    "s.Capacidade as Capacidade, "+
                    "se.Id as EscolaId, "+
                    "se.Nome as EscolaNome, "+
                    "ts.Id as TipoSalaId, "+
                    "ts.Descricao as TipoSalaDescricao, "+
                    "ts.FlagSalaAula as TipoSalaFlagSalaAula "+
		            "FROM Sala s "+
		            "INNER JOIN s.Escola se "+
		            "INNER JOIN s.TipoSala ts "+
		            "WHERE s.Id = :id")
        		.SetParameter("id", id)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(SalaVO)))
        		.UniqueResult<SalaVO>();
        	return model;
        	
        	/*
            SalaVO svo = null;
            Sala s = null;
            Escola se = null;
            TipoSala ts = null;

            SalaVO model =
            Session.QueryOver<Sala>(() => s)
                .Inner.JoinQueryOver<Escola>(() => s.Escola, () => se)
                .Inner.JoinQueryOver<TipoSala>(() => s.TipoSala, () => ts)
                .SelectList(list => list
                    .Select(() => s.Id).WithAlias(() => svo.Id)
                    .Select(() => s.Descricao).WithAlias(() => svo.Descricao)
                    .Select(() => s.Metragem).WithAlias(() => svo.Metragem)
                    .Select(() => s.Capacidade).WithAlias(() => svo.Capacidade)
                    .Select(() => se.Id).WithAlias(() => svo.EscolaId)
                    .Select(() => se.Nome).WithAlias(() => svo.EscolaNome)
                    .Select(() => ts.Id).WithAlias(() => svo.TipoSalaId)
                    .Select(() => ts.Descricao).WithAlias(() => svo.TipoSalaDescricao)
                    .Select(() => ts.FlagSalaAula).WithAlias(() => svo.TipoSalaFlagSalaAula)
                    
                ).Where(() => s.Id == id)
                .TransformUsing(Transformers.AliasToBean<SalaVO>())
                .SingleOrDefault<SalaVO>();

            return model;
            */
        }

    }
}

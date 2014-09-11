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
                "t.Descricao as Descricao "+
        		"FROM Turno t "+
        		"WHERE t.Id = :id")
        		.SetParameter("id",id)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(TurnoVO)))
        		.UniqueResult<TurnoVO>();
        	return model;
        }

        public bool PossuiEscolaTurno(int id)
        {
            Int64 qtd = 0;
            qtd = Session.CreateQuery(  "SELECT count(distinct tb.Id) as qtd " +
                                        "FROM EscolaTurno as tb " +
                                        "WHERE tb.Turno.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return (qtd > 0);
        }

        public bool PossuiTurma(int id)
        {
            Int64 qtd = 0;
            qtd = Session.CreateQuery(  "SELECT count(distinct tb.Id) as qtd " +
                                        "FROM Turma as tb " +
                                        "WHERE tb.Turno.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return (qtd > 0);
        }

        public bool PodeExcluir(int id, out string mensagemRetorno)
        {
            mensagemRetorno = "";
            if (this.PossuiTurma(id))
            {
                mensagemRetorno = "Turno está sendo utilizado em Turmas";
                return false;
            }
            if (this.PossuiEscolaTurno(id))
            {
                mensagemRetorno = "Existem Escolas que utilizam esse Turno";
                return false;
            }
            return true;
        }


    }
}

// Wuthering Heights - Kate Bush
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
    public class DisciplinaDAO : GenericDAO<Disciplina>
    {

        public IEnumerable<Disciplina> GetListagem(string searchString = null)
        {
            IQueryOver<Disciplina> q = Session.QueryOver<Disciplina>();
            IEnumerable<Disciplina> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Disciplina>().Where(s => s.Descricao.ToLower().Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Disciplina>().ToList();
            }
            return lista;
        }


        public DisciplinaVO GetVOById(int id)
        {
            DisciplinaVO model = Session.CreateQuery("SELECT "+
                "d.Id as Id, "+
                "d.Descricao as Descricao, "+
                "d.DescricaoAbreviada as DescricaoAbreviada, "+
                "d.FlagAtivo as FlagAtivo, "+
                "de.Id as DisciplinaEducacensoId, "+
                "de.Descricao as DisciplinaEducacensoDescricao, " +
                "de.ValorEducacenso as ValorEducacenso " +
                "FROM Disciplina d "+
                "INNER JOIN d.DisciplinaEducacenso de "+
                "WHERE d.Id = :id")
                .SetParameter("id", id)
                .SetResultTransformer(Transformers.AliasToBean(typeof(DisciplinaVO)))
                .UniqueResult<DisciplinaVO>();

            return model;
        }
        

        public IEnumerable<DisciplinaVO> GetListagemVO()
        {

            IEnumerable<DisciplinaVO> model = 
                Session.CreateQuery("SELECT " +
                "d.Id as Id, " +
                "d.Descricao as Descricao, " +
                "d.DescricaoAbreviada as DescricaoAbreviada, " +
                "d.FlagAtivo as FlagAtivo, " +
                "de.Id as DisciplinaEducacensoId, " +
                "de.Descricao as DisciplinaEducacensoDescricao, " +
                "de.ValorEducacenso as ValorEducacenso " +
                "FROM Disciplina d " +
                "INNER JOIN d.DisciplinaEducacenso de " +
                "ORDER BY d.Descricao")
                .SetResultTransformer(Transformers.AliasToBean(typeof(DisciplinaVO)))
                .List<DisciplinaVO>();
            return model;
        }

        public bool PossuiMatrizDisciplina(int id)
        {

            Int64 qtd = Session.CreateQuery("SELECT count(distinct tb.Id) as qtd " +
                                            "FROM MatrizDisciplina as tb " +
                                            "WHERE tb.Disciplina.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return qtd > 0;
        }

        public bool PossuiTurmaHorario(int id)
        {

            Int64 qtd = Session.CreateQuery("SELECT count(distinct tb.Id) as qtd " +
                                            "FROM TurmaHorario as tb " +
                                            "WHERE tb.Disciplina.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return qtd > 0;
        }

        public bool PodeExcluir(int id, out string mensagemRetorno)
        {
            mensagemRetorno = "";
            if (this.PossuiMatrizDisciplina(id))
            {
                mensagemRetorno = "Disciplina está sendo utilizada em Matriz Curricular";
                return false;
            }
            if (this.PossuiTurmaHorario(id))
            {
                mensagemRetorno = "Disciplina está sendo utilizada em Quadro de Horários";
                return false;
            }
            return true;
        }

    }
}

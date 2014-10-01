using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class ConceitoDAO : GenericDAO<Conceito>
    {
        public IEnumerable<Conceito> GetListagem(string searchString = null)
        {
            IQueryOver<Conceito> q = Session.QueryOver<Conceito>();
            IEnumerable<Conceito> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Conceito>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Conceito>().ToList();
            }
            return lista;
        }
        
        public bool PossuiConceitoNivel(int id)
        {

            Int64 qtd = Session.CreateQuery("SELECT count(distinct tb.Id) as qtd " +
                                            "FROM ConceitoNivel as tb " +
                                            "WHERE tb.Conceito.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return qtd > 0;
        }

        /*
        public bool PossuiTurma(int id)
        {

            Int64 qtd = Session.CreateQuery("SELECT count(distinct tb.Id) as qtd " +
                                            "FROM Turma as tb " +
                                            "WHERE tb.Horario.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return qtd > 0;
        }
        */

        public bool PodeExcluir(int id, out string mensagemRetorno)
        {
            mensagemRetorno = "";
            if (this.PossuiConceitoNivel(id))
            {
                mensagemRetorno = "Conceito possui Níveis";
                return false;
            }
            /*
            if (this.PossuiTurma(id))
            {
                mensagemRetorno = "Horário está sendo utilizada em Turmas";
                return false;
            }
             */ 
            return true;
        }


    } // END CLASS
} // END NAMESPACE

using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Petra.Util.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class TipoSalaDAO : GenericDAO<TipoSala>
    {
        public IEnumerable<TipoSala> GetListagem(string searchString = null)
        {
            IQueryOver<TipoSala> q = Session.QueryOver<TipoSala>();
            IEnumerable<TipoSala> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TipoSala>()
                	.Where(s => s.Descricao.ToLower().Contains(searchString.ToLower()))
                	.ToList();
            }
            else
            {
                lista = q.List<TipoSala>().ToList();
            }
            return lista;
        }

        public bool PossuiSala(int id) {
            Int64 qtd = Session.CreateQuery("SELECT count(distinct tb.Id) as qtd " +
                                            "FROM Sala as tb " +
                                            "WHERE tb.TipoSala.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return (qtd > 0);
        }

        public bool PodeExcluir(int id, out string mensagemRetorno)
        {
            mensagemRetorno = "";
            if (this.PossuiSala(id))
            {
                mensagemRetorno = "Existem Salas com esse Tipo de Sala";
                return false;
            }
            return true;
        }

    }
}

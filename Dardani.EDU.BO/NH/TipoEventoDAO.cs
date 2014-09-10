using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class TipoEventoDAO : GenericDAO<TipoEvento>
    {
        public IEnumerable<TipoEvento> GetListagem()
        {
            IEnumerable<TipoEvento> lista = Session.QueryOver<TipoEvento>()
                .OrderBy(x => x.Descricao).Asc.List();
            
            return lista;
        }

        public bool PossuiCalendario(int id) {
            Int64 qtd = 0;
            qtd = Session.CreateQuery("SELECT count(distinct cd.Id) as qtd " +
                                        "FROM CalendarioDiaEvento as cd " +
                                        "WHERE cd.TipoEvento.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return (qtd > 0);
        }

        public bool PodeExcluir(int id, out string mensagemRetorno)
        {
            mensagemRetorno = "";
            if (this.PossuiCalendario(id))
            {
                mensagemRetorno = "Tipo de Evento está sendo utilizado em Calendários";
                return false;
            }
            return true;
        }
    } // END CLASS
} // END NAMESPACE

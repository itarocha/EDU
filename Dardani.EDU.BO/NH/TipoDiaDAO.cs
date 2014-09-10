using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Transform;

namespace Dardani.EDU.BO.NH
{
    public class TipoDiaDAO : GenericDAO<TipoDia>
    {
        public IEnumerable<TipoDia> GetListagem()
        {
            IEnumerable<TipoDia> lista = Session.QueryOver<TipoDia>()
                .OrderBy(x => x.Descricao).Desc.List();
            
            return lista;
        }

        public bool PossuiCalendarioDia(int id) {
            Int64 qtd = Session.CreateQuery("SELECT count(distinct cd.Id) as qtd " +
                                            "FROM CalendarioDia as cd " +
                                            "WHERE cd.TipoDia.Id = :id ")
                       .SetParameter("id", id)
                       .UniqueResult<Int64>();
            return qtd > 0;
        }

        public bool PodeExcluir(int id, out string mensagemRetorno){
            mensagemRetorno = "";
            if (this.PossuiCalendarioDia(id))
            {
                mensagemRetorno = "Tipo de Dia está sendo utilizado em Calendário";
                return false;
            }
            return true;
        }

    } // END CLASS
} // END NAMESPACE

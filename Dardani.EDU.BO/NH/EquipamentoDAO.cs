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
    public class EquipamentoDAO : GenericDAO<Equipamento>
    {

        public IEnumerable<Equipamento> GetListagem(string searchString = null)
        {
            IQueryOver<Equipamento> q = Session.QueryOver<Equipamento>();
            IEnumerable<Equipamento> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Equipamento>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Equipamento>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

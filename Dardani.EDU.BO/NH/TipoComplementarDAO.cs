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
    public class TipoComplementarDAO : GenericDAO<TipoComplementar>
    {

        public IEnumerable<TipoComplementar> GetListagem(string searchString = null)
        {
            IQueryOver<TipoComplementar> q = Session.QueryOver<TipoComplementar>();
            IEnumerable<TipoComplementar> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TipoComplementar>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<TipoComplementar>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

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
    public class EspecialCategoriaDAO : GenericDAO<EspecialCategoria>
    {

        public IEnumerable<EspecialCategoria> GetListagem(string searchString = null)
        {
            IQueryOver<EspecialCategoria> q = Session.QueryOver<EspecialCategoria>();
            IEnumerable<EspecialCategoria> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<EspecialCategoria>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<EspecialCategoria>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

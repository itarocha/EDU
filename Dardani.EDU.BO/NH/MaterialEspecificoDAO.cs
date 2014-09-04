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
    public class MaterialEspecificoDAO : GenericDAO<MaterialEspecifico>
    {

        public IEnumerable<MaterialEspecifico> GetListagem(string searchString = null)
        {
            IQueryOver<MaterialEspecifico> q = Session.QueryOver<MaterialEspecifico>();
            IEnumerable<MaterialEspecifico> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<MaterialEspecifico>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<MaterialEspecifico>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

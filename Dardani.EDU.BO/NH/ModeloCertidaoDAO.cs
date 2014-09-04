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
    public class ModeloCertidaoDAO : GenericDAO<ModeloCertidao>
    {

        public IEnumerable<ModeloCertidao> GetListagem(string searchString = null)
        {
            IQueryOver<ModeloCertidao> q = Session.QueryOver<ModeloCertidao>();
            IEnumerable<ModeloCertidao> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<ModeloCertidao>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<ModeloCertidao>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

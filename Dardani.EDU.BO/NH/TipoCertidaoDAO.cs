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
    public class TipoCertidaoDAO : GenericDAO<TipoCertidao>
    {

        public IEnumerable<TipoCertidao> GetListagem(string searchString = null)
        {
            IQueryOver<TipoCertidao> q = Session.QueryOver<TipoCertidao>();
            IEnumerable<TipoCertidao> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TipoCertidao>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<TipoCertidao>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

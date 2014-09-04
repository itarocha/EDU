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
    public class TipoInstituicaoDAO : GenericDAO<TipoInstituicao>
    {

        public IEnumerable<TipoInstituicao> GetListagem(string searchString = null)
        {
            IQueryOver<TipoInstituicao> q = Session.QueryOver<TipoInstituicao>();
            IEnumerable<TipoInstituicao> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TipoInstituicao>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<TipoInstituicao>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

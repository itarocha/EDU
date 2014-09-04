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
    public class TipoNacionalidadeDAO : GenericDAO<TipoNacionalidade>
    {

        public TipoNacionalidade GetByValorEducacenso(int codigo)
        {
            TipoNacionalidade value = Session.QueryOver<TipoNacionalidade>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<TipoNacionalidade> GetListagem(string searchString = null)
        {
            IQueryOver<TipoNacionalidade> q = Session.QueryOver<TipoNacionalidade>();
            IEnumerable<TipoNacionalidade> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TipoNacionalidade>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<TipoNacionalidade>().ToList();
            }
            return lista;
        }
    } // END CLASS
} // END NAMESPACE

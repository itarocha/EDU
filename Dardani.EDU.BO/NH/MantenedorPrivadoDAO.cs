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
    public class MantenedorPrivadoDAO : GenericDAO<MantenedorPrivado>
    {

        public MantenedorPrivado GetByValorEducacenso(int codigo)
        {
            MantenedorPrivado retorno = Session.QueryOver<MantenedorPrivado>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return retorno;
        }

        public IEnumerable<MantenedorPrivado> GetListagem(string searchString = null)
        {
            IQueryOver<MantenedorPrivado> q = Session.QueryOver<MantenedorPrivado>();
            IEnumerable<MantenedorPrivado> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<MantenedorPrivado>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<MantenedorPrivado>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

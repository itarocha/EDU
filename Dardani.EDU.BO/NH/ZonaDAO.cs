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
    public class ZonaDAO : GenericDAO<Zona>
    {

        public Zona GetByValorEducacenso(int codigo)
        {
            Zona retorno = Session.QueryOver<Zona>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return retorno;
        }

        public IEnumerable<Zona> GetListagem(string searchString = null)
        {
            IQueryOver<Zona> q = Session.QueryOver<Zona>();
            IEnumerable<Zona> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Zona>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Zona>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

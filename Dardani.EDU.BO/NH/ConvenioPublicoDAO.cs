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
    public class ConvenioPublicoDAO : GenericDAO<ConvenioPublico>
    {

        public ConvenioPublico GetByValorEducacenso(int codigo)
        {
            ConvenioPublico retorno = Session.QueryOver<ConvenioPublico>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return retorno;
        }

        public IEnumerable<ConvenioPublico> GetListagem(string searchString = null)
        {
            IQueryOver<ConvenioPublico> q = Session.QueryOver<ConvenioPublico>();
            IEnumerable<ConvenioPublico> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<ConvenioPublico>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<ConvenioPublico>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

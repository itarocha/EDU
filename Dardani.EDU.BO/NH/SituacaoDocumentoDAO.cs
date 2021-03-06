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
    public class SituacaoDocumentoDAO : GenericDAO<SituacaoDocumento>
    {

        public IEnumerable<SituacaoDocumento> GetListagem(string searchString = null)
        {
            IQueryOver<SituacaoDocumento> q = Session.QueryOver<SituacaoDocumento>();
            IEnumerable<SituacaoDocumento> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<SituacaoDocumento>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<SituacaoDocumento>().ToList();
            }
            return lista;
        }

     } // END CLASS
} // END NAMESPACE

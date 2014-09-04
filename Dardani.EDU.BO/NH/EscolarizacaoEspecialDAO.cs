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
    public class EscolarizacaoEspecialDAO : GenericDAO<EscolarizacaoEspecial>
    {

        public EscolarizacaoEspecial GetByValorEducacenso(int codigo)
        {
            EscolarizacaoEspecial value = Session.QueryOver<EscolarizacaoEspecial>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<EscolarizacaoEspecial> GetListagem(string searchString = null)
        {
            IQueryOver<EscolarizacaoEspecial> q = Session.QueryOver<EscolarizacaoEspecial>();
            IEnumerable<EscolarizacaoEspecial> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<EscolarizacaoEspecial>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<EscolarizacaoEspecial>().ToList();
            }
            return lista;
        }
    } // END CLASS
} // END NAMESPACE

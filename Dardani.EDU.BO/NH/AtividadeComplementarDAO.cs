using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
//using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class AtividadeComplementarDAO : GenericDAO<AtividadeComplementar>
    {

        public AtividadeComplementar GetByValorEducacenso(int codigo)
        {
            AtividadeComplementar value = Session.QueryOver<AtividadeComplementar>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<AtividadeComplementar> GetListagem(string searchString = null)
        {
            IQueryOver<AtividadeComplementar> q = Session.QueryOver<AtividadeComplementar>();
            IEnumerable<AtividadeComplementar> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<AtividadeComplementar>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<AtividadeComplementar>().ToList();
            }
            return lista;
        }
    } // END CLASS
} // END NAMESPACE

using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
//using Petra.Util.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class DisciplinaEducacensoDAO : GenericDAO<DisciplinaEducacenso>
    {

        public DisciplinaEducacenso GetByValorEducacenso(int codigo)
        {
            DisciplinaEducacenso value = Session.QueryOver<DisciplinaEducacenso>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }
        
        public IEnumerable<DisciplinaEducacenso> GetListagem(string searchString = null)
        {
            IQueryOver<DisciplinaEducacenso> q = Session.QueryOver<DisciplinaEducacenso>();
            IEnumerable<DisciplinaEducacenso> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<DisciplinaEducacenso>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower()))
                    .ToList();
            }
            else
            {
                lista = q.List<DisciplinaEducacenso>().ToList();
            }
            return lista;
        }
    }
}

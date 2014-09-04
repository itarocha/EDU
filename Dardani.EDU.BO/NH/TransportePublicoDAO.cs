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
    public class TransportePublicoDAO : GenericDAO<TransportePublico>
    {

        public TransportePublico GetByValorEducacenso(int codigo)
        {
            TransportePublico value = Session.QueryOver<TransportePublico>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<TransportePublico> GetListagem(string searchString = null)
        {
            IQueryOver<TransportePublico> q = Session.QueryOver<TransportePublico>();
            IEnumerable<TransportePublico> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<TransportePublico>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<TransportePublico>().ToList();
            }
            return lista;
        }
    } // END CLASS
} // END NAMESPACE

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
    public class AcaoDAO : GenericDAO<Acao>
    {
        public Acao GetByIdString(String id) {
            return Session.QueryOver<Acao>().Where(x => x.Id == id).SingleOrDefault<Acao>();
        }

        public IEnumerable<Acao> GetListagem()
        {
            IQueryOver<Acao> q = Session.QueryOver<Acao>();
            IEnumerable<Acao> lista;
            lista = q.List<Acao>().OrderBy(x => x.Descricao).ToList();
            return lista;
        }


    } // END CLASS
} // END NAMESPACE

using Dardani.EDU.Entities.Model;
using Petra.DAO.NH;
using Petra.DAO.Util;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.GER.BO.NH
{
    public class RacaDAO : GenericDAO<Raca> {
        
        public Raca GetByValorEducacenso(int codigo)
        {
            Raca retorno = Session.QueryOver<Raca>()
                .And(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return retorno;
        }
    }
}

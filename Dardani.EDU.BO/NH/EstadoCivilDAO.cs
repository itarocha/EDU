using Dardani.EDU.Entities.Model;
using Petra.DAO.NH;
using Petra.DAO.Util;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class EstadoCivilDAO : GenericDAO<EstadoCivil> {

        public EstadoCivil GetByValorEducacenso(int codigo)
        {
            EstadoCivil retorno = Session.QueryOver<EstadoCivil>()
                .And(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return retorno;
        }
    }
}

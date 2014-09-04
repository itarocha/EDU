using Petra.DAO.NH;
using Petra.DAO.Util;
using Dardani.EDU.Entities.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class SexoDAO : GenericDAO<Sexo> {

        public Sexo GetByValorEducacenso(int codigo)
        {
            Sexo retorno = Session.QueryOver<Sexo>()
                .And(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return retorno;
        }
    }
}

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
    public class MunicipioDAO : GenericDAO<Municipio>
    {
        public Municipio GetByValorEducacenso(string codigo)
        {
            Municipio value = Session.QueryOver<Municipio>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

    } // END CLASS
} // END NAMESPACE

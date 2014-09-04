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
    public class EtapaDAO : GenericDAO<Etapa>
    {
        public Etapa GetByValorEducacenso(int modalidadeId, int valorEducacenso)
        {
            Etapa value = Session.QueryOver<Etapa>()
                .Where(x => x.Modalidade.Id == modalidadeId)
                .And(x => x.ValorEducacenso == valorEducacenso)
                .List().FirstOrDefault();
            return value;
        }

    } // END CLASS
} // END NAMESPACE

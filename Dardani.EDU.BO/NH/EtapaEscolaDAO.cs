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
    public class EtapaEscolaDAO : GenericDAO<EtapaEscola>
    {
        public EtapaEscola GetByModalidadeValorEducacenso(int modalidadeId, int valorEducacenso)
        {
            EtapaEscola value = Session.QueryOver<EtapaEscola>()
                .Where(x => x.Modalidade.Id == modalidadeId)
                .And(x => x.ValorEducacenso == valorEducacenso)
                .List().FirstOrDefault();
            return value;
        }

        public EtapaEscola GetByValorEducacenso(int valorEducacenso)
        {
            EtapaEscola value = Session.QueryOver<EtapaEscola>()
                .Where(x => x.ValorEducacenso == valorEducacenso)
                .List().FirstOrDefault();
            return value;
        }

    } // END CLASS
} // END NAMESPACE

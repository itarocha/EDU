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
    public class SituacaoFuncionamentoDAO : GenericDAO<SituacaoFuncionamento>
    {

        public IEnumerable<SituacaoFuncionamento> GetListagem(string searchString = null)
        {
            IQueryOver<SituacaoFuncionamento> q = Session.QueryOver<SituacaoFuncionamento>();
            IEnumerable<SituacaoFuncionamento> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<SituacaoFuncionamento>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<SituacaoFuncionamento>().ToList();
            }
            return lista;
        }

        public SituacaoFuncionamento GetByValorEducacenso(int codigo)
        {
            SituacaoFuncionamento sf = Session.QueryOver<SituacaoFuncionamento>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();            
            return sf;
        }

    } // END CLASS
} // END NAMESPACE

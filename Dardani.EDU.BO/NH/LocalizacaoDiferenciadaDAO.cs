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
    public class LocalizacaoDiferenciadaDAO : GenericDAO<LocalizacaoDiferenciada>
    {

        public LocalizacaoDiferenciada GetByValorEducacenso(int codigo)
        {
            LocalizacaoDiferenciada value = Session.QueryOver<LocalizacaoDiferenciada>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<LocalizacaoDiferenciada> GetListagem(string searchString = null)
        {
            IQueryOver<LocalizacaoDiferenciada> q = Session.QueryOver<LocalizacaoDiferenciada>();
            IEnumerable<LocalizacaoDiferenciada> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<LocalizacaoDiferenciada>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<LocalizacaoDiferenciada>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

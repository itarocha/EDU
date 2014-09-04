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
    public class VeiculoTipoDAO : GenericDAO<VeiculoTipo>
    {
        public VeiculoTipo GetByValorEducacenso(int codigo)
        {
            VeiculoTipo value = Session.QueryOver<VeiculoTipo>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<VeiculoTipo> GetListagem(string searchString = null)
        {
            IQueryOver<VeiculoTipo> q = Session.QueryOver<VeiculoTipo>();
            IEnumerable<VeiculoTipo> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<VeiculoTipo>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<VeiculoTipo>().ToList();
            }
            return lista;
        }

        public IEnumerable<ItemVO> BuidListaItemVO()
        {
            IEnumerable<VeiculoTipo> lista = Session.QueryOver<VeiculoTipo>()
                .OrderBy(x => x.Descricao).Asc.List();

            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }
    } // END CLASS
} // END NAMESPACE

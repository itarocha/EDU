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
    public class VeiculoCategoriaDAO : GenericDAO<VeiculoCategoria>
    {

        public IEnumerable<VeiculoCategoria> GetListagem(string searchString = null)
        {
            IQueryOver<VeiculoCategoria> q = Session.QueryOver<VeiculoCategoria>();
            IEnumerable<VeiculoCategoria> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<VeiculoCategoria>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<VeiculoCategoria>().ToList();
            }
            return lista;
        }

    } // END CLASS
} // END NAMESPACE

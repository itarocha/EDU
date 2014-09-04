using Dardani.DAO.NH;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.Entities.VO;
using Dardani.GER.Entities.Model;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dardani.EDU.BO.NH
{
    public class EscolaDAO : GenericDAO<Escola>
    {
        public IEnumerable<Escola> GetListagem(string searchString = null)
        {
            IQueryOver<Escola> q = Session.QueryOver<Escola>();
            IEnumerable<Escola> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Escola>().Where(s => s.Nome.ToLower().Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Escola>().ToList();
            }
            return lista;
        }

        public IEnumerable<ItemVO> BuidListaItemVO()
        {
            IEnumerable<Escola> lista = Session.QueryOver<Escola>().OrderBy(x => x.Nome).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Nome });
            }
            return retorno;
        }

        public EscolaVO GetVOById(int id)
        {
            EscolaVO evo = null;
            Escola e = null;

            EscolaVO model =
            Session.QueryOver<Escola>(() => e)
                .SelectList(list => list
                    .Select(() => e.Id).WithAlias(() => evo.Id)
                    .Select(() => e.Nome).WithAlias(() => evo.Nome)
                ).Where(() => e.Id == id)
                .TransformUsing(Transformers.AliasToBean<EscolaVO>())
                .SingleOrDefault<EscolaVO>();

            return model;
        }
    }
}

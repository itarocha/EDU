using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.Entities.VO;
using Petra.Util.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Transform;

namespace Dardani.EDU.BO.NH
{
    public class ModalidadeDAO : GenericDAO<Modalidade>
    {

        public Modalidade GetByValorEducacenso(int codigo)
        {
            Modalidade value = Session.QueryOver<Modalidade>()
                .Where(x => x.ValorEducacenso == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public IEnumerable<Modalidade> GetListagemFull()
        {
            IQueryOver<Modalidade> q = Session.QueryOver<Modalidade>();
            IEnumerable<Modalidade> lista;
            lista = q.List<Modalidade>().ToList();

            foreach (Modalidade ctg in lista)
            {
                IEnumerable<EtapaEscola> li =
                    Session.QueryOver<EtapaEscola>()
                    .Where(x => x.Modalidade.Id == ctg.Id).List();
                ctg.EtapasEscola.Clear();
                foreach (EtapaEscola item in li)
                {
                    ctg.EtapasEscola.Add(item);
                }
            }
            return lista;
        }

        public IEnumerable<Modalidade> GetListagem(string searchString = null)
        {
            IQueryOver<Modalidade> q = Session.QueryOver<Modalidade>();
            IEnumerable<Modalidade> lista;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = q.List<Modalidade>()
                    .Where(s => s.Descricao.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Modalidade>().ToList();
            }
            return lista;
        }

        public IEnumerable<EtapaVO> GetListaEtapasVO()
        {
        	IEnumerable<EtapaVO> model = Session.CreateQuery(
        	   "SELECT "+
               "e.Id as Id, "+
               "te.Id as TipoEnsinoId, "+
               "te.Descricao as TipoEnsinoDescricao, "+
               "m.Id as ModalidadeId, "+
               "m.Descricao as ModalidadeDescricao, "+
               "s.Id as SerieId, "+
               "s.Descricao as SerieDescricao "+
        	   "FROM Etapa e "+
        	   "INNER JOIN e.TipoEnsino te "+
        	   "INNER JOIN e.Modalidade m "+
        	   "INNER JOIN e.Serie s "+
        	   "ORDER BY m.Descricao")
        	   .SetResultTransformer(Transformers.AliasToBean(typeof(EtapaVO)))
        	   .List<EtapaVO>();
        	
        	return model;
        }

        public EtapaVO GetEtapaVO(int modalidadeId, int etapaId)
        {
        	EtapaVO model = 
        		Session.CreateQuery(
        	   "SELECT "+
               "e.Id as Id, "+
               "te.Id as TipoEnsinoId, "+
               "te.Descricao as TipoEnsinoDescricao, "+
               "m.Id as ModalidadeId, "+
               "m.Descricao as ModalidadeDescricao, "+
               "s.Id as SerieId, "+
               "s.Descricao as SerieDescricao "+
        	   "FROM Etapa e "+
        	   "INNER JOIN e.TipoEnsino te "+
        	   "INNER JOIN e.Modalidade m "+
        	   "INNER JOIN e.Serie s "+
        	   "WHERE e.Id = :etapaId "+
        	   "AND m.Id = :modalidadeId "+
        	   "ORDER BY m.Descricao")
        	   .SetParameter("etapaId",etapaId)
        		.SetParameter("modalidadeId",modalidadeId)
        		.SetResultTransformer(Transformers.AliasToBean(typeof(EtapaVO)))
        	   .UniqueResult<EtapaVO>();
        	
        	return model;
        }
  
        public IEnumerable<ItemVO> BuidListaItemVO()
        {
            IEnumerable<Modalidade> lista = Session.QueryOver<Modalidade>()
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


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
        	
        	/*
            Etapa e = null;
            EtapaVO avo = null;
            TipoEnsino te = null;
            Modalidade m = null;
            Serie s = null;

            IEnumerable<EtapaVO> lista =
            Session.QueryOver<Etapa>(() => e)
                .Inner.JoinQueryOver<TipoEnsino>(() => e.TipoEnsino, () => te)
                .Inner.JoinQueryOver<Modalidade>(() => e.Modalidade, () => m)
                .Inner.JoinQueryOver<Serie>(() => e.Serie, () => s)
                .SelectList(list => list
                    .Select(() => e.Id).WithAlias(() => avo.Id)
                    .Select(() => te.Id).WithAlias(() => avo.TipoEnsinoId)
                    .Select(() => te.Descricao).WithAlias(() => avo.TipoEnsinoDescricao)
                    .Select(() => m.Id).WithAlias(() => avo.ModalidadeId)
                    .Select(() => m.Descricao).WithAlias(() => avo.ModalidadeDescricao)
                    .Select(() => s.Id).WithAlias(() => avo.SerieId)
                    .Select(() => s.Descricao).WithAlias(() => avo.SerieDescricao)
                ).TransformUsing(Transformers.AliasToBean<EtapaVO>())
                .List<EtapaVO>()
                .OrderBy(x => x.ModalidadeDescricao);

            return lista;
            */
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
        	
        	/*
            Etapa e = null;
            EtapaVO avo = null;
            TipoEnsino te = null;
            Modalidade m = null;
            Serie s = null;

            EtapaVO model =
            Session.QueryOver<Etapa>(() => e)
                .Inner.JoinQueryOver<TipoEnsino>(() => e.TipoEnsino, () => te)
                .Inner.JoinQueryOver<Modalidade>(() => e.Modalidade, () => m)
                .Inner.JoinQueryOver<Serie>(() => e.Serie, () => s)
                .Where(() => e.Id == etapaId)
                .Where(() => e.Modalidade.Id == modalidadeId)
                .SelectList(list => list
                    .Select(() => e.Id).WithAlias(() => avo.Id)
                    .Select(() => te.Id).WithAlias(() => avo.TipoEnsinoId)
                    .Select(() => te.Descricao).WithAlias(() => avo.TipoEnsinoDescricao)
                    .Select(() => m.Id).WithAlias(() => avo.ModalidadeId)
                    .Select(() => m.Descricao).WithAlias(() => avo.ModalidadeDescricao)
                    .Select(() => s.Id).WithAlias(() => avo.SerieId)
                    .Select(() => s.Descricao).WithAlias(() => avo.SerieDescricao)
                ).TransformUsing(Transformers.AliasToBean<EtapaVO>())
                .SingleOrDefault<EtapaVO>();

            return model;
            */
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

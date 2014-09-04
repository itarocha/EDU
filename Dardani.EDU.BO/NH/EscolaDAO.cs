using System;
using Petra.DAO.NH;
using Dardani.EDU.Entities.Model;
using Petra.Util.Model;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Transform;
using Dardani.EDU.Entities.VO;
using NHibernate.Criterion;

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
                lista = q.List<Escola>()
                    .Where(s => s.Nome.ToLower()
                    .Contains(searchString.ToLower())).ToList();
            }
            else
            {
                lista = q.List<Escola>().ToList();
            }
            return lista;
        }

        public Escola GetByCodigoINEP(string codigo)
        {
            Escola value = Session.QueryOver<Escola>()
                .Where(x => x.CodigoINEP == codigo)
                .List().FirstOrDefault();
            return value;
        }

        public EscolaInfraestruturaVO GetEscolaInfraestrutura(int escolaId)
        {

            EscolaInfraestruturaVO escola = new EscolaInfraestruturaVO();
            escola.Id = escolaId;
            escola.EscolaId = escolaId;

            IEnumerable<EscolaInfraestruturaItem> itens =
                Session.QueryOver<EscolaInfraestruturaItem>().Where(x => x.Escola.Id == escolaId).List();
            List<int> list = new List<int>();
            foreach (EscolaInfraestruturaItem i in itens) {
                list.Add(i.InfraestruturaItem.Id);
            }
            escola.ListaItensInfraestrutura = list.ToArray();

            return escola;
        }


        public void GravarInfraestrutura(EscolaInfraestruturaVO escola){
            try {
                IEnumerable<EscolaInfraestruturaItem> itens =
                    Session.QueryOver<EscolaInfraestruturaItem>().Where(x => x.Escola.Id == escola.Id).List();

                foreach (EscolaInfraestruturaItem i in itens) {
                    Session.Delete(i);
                }

                Escola e = GetById(escola.Id);
                InfraestruturaItemDAO idao = new InfraestruturaItemDAO();

                foreach (int i in escola.ListaItensInfraestrutura) { 
                    InfraestruturaItem item = idao.GetById(i);
                    if ((e != null) && (item != null)) {
                        Session.Save(new EscolaInfraestruturaItem() { Escola = e, InfraestruturaItem = item });
                    }
                }
            } catch(Exception e) {
            }
        }

        public EscolaEducacionalVO GetEscolaEducacional(int escolaId)
        {
            EscolaEducacionalDAO eedao = new EscolaEducacionalDAO();
            EscolaEducacionalVO model = eedao.GetEscolaEducacionalVOById(escolaId);
            if (model == null)
            {
                model = new EscolaEducacionalVO();
                model.EscolaId = escolaId;
            }

            IEnumerable<EscolaEtapa> etapas =
                Session.QueryOver<EscolaEtapa>().Where(x => x.Escola.Id == escolaId).List();
            List<int> listaEtapas = new List<int>();
            foreach (EscolaEtapa ee in etapas)
            {
                listaEtapas.Add(ee.EtapaEscola.Id);
            }
            model.ListaEtapas = listaEtapas.ToArray();

            IEnumerable<EscolaModalidade> modalidades =
                Session.QueryOver<EscolaModalidade>().Where(x => x.Escola.Id == escolaId).List();
            List<int> listaModalidades = new List<int>();
            foreach (EscolaModalidade em in modalidades)
            {
                listaModalidades.Add(em.Modalidade.Id);
            }
            model.ListaModalidades = listaModalidades.ToArray();

            /*
            IEnumerable<EscolaInfraestruturaItem> itens =
                Session.QueryOver<EscolaInfraestruturaItem>().Where(x => x.Escola.Id == escolaId).List();

            EscolaInfraestruturaVO escola = new EscolaInfraestruturaVO();
            escola.Id = escolaId;
            escola.EscolaId = escolaId;

            List<int> list = new List<int>();
            foreach (EscolaInfraestruturaItem i in itens)
            {
                list.Add(i.InfraestruturaItem.Id);
            }
            escola.ListaItensInfraestrutura = list.ToArray();
            */
            return model;
        }


        public IEnumerable<ItemVO> BuidListaItemVO()
        {
            IEnumerable<Escola> lista = Session.QueryOver<Escola>()
                .OrderBy(x => x.Nome).Asc.List();

            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Nome });
            }
            return retorno;
        }


        public IEnumerable<EscolaVO> GetListaEscolas(/*string tipo*/)
        {
            IEnumerable<EscolaVO> retorno =
            Session.CreateQuery("SELECT " +
                    "e.Id as Id, " +
                    "e.Nome as Nome, " +
                    "e.CodigoINEP as CodigoINEP, " +
                    "sf.Id as SituacaoFuncionamentoId, " +
                    "sf.Descricao as SituacaoFuncionamentoDescricao, " +
                    "e.NomeGestor as NomeGestor, " +
                    "e.FlagGestorDiretor as FlagGestorDiretor, " +
                    "e.Email as Email, " +
                    "e.CodigoOrgaoRegional as CodigoOrgaoRegional, " +
                    "e.NomeOrgaoRegional as NomeOrgaoRegional, " +
                    "ta.Id as TipoAdministracaoId, " +
                    "ta.Descricao as TipoAdministracaoDescricao, " +
                    "er.Id as EstagioRegulamentacaoId, " +
                    "er.Descricao as EstagioRegulamentacaoDescricao, " +
                    "e.QuantidadeFuncionarios as QuantidadeFuncionarios, " +
                    "e.FlagAlimentacaoEscolar as FlagAlimentacaoEscolar " +
            "FROM Escola e " +
            "LEFT JOIN e.SituacaoFuncionamento sf " +
            "LEFT JOIN e.TipoAdministracao as ta " +
            "LEFT JOIN e.EstagioRegulamentacao as er " +
            "ORDER BY e.Nome")
            .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaVO)))
            .List<EscolaVO>();
            return retorno;
        }

        public EscolaVO GetEscolaVOById(int id)
        {
            EscolaVO model =
            Session.CreateQuery("SELECT " +
                    "e.Id as Id, " +
                    "e.Nome as Nome, " +
                    "e.CodigoINEP as CodigoINEP, " +
                    "sf.Id as SituacaoFuncionamentoId, " +
                    "sf.Descricao as SituacaoFuncionamentoDescricao, " +
                    "e.NomeGestor as NomeGestor, " +
                    "e.FlagGestorDiretor as FlagGestorDiretor, " +
                    "e.Email as Email, " +
                    "e.CodigoOrgaoRegional as CodigoOrgaoRegional, " +
                    "e.NomeOrgaoRegional as NomeOrgaoRegional, " +
                    "ta.Id as TipoAdministracaoId, " +
                    "ta.Descricao as TipoAdministracaoDescricao, " +
                    "er.Id as EstagioRegulamentacaoId, " +
                    "er.Descricao as EstagioRegulamentacaoDescricao, " +
                    "e.QuantidadeFuncionarios as QuantidadeFuncionarios, " +
                    "e.FlagAlimentacaoEscolar as FlagAlimentacaoEscolar " +
            "FROM Escola e " +
            "LEFT JOIN e.SituacaoFuncionamento sf " +
            "LEFT JOIN e.TipoAdministracao as ta " +
            "LEFT JOIN e.EstagioRegulamentacao as er "+
            "WHERE e.Id = :id")
            .SetParameter("id",id)
            .SetResultTransformer(Transformers.AliasToBean(typeof(EscolaVO)))
            .UniqueResult<EscolaVO>();
            
            return model;
        }
    } // END CLASS
} // END NAMESPACE

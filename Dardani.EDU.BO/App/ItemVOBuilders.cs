using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Transform;
using NHibernate.Criterion;
using Dardani.EDU.Entities.Model;
using Petra.DAO.Util;
using Petra.Util.Model;
using Dardani.EDU.BO.NH;
using Dardani.EDU.Entities.VO;

namespace Dardani.EDU.BO.App
{
    public sealed class ItemVOBuilders
    {
        private ISession Session { get { return NHibernateBase.Session; } }

        private static readonly ItemVOBuilders instance = new ItemVOBuilders();

        public static ItemVOBuilders Instance { get { return instance; } }

        private ItemVOBuilders()
        {
        }

        public IEnumerable<ItemVO> BuildListaEscolarizacaoEspecial()
        {

            IEnumerable<EscolarizacaoEspecial> lista = Session.QueryOver<EscolarizacaoEspecial>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaTransportePublico()
        {

            IEnumerable<TransportePublico> lista = Session.QueryOver<TransportePublico>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuidListaDisciplina()
        {
            IEnumerable<Disciplina> lista = Session.QueryOver<Disciplina>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuidListaDisciplinaEducacenso()
        {
            IEnumerable<DisciplinaEducacenso> lista = Session.QueryOver<DisciplinaEducacenso>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaTurmaUnificada()
        {

            IEnumerable<TurmaUnificada> lista = Session.QueryOver<TurmaUnificada>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }


        public IEnumerable<ItemVO> BuildListaEscola()
        {

            IEnumerable<Escola> lista = Session.QueryOver<Escola>().OrderBy(x => x.Nome).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Nome });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaAnoLetivo()
        {

            IEnumerable<AnoLetivo> lista = Session.QueryOver<AnoLetivo>().OrderBy(x => x.Ano).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Ano.ToString() });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaSexo()
        {

            IEnumerable<Sexo> lista = Session.QueryOver<Sexo>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }
        
        public IEnumerable<ItemVO> BuildListaEstadoCivil()
        {
            IEnumerable<EstadoCivil> lista = Session.QueryOver<EstadoCivil>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        /*
        public IEnumerable<ItemVO> BuildListaEscolaridades()
        {
            var lista =
                Session.QueryOver<using Petra.Util.Model;.Escolaridade>()
                    .Select(
                        c => c.Id,
                        c => c.Descricao)
                    .List<object[]>()
                    .Select(properties => new
                    {
                        Id = (int)properties[0],
                        Descricao = (string)properties[1],
                    });
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }
        */

        public IEnumerable<ItemVO> BuildListaRaca()
        {
            IEnumerable<Raca> lista = Session.QueryOver<Raca>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaPeriodoAula()
        {
            
            IEnumerable<PeriodoAula> lista =
                Session.QueryOver<PeriodoAula>().List().OrderBy(x => x.HoraInicio).OrderBy(x => x.HoraTermino);
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.HoraInicio + " - " + x.HoraTermino });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaCategoriaPrivada()
        {
            IEnumerable<CategoriaPrivada> lista = Session.QueryOver<CategoriaPrivada>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaAEE()
        {
            IEnumerable<AEE> lista = Session.QueryOver<AEE>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaAtividadeComplementar()
        {
            IEnumerable<AtividadeComplementar> lista = Session.QueryOver<AtividadeComplementar>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaLocalizacaoDiferenciada()
        {
            IEnumerable<LocalizacaoDiferenciada> lista = Session.QueryOver<LocalizacaoDiferenciada>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaLinguaIndigena()
        {
            IEnumerable<LinguaIndigena> lista = Session.QueryOver<LinguaIndigena>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaConvenioPublico()
        {
            IEnumerable<ConvenioPublico> lista = Session.QueryOver<ConvenioPublico>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaEquipamento()
        {
            IEnumerable<Equipamento> lista = Session.QueryOver<Equipamento>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaEstagioRegulamentacao()
        {
            IEnumerable<EstagioRegulamentacao> lista = Session.QueryOver<EstagioRegulamentacao>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaMantenedorPrivado()
        {
            IEnumerable<MantenedorPrivado> lista = Session.QueryOver<MantenedorPrivado>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaSituacaoFuncionamento()
        {
            IEnumerable<SituacaoFuncionamento> lista = Session.QueryOver<SituacaoFuncionamento>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }


        public IEnumerable<ItemVO> BuildListaTipoEnsino()
        {
            IEnumerable<TipoEnsino> lista = Session.QueryOver<TipoEnsino>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }


        public IEnumerable<ItemVO> BuildListaTipoAdministracao()
        {
            IEnumerable<TipoAdministracao> lista = Session.QueryOver<TipoAdministracao>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }


        public IEnumerable<ItemVO> BuildListaTipoNacionalidade()
        {
            IEnumerable<TipoNacionalidade> lista = Session.QueryOver<TipoNacionalidade>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaSituacaoDocumento()
        {
            IEnumerable<SituacaoDocumento> lista = Session.QueryOver<SituacaoDocumento>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaTipoCertidao()
        {
            IEnumerable<TipoCertidao> lista = Session.QueryOver<TipoCertidao>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaModeloCertidao()
        {
            IEnumerable<ModeloCertidao> lista = Session.QueryOver<ModeloCertidao>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaZona()
        {
            IEnumerable<Zona> lista = Session.QueryOver<Zona>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaPais()
        {
            IEnumerable<Pais> lista = Session.QueryOver<Pais>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaCalendario(int escolaId, int ano)
        {
            CalendarioDAO dao = new CalendarioDAO();
            IEnumerable<Calendario> lista = dao.GetListagemByEscolaAno(escolaId, ano);

            /*
            IEnumerable<Calendario> lista = Session.QueryOver<Calendario>()
                .Where(x => x.AnoLetivo.Ano == ano)
                .Where(x => x.Escola.Id == escolaId)
                .OrderBy(x => x.Descricao).Asc.List();
             */ 
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaSala(int escolaId)
        {
            IEnumerable<Sala> lista = Session.QueryOver<Sala>()
                .Where(x => x.Escola.Id == escolaId)
                .OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaTurno()
        {
            IEnumerable<Turno> lista = Session.QueryOver<Turno>()
                //.Where(x => x.Escola.Id == escolaId)
                .OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaHorario()
        {
            IEnumerable<Horario> lista = Session.QueryOver<Horario>()
                .OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaTipoAtendimento()
        {
            IEnumerable<TipoAtendimento> lista = Session.QueryOver<TipoAtendimento>()
                .OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaModalidade()
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

        public IEnumerable<ItemVO> BuildListaEtapa()
        {
            Etapa e = null;
            TipoEnsino te = null;
            Serie s = null;

            IEnumerable<Etapa> lista = Session.QueryOver<Etapa>(() => e)
                .Inner.JoinQueryOver<TipoEnsino>(() => e.TipoEnsino, () => te)
                .Inner.JoinQueryOver<Serie>(() => e.Serie, () => s)
                .OrderBy(() => te.Descricao)
                .Asc.ThenBy(() => s.Descricao)
                .Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.TipoEnsino.Descricao + " - " + x.Serie.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaEtapa(int idModalidade)
        {
            Etapa e = null;
            TipoEnsino te = null;
            Serie s = null;

            IEnumerable<Etapa> lista = Session.QueryOver<Etapa>(() => e)
                .Inner.JoinQueryOver<TipoEnsino>(() => e.TipoEnsino, () => te)
                .Inner.JoinQueryOver<Serie>(() => e.Serie, () => s)
                .Where(() => e.Modalidade.Id == idModalidade)
                .OrderBy(() => te.Descricao)
                .Asc.ThenBy(() => s.Descricao)
                .Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                //retorno.Add(new ItemVO { Id = x.Id, Descricao = x.TipoEnsino.Descricao + " - " + x.Serie.Descricao });
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.TipoEnsino.Descricao + " - " + x.Serie.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaProfissionaisPorDisciplina(int disciplinaId)
        {
            PessoaDisciplinaDAO pdao = new PessoaDisciplinaDAO();

            IEnumerable<PessoaVO> lista = pdao.GetListaPessoasByDisciplina(disciplinaId);
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                //retorno.Add(new ItemVO { Id = x.Id, Descricao = x.TipoEnsino.Descricao + " - " + x.Serie.Descricao });
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Nome });
            }
            return retorno;
        }


        public IEnumerable<ItemVO> BuildListaTipoSala()
        {
            IEnumerable<TipoSala> lista = Session.QueryOver<TipoSala>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }


        /*
        public IEnumerable<ItemVO> BuildListaCartorios()
        {
            IEnumerable<Cartorio> lista = Session.QueryOver<Cartorio>().OrderBy(x => x.Nome).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Nome });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaTiposLocaisObitos()
        {
            IEnumerable<TipoLocalObito> lista = Session.QueryOver<TipoLocalObito>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaCausasMortis()
        {

            IEnumerable<CausaMorte> lista = Session.QueryOver<CausaMorte>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }


        public IEnumerable<ItemVO> BuildListLotesPorSubgrupoId(int subgrupoId)
        {
            ItemVO i = null;

            IEnumerable<ItemVO> retorno =
                Session.QueryOver<Lote>()
                .Where(x => x.Subgrupo.Id == subgrupoId)
                .SelectList(list => list
                    .Select(x => x.Id).WithAlias(() => i.Id)
                    .Select(x => x.Descricao).WithAlias(() => i.Descricao)
                    ).TransformUsing(Transformers.AliasToBean<ItemVO>())
                    .List<ItemVO>()
                    .OrderBy(x => x.Descricao); // Deveria ter order by DescricaoId !!!!!!
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListGavetasPorLoteId(int loteId)
        {
            Lote l = null;
            TipoLote tl = null;

            List<ItemVO> lista = new List<ItemVO>();

            IList<int> retorno =
                Session.QueryOver<Lote>(() => l)
                .Inner.JoinQueryOver<TipoLote>(() => l.TipoLote, () => tl)
                .Where(x => l.Id == loteId)
                .Select(x => tl.Vagas)
                .List<int>();

            if (retorno.Count > 0)
            {
                int vagas = (int)retorno[0];
                for (int i = 1; i <= vagas; i++)
                {
                    lista.Add(new ItemVO { Id = i, Descricao = i.ToString() });
                }

            }
            return lista;
        }

        public IEnumerable<ItemVO> BuildListaGrupos()
        {
            ItemVO i = null;

            IEnumerable<ItemVO> retorno =
                Session.QueryOver<Grupo>()
                .SelectList(list => list
                    .Select(x => x.Id).WithAlias(() => i.Id)
                    .Select(x => x.Descricao).WithAlias(() => i.Descricao)
                    ).TransformUsing(Transformers.AliasToBean<ItemVO>())
                    .List<ItemVO>()
                    .OrderBy(x => x.Descricao);
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaCemiterios()
        {
            IEnumerable<Cemiterio> lista = Session.QueryOver<Cemiterio>().OrderBy(x => x.Nome).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Nome });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListGruposPorCemiterioId(int cemiterioId)
        {
            ItemVO i = null;

            IEnumerable<ItemVO> retorno =
                Session.QueryOver<Grupo>()
                .Where(x => x.Cemiterio.Id == cemiterioId)
                .SelectList(list => list
                    .Select(x => x.Id).WithAlias(() => i.Id)
                    .Select(x => x.Descricao).WithAlias(() => i.Descricao)
                    ).TransformUsing(Transformers.AliasToBean<ItemVO>())
                    .List<ItemVO>()
                    .OrderBy(x => x.Descricao);
            return retorno;
        }


        public IEnumerable<ItemVO> BuildListSubruposPorGrupoId(int grupoId)
        {
            ItemVO i = null;

            IEnumerable<ItemVO> retorno =
                Session.QueryOver<Subgrupo>()
                .Where(x => x.Grupo.Id == grupoId)
                .SelectList(list => list
                    .Select(x => x.Id).WithAlias(() => i.Id)
                    .Select(x => x.Descricao).WithAlias(() => i.Descricao)
                    ).TransformUsing(Transformers.AliasToBean<ItemVO>())
                    .List<ItemVO>()
                    .OrderBy(x => x.Descricao);
            return retorno;
        }
         */

        public IEnumerable<ItemVO> BuildListaEduUF()
        {
            IEnumerable<Estado> lista = Session.QueryOver<Estado>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaCidade()
        {
            IEnumerable<Municipio> lista = Session.QueryOver<Municipio>().OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

        public IEnumerable<ItemVO> BuildListaCidadeByUFId(int ufId)
        {
            IEnumerable<Municipio> lista = Session.QueryOver<Municipio>()
                .Where(x => x.Estado.Id == ufId)
                .OrderBy(x => x.Descricao).Asc.List();
            List<ItemVO> retorno = new List<ItemVO>();
            foreach (var x in lista)
            {
                retorno.Add(new ItemVO { Id = x.Id, Descricao = x.Descricao });
            }
            return retorno;
        }

 
    }
}

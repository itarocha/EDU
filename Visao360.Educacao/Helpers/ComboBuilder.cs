using Dardani.EDU.BO.App;
using Dardani.EDU.BO.NH;
using Dardani.EDU.Entities.VO;
using Petra.Util.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Visao360.Educacao.Helpers
{
    public class ComboBuilder
    {

        private static IEnumerable<SelectListItem> BuildLista(IEnumerable<ItemVO> lista)
        {
            List<SelectListItem> retorno = new List<SelectListItem>();
            retorno.Add(new SelectListItem { Value = "", Text = "------" });
            foreach (ItemVO i in lista)
            {
                retorno.Add(new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Descricao,
                    Selected = (i.Id.ToString() == "a")
                });
            }
            return retorno;
        }

        private static IEnumerable<SelectListItem> BuildLista(IEnumerable<ItemStringVO> lista)
        {
            List<SelectListItem> retorno = new List<SelectListItem>();
            retorno.Add(new SelectListItem { Value = "", Text = "------" });
            foreach (ItemStringVO i in lista)
            {
                retorno.Add(new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Descricao,
                    Selected = (i.Id.ToString() == "a")
                });
            }
            return retorno;
        }


        public static IEnumerable<SelectListItem> ListaDisciplinasByModalidadeEtapa(int modalidadeId, int etapaId)
        {
            MatrizDisciplinaDAO mdao = new MatrizDisciplinaDAO();
            IEnumerable<MatrizDisciplinaVO> listaDisciplinas = mdao.GetMatrizDisciplinaVOByModaliadeEtapa(modalidadeId, etapaId);

            List<SelectListItem> retorno = new List<SelectListItem>();
            retorno.Add(new SelectListItem { Value = "", Text = "------" });
            foreach (MatrizDisciplinaVO i in listaDisciplinas)
            {
                retorno.Add(new SelectListItem
                {
                    Value = i.DisciplinaId.ToString(),
                    Text = i.DisciplinaDescricao,
                    Selected = (i.Id.ToString() == "a")
                });
            }
            return retorno;
        }


        public static IEnumerable<SelectListItem> ListaSimNao()
        {
            return BuildLista(EDUListasBuilder.BuildListaSimNao());
        }

        public static IEnumerable<SelectListItem> ListaModalidade()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaModalidade());
        }

        public static IEnumerable<SelectListItem> ListaEtapa(int modalidadeId = 0)
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaEtapa()/*.BuildListaEtapa(modalidadeId)*/);
        }

        public static IEnumerable<SelectListItem> ListaPeriodoAula()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaPeriodoAula());
        }

        public static IEnumerable<SelectListItem> ListaTurno()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaTurno());
        }

        public static IEnumerable<SelectListItem> ListaTipoDia()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaTipoDia());
        }

        public static IEnumerable<SelectListItem> ListaDisciplinaEducacenso()
        {
            return BuildLista(ItemVOBuilders.Instance.BuidListaDisciplinaEducacenso());
        }

        public static IEnumerable<SelectListItem> ListaHorario()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaHorario());
        }


        public static IEnumerable<SelectListItem> ListaRaca()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaRaca());
        }

        public static IEnumerable<SelectListItem> ListaTipoSala()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaTipoSala());
        }

        public static IEnumerable<SelectListItem> ListaPais()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaPais());
        }

        public static IEnumerable<SelectListItem> ListaTipoNacionalidade()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaTipoNacionalidade());
        }

        public static IEnumerable<SelectListItem> ListaSituacaoDocumento()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaSituacaoDocumento());
        }
        
        public static IEnumerable<SelectListItem> ListaModeloCertidao()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaModeloCertidao());
        }

        public static IEnumerable<SelectListItem> ListaTipoCertidao()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaTipoCertidao());
        }

        public static IEnumerable<SelectListItem> ListaTipoAvaliacao()
        {
            return BuildLista(EDUListasBuilder.BuildListaTipoAvaliacao());
        }

        public static IEnumerable<SelectListItem> ListaDisciplinaCategoria()
        {
            return BuildLista(EDUListasBuilder.BuildListaDisciplinaCategoria());
        }

        public static IEnumerable<SelectListItem> ListaDisciplina()
        {
            return BuildLista(ItemVOBuilders.Instance.BuidListaDisciplina());
        }

        public static IEnumerable<SelectListItem> ListaSexo()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaSexo());
        }

        public static IEnumerable<SelectListItem> ListaEstadoCivil()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaEstadoCivil());
        }

        public static IEnumerable<SelectListItem> ListaTipoAtendimento()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaTipoAtendimento());
        }

        public static IEnumerable<SelectListItem> ListaCalendario(int escolaId, int ano)
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaCalendario(escolaId, ano));
        }

        public static IEnumerable<SelectListItem> ListaSala(int escolaId)
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaSala(escolaId));
        }

        public static IEnumerable<SelectListItem> ListaSituacaoFuncionamento()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaSituacaoFuncionamento());
        }

        public static IEnumerable<SelectListItem> ListaTipoAdministracao()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaTipoAdministracao());
        }

        public static IEnumerable<SelectListItem> ListaEstagioRegulamentacao()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaEstagioRegulamentacao());
        }

        public static IEnumerable<SelectListItem> ListaUF()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaUF());
        }

        public static IEnumerable<SelectListItem> ListaCidade()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaCidade());
        }

        public static IEnumerable<SelectListItem> ListaZona()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaZona());
        }

        public static IEnumerable<SelectListItem> ListaAEE()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaAEE());
        }

        public static IEnumerable<SelectListItem> ListaAtividadeComplementar()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaAtividadeComplementar());
        }

        public static IEnumerable<SelectListItem> ListaLocalizacaoDiferenciada()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaLocalizacaoDiferenciada());
        }

        public static IEnumerable<SelectListItem> ListaLinguaIndigena()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaLinguaIndigena());
        }

        public static IEnumerable<SelectListItem> ListaEscolarizacaoEspecial()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaEscolarizacaoEspecial());
        }

        public static IEnumerable<SelectListItem> ListaTransportePublico()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaTransportePublico());
        }

        public static IEnumerable<SelectListItem> ListaTurmaUnificada()
        {
            return BuildLista(ItemVOBuilders.Instance.BuildListaTurmaUnificada());
        }


    }
}
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visao360.Educacao.Filters;
using Visao360.Educacao.Helpers;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.BO.NH;
using Petra.Util.Model;
using Petra.Util.Funcoes;
using Dardani.EDU.Entities.VO;
using Visao360.Educacao.Models;
using Petra.DAO.Util;
using Dardani.EDU.BO.App;

namespace Visao360.Educacao.Controllers
{
    public class EtapasController : BaseController
    {
 
        // Etapas
        public ActionResult Index(){
            ModalidadeDAO mdao = new ModalidadeDAO();
            IEnumerable<EtapaVO> lista = mdao.GetListaEtapasVO();
            return View(lista);
        }

        // Etapa.Matrizes
        public ActionResult Matrizes(int etapaId)
        {
            // Se não existir, fudeu
            Etapa etapa = new EtapaDAO().GetById(etapaId);
            
            // Busca lista de Matrizes da Etapa

            MatrizDAO dao = new MatrizDAO();
            

            return View();
        }

        public ActionResult EditarMatriz(int etapaId, int matrizId = 0)
        {
            return View();
        }

        [HttpPost, ActionName("EditarMatriz")]
        public ActionResult EditarMatrizPost()
        {
            return View();
        }

        // Etapa.Matriz.Periodos
        public ActionResult MatrizPeriodos(int etapaId, int matrizId){
            return View();
        }

        // Etapa.Matriz.Periodo.Edit
        public ActionResult EditarMatrizPeriodo(int etapaId, int matrizId, int periodoId = 0)
        {
            return View();
        }

        [HttpPost, ActionName("EditarMatrizPeriodoPost")]
        public ActionResult EditarMatrizPeriodoPost()
        {
            return View();
        }

        // Etapa.Matriz.Disciplinas
        public ActionResult MatrizDisciplinas(int etapaId, int matrizId){
            return View();
        }

        // Etapa.Matriz.Disciplina.Edit
        public ActionResult EditarMatrizDisciplina(int etapaId, int matrizId, int disciplinaId = 0)
        {
            return View();
        }

        [HttpPost, ActionName("EditarMatrizDisciplinaPost")]
        public ActionResult EditarMatrizDisciplinaPost()
        {
            return View();
        }
        
        
        
        
        
        /*        
        
        [Acesso(AcaoId = "matrizes.index")]
        public ActionResult Index()
        {
            ModalidadeDAO mdao = new ModalidadeDAO();
            IEnumerable<EtapaVO> lista = mdao.GetListaEtapasVO();
            return View(lista);
        }

        private MatrizVO GetMatrizVO(int modalidadeId, int etapaId) {
            MatrizVO model = null;

            MatrizDAO dao = new MatrizDAO();
            model = dao.GetMatrizVOByAnoLetivoModalidadeEtapa(this.EscolaSessao.AnoLetivoId, modalidadeId, etapaId);
            if (model == null)
            {
                model = new MatrizVO() { ModalidadeId = modalidadeId, EtapaId = etapaId };
            }
            return model;
        }

        // Já selecionado
        [Acesso(AcaoId = "matrizes.matriz")]
        [SelecionouFilial(MensagemErro = "Para gerenciar Matrizes, selecione primeiro uma Escola Padrão.")]
        public ActionResult Matriz(int modalidadeId, int etapaId)
        {
            ViewData["modalidadeId"] = modalidadeId;
            ViewData["etapaId"] = etapaId;

            ModalidadeDAO mdao = new ModalidadeDAO();
            EtapaVO etapaVO = mdao.GetEtapaVO(modalidadeId, etapaId);
            ViewBag.EtapaVO = etapaVO;

            MatrizVO model = GetMatrizVO(modalidadeId, etapaId);

            IEnumerable<MatrizDisciplinaVO> listaDisciplinas = new List<MatrizDisciplinaVO>();
            if (model.Id > 0) {
                MatrizDisciplinaDAO mddao = new MatrizDisciplinaDAO();
                listaDisciplinas  = mddao.GetMatrizDisciplinaVOByMatriz(model.Id);
            }
            ViewBag.Disciplinas = listaDisciplinas;

            return View(model);
        }

        // Já selecionado
        [HttpPost, ActionName("Matriz")]
        [Persistencia]
        public ActionResult Matriz(MatrizVO model)
        {
            // Se não existir, insere, se existir, atualiza

            //int id = new Random().Next(5000);
            //model.Id = id; 

            MatrizDAO dao = new MatrizDAO();
            Matriz toSave = (model.Id == 0) ? new Matriz() : dao.GetById(model.Id);

            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            model.AnoLetivoId = e.AnoLetivoId;

            Conversor.Converter(model, toSave, NHibernateBase.Session);
            dao.SaveOrUpdate(toSave, toSave.Id);

            // Na verdade deve enviar para MatrizDisciplina/id/
            return Redirect(String.Format("/MatrizDisciplina/{0}/{1}", model.ModalidadeId, model.EtapaId));
        }

        // Já selecionado
        [SelecionouFilial(MensagemErro = "Para acessar Disciplinas da Matriz, selecione primeiro uma Escola Padrão.")]
        [Acesso(AcaoId = "matrizes.matrizdisciplina")]
        public ActionResult MatrizDisciplina(int modalidadeId, int etapaId, int identificacao = 0)
        {
            MatrizVO matrizVO = GetMatrizVO(modalidadeId, etapaId);

            if (matrizVO.Id == 0) {
                this.FlashMessage("É necesssário informar o cabeçalho da Matriz Curricular");
                return Redirect(String.Format("/Matriz/{0}/{1}", modalidadeId, etapaId));
            }

            ViewData["modalidadeId"] = modalidadeId;
            ViewData["etapaId"] = etapaId;

            ModalidadeDAO mdao = new ModalidadeDAO();
            EtapaVO etapaVO = mdao.GetEtapaVO(modalidadeId, etapaId);
            ViewBag.EtapaVO = etapaVO;

            EnviarViewBagMatrizDisciplina();

            // Se não existir, insere
            if (identificacao == 0)
            {
                ViewBag.Acao = "Incluir Disciplina na Matriz Curricular";
            }
            else
            {
                ViewBag.Acao = "Editar Disciplina na Matriz Curricular";
            }

            MatrizDisciplinaDAO dao = new MatrizDisciplinaDAO();
            MatrizDisciplinaVO model;

            if (identificacao == 0)
            {
                model = new MatrizDisciplinaVO()
                {
                    MatrizId = matrizVO.Id,
                    //DisciplinaId = disciplinaId,
                    FlagAceitaDispensa = "N", // Não
                    FlagCategoria = "N", // N = Base Nacional / D = Parte Diversificada
                    FlagReprova = "S", // Sim
                    FlagTipoAvaliacao = "N" // N = Nota; C = Conceito
                };
            }
            else {
                model = dao.GetMatrizDisciplinaVOById(identificacao);
            }
            return View(model);
        }

        private void EnviarViewBagMatrizDisciplina()
        {
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
            ViewBag.ListaTipoAvaliacao = ComboBuilder.ListaTipoAvaliacao();
            ViewBag.ListaDisciplinaCategoria = ComboBuilder.ListaDisciplinaCategoria();
            ViewBag.ListaDisciplina = ComboBuilder.ListaDisciplina();
        }

        // Já selecionado
        [HttpPost, ActionName("MatrizDisciplina")]
        [Persistencia]
        public ActionResult MatrizDisciplina(MatrizDisciplinaVO model)
        {
            // Se não existir, insere, se existir, atualiza
            Boolean novo = (model.Id == 0);

            if (!novo)
            {
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Nova Disciplina" : "Editar Disciplina";
                EnviarViewBagMatrizDisciplina();
                return View(model);
            }

            MatrizDisciplinaDAO dao = new MatrizDisciplinaDAO();
            MatrizDisciplina toSave = novo ? new MatrizDisciplina() : dao.GetById(model.Id);

            Conversor.Converter(model, toSave, NHibernateBase.Session);
            dao.SaveOrUpdate(toSave, toSave.Id);

            MatrizDAO mdao = new MatrizDAO();
            MatrizVO matriz = mdao.GetMatrizVOById(model.MatrizId);

            // Na verdade deve enviar para MatrizDisciplina/id/
            return Redirect(String.Format("/Matriz/{0}/{1}", matriz.ModalidadeId, matriz.EtapaId));
        }
         */ 
    }
}

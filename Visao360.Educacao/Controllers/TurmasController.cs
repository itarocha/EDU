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
using Dardani.EDU.BO.App;
using Visao360.Educacao.Models;
using Petra.DAO.Util;

namespace Visao360.Educacao.Controllers
{
    public class TurmasController : BaseController
    {
        [Acesso(AcaoId = "turmas.index")]
        [SelecionouFilial(MensagemErro = "Para acessar Turmas da Escola, selecione primeiro uma Escola Padrão.")]
        public ActionResult Index(string searchString)
        {
            // Modelo de seleção de Escola
            ViewBag.EscolaId = this.EscolaSessao.EscolaId;

            IEnumerable<TurmaVO> lista = new TurmaDAO()
                .GetListagemByEscolaAno(this.EscolaSessao.EscolaId, this.EscolaSessao.AnoLetivoAno);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        //[Acesso(AcaoId = "turmas.etapas")]
        public ActionResult Etapas(int modalidadeId)
        {
            IEnumerable<ItemVO> lista = ItemVOBuilders.Instance.BuildListaEtapa(modalidadeId);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        //[Acesso(AcaoId = "turmas.profissionaispordisciplina")]
        public ActionResult ProfissionaisPorDisciplina(int disciplinaId)
        {
            IEnumerable<ItemVO> lista = ItemVOBuilders.Instance.BuildListaProfissionaisPorDisciplina(disciplinaId);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        /*
        [Role(Roles = "Administrador,Visitante")]
        public ActionResult OldEtapas(int modalidadeId)
        {
            IEnumerable<ItemVO> lista = ItemVOBuilders.Instance.BuildListaEtapa(modalidadeId);
            string html = "";
            foreach(ItemVO i in lista){
                html = html + "<option value='" + i.Id + "'>" + i.Descricao + "</option>";
            }
            return this.Content(html, "text/html", System.Text.Encoding.UTF8);
        }
        */

        [Acesso(AcaoId = "turmas.view")]
        public ActionResult View(int id = 0)
        {
            TurmaVO model = null;
            TurmaDAO dao = new TurmaDAO();
            
            model = dao.GetVOById(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.Acao = "Movimentação de Turma";
            return View(model);
        }

        [Acesso(AcaoId = "turmas.quadrohorarios")]
        [SelecionouFilial(MensagemErro = "Para acessar Quadro de Horários, selecione primeiro uma Escola Padrão.")]
        public ActionResult QuadroHorarios(int id = 0)
        {
            TurmaVO model = null;
            TurmaDAO dao = new TurmaDAO();

            model = dao.GetVOById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            
            // Pode ser que não tenha turma !!!
            Quadro q = new Quadro(id);


            ViewBag.ListaDisciplina = ComboBuilder.ListaDisciplinasByModalidadeEtapa(model.ModalidadeId, model.EtapaId, true);
            /*
            List<Celula> celulas = new List<Celula>();
            celulas.Add(new Celula() { CabecalhoId = 2, HorarioId = 2, Nome = "Academia" });
            celulas.Add(new Celula() { CabecalhoId = 2, HorarioId = 3, Nome = "Musculação" });
            celulas.Add(new Celula() { CabecalhoId = 3, HorarioId = 3, Nome = "Academia" });
            celulas.Add(new Celula() { CabecalhoId = 1, HorarioId = 1, Nome = "Ginástica" });
            celulas.Add(new Celula() { CabecalhoId = 1, HorarioId = 4, Nome = "Dança" });
            
            q.Celulas = celulas;
            */

            //ViewBag.Linhas = 5;
            //ViewBag.Colunas = 8;
            //ViewBag.Cabecalho = new string[] { "a", "b", "c", "d", "e" };

            //TurnoPeriodoDAO tpdao = new TurnoPeriodoDAO();
            //IEnumerable<TurnoPeriodoVO> periodos = tpdao.GetByTurnoId(id);
            //q.Periodos = periodos;

            
            ViewBag.Turma = model;

            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();

            IEnumerable<SelectListItem> listaModalidade = ComboBuilder.ListaModalidade();
            ViewBag.ListaModalidade = listaModalidade;

            ViewBag.Acao = "Edição de Quadro de Horários";

            //ViewBag.Linhas = 4;
            //ViewBag.Colunas = 8;
            //ViewBag.Cabecalho = Json(new String[] { "--", "DOM", "SEG", "TER", "QUA", "QUI", "SEX", "SAB" });
            //ViewBag.Cabecalho = new String[] { "--", "DOM", "SEG", "TER", "QUA", "QUI", "SEX", "SAB" };
            return View(q);
        }

        [HttpPost]
        [ActionName("QuadroHorarios")]
        public ActionResult QuadroRetornoConfirmed(RetornoQuadro r)
        {
            // O formato de "horarios" é "f_d" onde f = faixa; "_" = delimitador; d = dia da semana (Domingo = 1.. Sábado = 7)
            string[] x = r.horarios;
            string[] horarios;
            int periodoAulaId;
            short diaSemana;

            TurmaHorarioDAO thdao = new TurmaHorarioDAO();
            TurmaHorarioVO thvo = new TurmaHorarioVO(); // aToSave = novo ? new Turma() : adao.GetById(model.Id);
            TurmaHorario th;
            //Conversor.Converter(model, aToSave, NHibernateBase.Session);
            // Grava

            if ( (r.horarios != null) && (r.horarios.Length > 0) ) {
                for (int i = 0; i <= r.horarios.Length - 1; i++)
                {
                    horarios = r.horarios[i].Split('_');
                    //horario.Split("_");
                    if (horarios.Length == 2) {
                        if (!int.TryParse(horarios[0], out periodoAulaId)) { periodoAulaId = 0; }
                        if (!short.TryParse(horarios[1], out diaSemana)) { diaSemana = 0; }

                        IEnumerable<TurmaHorario> listaToDelete = thdao.GetListagemByPeriodoAulaDiaSemana(periodoAulaId, diaSemana);
                        if (listaToDelete != null)
                        {
                            foreach (TurmaHorario xis in listaToDelete)
                            {
                                thdao.Delete(xis);
                            }
                        }



                        if (r.DisciplinaId > 0)
                        {
                            th = new TurmaHorario();
                            thvo = new TurmaHorarioVO();
                            thvo.TurmaId = r.TurmaId;
                            thvo.DisciplinaId = r.DisciplinaId;
                            thvo.PessoaId = r.PessoaId;
                            thvo.FlagDiaSemana = diaSemana;
                            thvo.PeriodoAulaId = periodoAulaId;

                            Conversor.Converter(thvo, th, NHibernateBase.Session);

                            thdao.SaveOrUpdate(th, 0);
                        }
                    }

                    //Console.WriteLine(r.horarios[i]);
                }
            }
            return Redirect(string.Format("/Turmas/QuadroHorarios/{0}",r.TurmaId));
        }

        [Acesso(AcaoId = "turmas.edit")]
        [SelecionouFilial(MensagemErro = "Para Editar Turma, selecione primeiro uma Escola Padrão.")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);
            TurmaVO model = null;
            TurmaDAO dao = new TurmaDAO();

            if (novo)
            {
                model = new TurmaVO();
                model.EscolaId = this.EscolaSessao.EscolaId;
            }
            else
            {
                model = dao.GetVOById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }
            EnviarViewBagTurmas();

            ViewBag.Acao = novo ? "Nova Turma" : "Editar Turma";
            return View(model);
        }

        private void EnviarViewBagTurmas(){
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();

            ViewBag.ListaCalendario = ComboBuilder.ListaCalendario(e.EscolaId, e.AnoLetivoAno);
            ViewBag.ListaSala = ComboBuilder.ListaSala(e.EscolaId);
            ViewBag.ListaTurno = ComboBuilder.ListaTurno(); // Deverá ser apenas os da escola!!!!!!!! e.EscolaId
            ViewBag.ListaHorario = ComboBuilder.ListaHorario();
            ViewBag.ListaModalidade = ComboBuilder.ListaModalidade();
            ViewBag.ListaEtapa = ComboBuilder.ListaEtapa();
            ViewBag.ListaTipoAtendimento = ComboBuilder.ListaTipoAtendimento();
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
        }

        [HttpPost, ActionName("Edit")]
        [Persistencia]
        public ActionResult EditConfirmed(TurmaVO model)
        {
            model.FlagDomingo = model.CkbDom == "S" ? "S" : "N";
            model.FlagSegunda = model.CkbSeg == "S" ? "S" : "N";
            model.FlagTerca = model.CkbTer == "S" ? "S" : "N";
            model.FlagQuarta = model.CkbQua == "S" ? "S" : "N";
            model.FlagQuinta = model.CkbQui == "S" ? "S" : "N";
            model.FlagSexta = model.CkbSex == "S" ? "S" : "N";
            model.FlagSabado = model.CkbSab == "S" ? "S" : "N";

            // Verifica se é novo
            Boolean novo = (model.Id == 0);

            // Se não é válido, retorna
            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Nova Turma" : "Editar Turma";
                EnviarViewBagTurmas();
                return View(model);
            }

            // Pega o repositório
            TurmaDAO adao = new TurmaDAO();
            Turma aToSave = novo ? new Turma() : adao.GetById(model.Id);

            // Converte VO para Entity
            Conversor.Converter(model, aToSave, NHibernateBase.Session);

            // Grava
            adao.SaveOrUpdate(aToSave, aToSave.Id);

            // Redireciona
            return RedirectToAction("Index");
        }

        [Acesso(AcaoId = "turmas.delete")]
        [SelecionouFilial(MensagemErro = "Para Excluir Turma, selecione primeiro uma Escola Padrão.")]
        public ActionResult Delete(int Id)
        {
            TurmaDAO dao = new TurmaDAO();
            Turma model = dao.GetById(Id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [Persistencia]
        public ActionResult DeleteConfirmed(int id)
        {
            /*
            bool existe = new LoteDAO().ExisteLotePorEnsinoId(id);

            if (existe)
            {
                ModelState.AddModelError("Id", "Existem Lotes para esse Tipo de Lote. Exclusão não permitida.");
            }
            */
            TurmaDAO dao = new TurmaDAO();
            if (ModelState.IsValid)
            {
                Turma o = dao.GetById(id);
                string nome = o.Nome;
                dao.Delete(o);
                FlashMessage(string.Format("Turma \"{0}\" excluída com sucesso", nome));
                return RedirectToAction("Index");
            }
            Turma model = dao.GetById(id);
            return View(model);
        }

        [Acesso(AcaoId = "turmas.horarios")]
        [SelecionouFilial(MensagemErro = "Para acessar Horários de Turma, selecione primeiro uma Escola Padrão.")]
        public ActionResult Horarios(int turmaId)
        {
            TurmaDAO tdao = new TurmaDAO();
            Turma turma = tdao.GetById(turmaId);
            if (turma == null)
            {
                return HttpNotFound();
            }
            ViewBag.TurmaId = turma.Id;

            /*
            // Se escola for 0, redirecionar para index de Escolas e enviar mensagem
            if (escolaId == 0)
            {
                FlashMessage("Para gerenciar Turmas é necessário tornar uma Escola Padrão");
                return RedirectToAction("Selecionar","Home"); // RedirectToAction("Index", controllerName: "Escolas");
            }
            */
            //Busca de Matriz : 
            //turma.Turno.Id
            //turma.Modalidade.Id
            //turma.Etapa.Id
            //e.AnoLetivoId
            
            MatrizDAO matrizDAO = new MatrizDAO();
            MatrizVO matrizVO = matrizDAO.GetMatrizVOByAnoLetivoModalidadeEtapa(this.EscolaSessao.AnoLetivoId, 
                turma.Modalidade.Id, turma.Etapa.Id);
            ViewBag.MatrizVO = matrizVO;

            return View(turma);
        }

        [Acesso(AcaoId = "turmas.alunos")]
        [SelecionouFilial(MensagemErro = "Para acessar Alunos da Turma, selecione primeiro uma Escola Padrão.")]
        public ActionResult Alunos(int turmaId)
        {
            TurmaDAO tdao = new TurmaDAO();
            Turma turma = tdao.GetById(turmaId);
            if (turma == null)
            {
                return HttpNotFound();
            }
            ViewBag.Turma = turma;

            TurmaDAO dao = new TurmaDAO();
            Turma t = dao.GetById(turmaId);

            MatriculaDAO mdao = new MatriculaDAO();
            IEnumerable<MatriculaVO> lista = mdao.GetListaMatriculasPorTurma(turmaId);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListagemAlunos", lista);
            }
            return View(lista);
        }
   }
}
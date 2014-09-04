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
using Visao360.Educacao.Models;
using Dardani.EDU.Entities.VO;
using Petra.Util.Model;
using Petra.Util.Funcoes;
using Petra.DAO.Util;

namespace Visao360.Educacao.Controllers
{
    public class HorariosController : BaseController
    {
        //
        // GET: /Funerarias/
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            int escolaId = (e == null) ? 0 : e.EscolaId;

            // Se escola for 0, redirecionar para index de Escolas e enviar mensagem
            if (escolaId == 0)
            {
                TempData["mensagem"] = "Para gerenciar Calendários é necessário tornar uma Escola Padrão";
                return RedirectToAction("Selecionar", "Home"); // RedirectToAction("Index", controllerName: "Escolas");
            }
            ViewBag.EscolaId = escolaId;
            IEnumerable<Horario> lista = new HorarioDAO().GetListagemByEscolaId(escolaId);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        [Role(Roles = "Administrador")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);
            HorarioVO model = novo ? new HorarioVO() : new HorarioDAO().GetVOById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            if (novo){
                EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
                //////////model.EscolaId = e.EscolaId;
            }

            ViewBag.Acao = novo ? "Novo Horario" : "Editar Horario";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(HorarioVO model)
        {
            Boolean novo = (model.Id == 0);
            if (!novo){}

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Horário" : "Editar Horário";

                return View(model);
            }
            HorarioDAO dao = new HorarioDAO();
            Horario toSave = novo ? new Horario() : dao.GetById(model.Id);
            Conversor.Converter(model, toSave, NHibernateBase.Session); //
            dao.SaveOrUpdate(toSave, toSave.Id);
            return RedirectToAction("Index");
        }

        // Horários
        [Role(Roles = "Administrador")]
        // id = HorarioId
        public ActionResult Periodos(int id)
        {
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            int escolaId = (e == null) ? 0 : e.EscolaId;

            Horario Horario = new HorarioDAO().GetById(id);
            @ViewBag.Horario = Horario;

            // Se escola for 0, redirecionar para index de Escolas e enviar mensagem
            if (escolaId == 0)
            {
                TempData["mensagem"] = "Para gerenciar Períodos é necessário tornar uma Escola Padrão";
                return RedirectToAction("Selecionar", "Home"); // RedirectToAction("Index", controllerName: "Escolas");
            }

            if (Horario == null)
            {
                return HttpNotFound();
            }

            IEnumerable<HorarioPeriodoVO> lista = new HorarioPeriodoDAO().GetByHorarioId(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        [Role(Roles = "Administrador")]
        public ActionResult PeriodoEdit(int horarioId, int periodoId = 0)
        {
            Boolean novo = (periodoId == 0);
            HorarioPeriodoVO model = null;
            HorarioPeriodoDAO dao = new HorarioPeriodoDAO();

            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            int escolaId = (e == null) ? 0 : e.EscolaId;

            if (novo)
            {
                model = new HorarioPeriodoVO();
                model.HorarioId = horarioId;
                //model.EscolaId = escolaId;
            }
            else
            {
                model = dao.GetVOById(horarioId);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }

            Horario Horario = new HorarioDAO().GetById(horarioId);
            @ViewBag.Horario = Horario;

            EnviarViewBagHorarios();

            ViewBag.Acao = novo ? "Novo Período" : "Editar Período";
            return View(model);
        }

        private void EnviarViewBagHorarios()
        {
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            int escolaId = (e == null) ? 0 : e.EscolaId;
            ViewBag.ListaPeriodoAula = ComboBuilder.ListaPeriodoAula();
            //ViewBag.ListaHorarios = ComboBuilder.ListaHorario(escolaId);
        }


        [HttpPost, ActionName("PeriodoEdit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult PeriodoEditConfirmed(HorarioPeriodoVO model)
        {
            Boolean novo = (model.Id == 0);

            if (!novo)
            {
                /*
                int maximoSepultados = new EnsinoDAO().GetMaximoSepultadosPorEnsinoId(model.Id);
                if (maximoSepultados > model.Vagas)
                {
                    ModelState.AddModelError("EnsinoId", String.Format("Existem Lotes com esse tipo que possuem sepultados com quantidade superior " +
                        "ao digitado abaixo. Sepultados: {0}, Vagas: {1}", maximoSepultados, model.Vagas));
                }
                 */
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Período" : "Editar Período";

                Horario Horario = new HorarioDAO().GetById(model.HorarioId);
                @ViewBag.Horario = Horario;

                EnviarViewBagHorarios();
                return View(model);
            }

            HorarioDAO tdao = new HorarioDAO();
            PeriodoAulaDAO padao = new PeriodoAulaDAO();

            HorarioPeriodoDAO dao = new HorarioPeriodoDAO();
            HorarioPeriodo toSave = novo ? new HorarioPeriodo() : dao.GetById(model.Id);
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            //model.EscolaId = e.EscolaId;

            Conversor.Converter(model, toSave, NHibernateBase.Session); //

            //toSave.Horario = tdao.GetById(model.HorarioId);
            //toSave.PeriodoAula = padao.GetById(model.PeriodoAulaId);
            //toSave.Escola = e;

            dao.SaveOrUpdate(toSave, toSave.Id);


            return Redirect(String.Format("/PeriodosHorario/{0}", model.HorarioId));
        }













        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            HorarioDAO dao = new HorarioDAO();
            Horario model = dao.GetById(Id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult DeleteConfirmed(int id)
        {
            /*
            bool existe = new LoteDAO().ExisteLotePorHorarioId(id);

            if (existe)
            {
                ModelState.AddModelError("Id", "Existem Lotes para esse Tipo de Lote. Exclusão não permitida.");
            }
            */
            HorarioDAO dao = new HorarioDAO();
            if (ModelState.IsValid)
            {
                Horario o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                TempData["mensagem"] = string.Format("Horario \"{0}\" excluído com sucesso", descricao);
                return RedirectToAction("Index");
            }
            Horario model = dao.GetById(id);
            return View(model);
        }
    }
}


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
    public class TurnosController : BaseController
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
            IEnumerable<Turno> lista = new TurnoDAO().GetListagemByEscolaId(escolaId);
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
            TurnoVO model = novo ? new TurnoVO() : new TurnoDAO().GetVOById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            if (novo){
                EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
                //////////model.EscolaId = e.EscolaId;
            }

            ViewBag.Acao = novo ? "Novo Turno" : "Editar Turno";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(TurnoVO model)
        {
            Boolean novo = (model.Id == 0);
            if (!novo){}

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Turno" : "Editar Turno";

                return View(model);
            }
            TurnoDAO dao = new TurnoDAO();
            Turno toSave = novo ? new Turno() : dao.GetById(model.Id);
            Conversor.Converter(model, toSave, NHibernateBase.Session); //
            dao.SaveOrUpdate(toSave, toSave.Id);
            return RedirectToAction("Index");
        }

        private void EnviarViewBagHorarios()
        {
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            int escolaId = (e == null) ? 0 : e.EscolaId;
            ViewBag.ListaPeriodoAula = ComboBuilder.ListaPeriodoAula();
            //ViewBag.ListaTurnos = ComboBuilder.ListaTurno(escolaId);
        }






        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            TurnoDAO dao = new TurnoDAO();
            Turno model = dao.GetById(Id);

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
            bool existe = new LoteDAO().ExisteLotePorTurnoId(id);

            if (existe)
            {
                ModelState.AddModelError("Id", "Existem Lotes para esse Tipo de Lote. Exclusão não permitida.");
            }
            */
            TurnoDAO dao = new TurnoDAO();
            if (ModelState.IsValid)
            {
                Turno o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                TempData["mensagem"] = string.Format("Turno \"{0}\" excluído com sucesso", descricao);
                return RedirectToAction("Index");
            }
            Turno model = dao.GetById(id);
            return View(model);
        }
    }
}


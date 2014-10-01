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

namespace Visao360.Educacao.Controllers
{
    public class PeriodosAnosController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            IEnumerable<PeriodoAno> lista = new PeriodoAnoDAO().GetListagem(searchString);
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

            PeriodoAno model = novo ? new PeriodoAno() : new PeriodoAnoDAO().GetById(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            EnviarViewBagEdit();
            ViewBag.Acao = novo ? "Novo Período do Ano" : "Editar Período do Ano";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(PeriodoAno model)
        {
            Boolean novo = (model.Id == 0);
            PeriodoAnoDAO dao = new PeriodoAnoDAO();
            if (!novo)
            {
                EnviarViewBagEdit();
                /*
                if (dao.PossuiHorarioPeriodo(model.Id)) {
                    ModelState.AddModelError("Id", "Período do Ano não pode ser alterado porque já está sendo utilizado.");
                }
                 */ 
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Período do Ano" : "Editar Período do Ano";

                return View(model);
            }

            dao.SaveOrUpdate(model, model.Id);
            return RedirectToAction("Index");
        }

        private void EnviarViewBagEdit()
        {
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            PeriodoAnoDAO dao = new PeriodoAnoDAO();
            PeriodoAno model = dao.GetById(Id);

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
            string mensagemRetorno;
            bool pode = new PeriodoAnoDAO().PodeExcluir(id, out mensagemRetorno);
            if (!pode)
            {
                ModelState.AddModelError("Id", mensagemRetorno);
            }

            PeriodoAnoDAO dao = new PeriodoAnoDAO();
            if (ModelState.IsValid)
            {
                PeriodoAno o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                this.FlashMessage(string.Format("Período do Ano \"{0}\" excluído com sucesso", descricao));
                return RedirectToAction("Index");
            }
            PeriodoAno model = dao.GetById(id);
            return View(model);
        }
    }
}
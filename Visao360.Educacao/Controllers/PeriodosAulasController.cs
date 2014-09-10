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
    public class PeriodosAulasController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            IEnumerable<PeriodoAula> lista = new PeriodoAulaDAO().GetListagem(searchString);
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

            PeriodoAula model = novo ? new PeriodoAula() : new PeriodoAulaDAO().GetById(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.Acao = novo ? "Novo Período de Aula" : "Editar Período de Aula";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(PeriodoAula model)
        {
            Boolean novo = (model.Id == 0);
            PeriodoAulaDAO dao = new PeriodoAulaDAO();
            if (!novo)
            {
                if (dao.PossuiHorarioPeriodo(model.Id)) {
                    ModelState.AddModelError("Id", "Período de Aula não pode ser alterado porque já está sendo utilizado.");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Período de Aula" : "Editar Período de Aula";

                return View(model);
            }

            
            dao.SaveOrUpdate(model, model.Id);
            return RedirectToAction("Index");
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            PeriodoAulaDAO dao = new PeriodoAulaDAO();
            PeriodoAula model = dao.GetById(Id);

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
            bool pode = new PeriodoAulaDAO().PodeExcluir(id, out mensagemRetorno);
            if (!pode)
            {
                ModelState.AddModelError("Id", mensagemRetorno);
            }

            PeriodoAulaDAO dao = new PeriodoAulaDAO();
            if (ModelState.IsValid)
            {
                PeriodoAula o = dao.GetById(id);
                string descricao = o.HoraInicio + " - "+o.HoraTermino;

                dao.Delete(o);

                this.FlashMessage(string.Format("Período \"{0}\" excluído com sucesso", descricao));
                return RedirectToAction("Index");
            }
            PeriodoAula model = dao.GetById(id);
            return View(model);
        }
    }
}


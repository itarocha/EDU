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

namespace Visao360.Educacao.Controllers
{
    public class AnosLetivosController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index()
        {
            IEnumerable<AnoLetivo> lista = new AnoLetivoDAO().GetListagem();
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

            AnoLetivo model = novo ? new AnoLetivo() : new AnoLetivoDAO().GetById(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            if (novo) {
                model.FlagStatus = "S";
            }

            ViewBag.Acao = novo ? "Novo Ano Letivo" : "Editar Ano Letivo";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(AnoLetivo model)
        {
            Boolean novo = (model.Id == 0);

            if (!novo)
            {
                /*
                int maximoSepultados = new TurnoDAO().GetMaximoSepultadosPorTurnoId(model.Id);
                if (maximoSepultados > model.Vagas)
                {
                    ModelState.AddModelError("TurnoId", String.Format("Existem Lotes com esse tipo que possuem sepultados com quantidade superior " +
                        "ao digitado abaixo. Sepultados: {0}, Vagas: {1}", maximoSepultados, model.Vagas));
                }
                 */
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Ano Letivo" : "Editar Ano Letivo";

                return View(model);
            }

            AnoLetivoDAO dao = new AnoLetivoDAO();
            dao.SaveOrUpdate(model, model.Id);
            return RedirectToAction("Index");
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            AnoLetivoDAO dao = new AnoLetivoDAO();
            AnoLetivo model = dao.GetById(Id);

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
            AnoLetivoDAO dao = new AnoLetivoDAO();
            if (ModelState.IsValid)
            {
                AnoLetivo o = dao.GetById(id);
                int ano =  o.Ano;

                dao.Delete(o);

                TempData["mensagem"] = string.Format("Ano Letivo \"{0}\" excluído com sucesso", ano);
                return RedirectToAction("Index");
            }
            AnoLetivo model = dao.GetById(id);
            return View(model);
        }
    }
}


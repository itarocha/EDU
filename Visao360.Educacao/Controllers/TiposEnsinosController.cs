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
    public class TiposEnsinosController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            IEnumerable<TipoEnsino> lista = new TipoEnsinoDAO().GetListagem(searchString);
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

            TipoEnsino model = novo ? new TipoEnsino() : new TipoEnsinoDAO().GetById(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.Acao = novo ? "Novo Tipo de Ensino" : "Editar Tipo de Ensino";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(TipoEnsino model)
        {
            Boolean novo = (model.Id == 0);

            if (!novo)
            {
                /*
                int maximoSepultados = new TipoEnsinoDAO().GetMaximoSepultadosPorTipoEnsinoId(model.Id);
                if (maximoSepultados > model.Vagas)
                {
                    ModelState.AddModelError("TipoEnsinoId", String.Format("Existem Lotes com esse tipo que possuem sepultados com quantidade superior " +
                        "ao digitado abaixo. Sepultados: {0}, Vagas: {1}", maximoSepultados, model.Vagas));
                }
                 */
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Tipo de Ensino" : "Editar Tipo de Ensino";

                return View(model);
            }

            TipoEnsinoDAO dao = new TipoEnsinoDAO();
            dao.SaveOrUpdate(model, model.Id);
            return RedirectToAction("Index");
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            TipoEnsinoDAO dao = new TipoEnsinoDAO();
            TipoEnsino model = dao.GetById(Id);

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
            bool existe = new LoteDAO().ExisteLotePorTipoEnsinoId(id);

            if (existe)
            {
                ModelState.AddModelError("Id", "Existem Lotes para esse Tipo de Lote. Exclusão não permitida.");
            }
            */
            TipoEnsinoDAO dao = new TipoEnsinoDAO();
            if (ModelState.IsValid)
            {
                TipoEnsino o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                this.FlashMessage(string.Format("Tipo de Ensino \"{0}\" excluído com sucesso", descricao));
                return RedirectToAction("Index");
            }
            TipoEnsino model = dao.GetById(id);
            return View(model);
        }
    }
}


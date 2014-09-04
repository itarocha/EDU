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

namespace Visao360.Educacao.Controllers
{
    public class TiposEventosController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            IEnumerable<TipoEvento> lista = new TipoEventoDAO().GetListagem();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        private void EnviarViewBagEdit()
        {
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
        }

        [Role(Roles = "Administrador")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);

            TipoEvento model = novo ? new TipoEvento() : new TipoEventoDAO().GetById(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            EnviarViewBagEdit();
            ViewBag.Acao = novo ? "Novo Tipo de Evento" : "Editar Tipo de Evento";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(TipoEvento model)
        {
            Boolean novo = (model.Id == 0);

            if (!novo)
            {
                /*
                int maximoSepultados = new TipoEventoDAO().GetMaximoSepultadosPorTipoEventoId(model.Id);
                if (maximoSepultados > model.Vagas)
                {
                    ModelState.AddModelError("TipoEventoId", String.Format("Existem Lotes com esse tipo que possuem sepultados com quantidade superior " +
                        "ao digitado abaixo. Sepultados: {0}, Vagas: {1}", maximoSepultados, model.Vagas));
                }
                 */
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Tipo de Evento" : "Editar Tipo de Evento";
                EnviarViewBagEdit();
                return View(model);
            }

            TipoEventoDAO dao = new TipoEventoDAO();
            dao.SaveOrUpdate(model, model.Id);
            return RedirectToAction("Index");
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            TipoEventoDAO dao = new TipoEventoDAO();
            TipoEvento model = dao.GetById(Id);

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
            bool existe = new LoteDAO().ExisteLotePorTipoEventoId(id);

            if (existe)
            {
                ModelState.AddModelError("Id", "Existem Lotes para esse Tipo de Lote. Exclusão não permitida.");
            }
            */
            TipoEventoDAO dao = new TipoEventoDAO();
            if (ModelState.IsValid)
            {
                TipoEvento o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                this.FlashMessage(string.Format("Tipo de Evento \"{0}\" excluído com sucesso", descricao));
                return RedirectToAction("Index");
            }
            TipoEvento model = dao.GetById(id);
            return View(model);
        }
    }
}


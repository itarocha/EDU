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
    public class TiposDiasController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            IEnumerable<TipoDia> lista = new TipoDiaDAO().GetListagem();
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

            TipoDia model = novo ? new TipoDia() : new TipoDiaDAO().GetById(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            EnviarViewBagEdit();
            ViewBag.Acao = novo ? "Novo Tipo de Dia" : "Editar Tipo de Dia";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(TipoDia model)
        {
            Boolean novo = (model.Id == 0);

            if (!novo)
            {
                /*
                int maximoSepultados = new TipoDiaDAO().GetMaximoSepultadosPorTipoDiaId(model.Id);
                if (maximoSepultados > model.Vagas)
                {
                    ModelState.AddModelError("TipoDiaId", String.Format("Existem Lotes com esse tipo que possuem sepultados com quantidade superior " +
                        "ao digitado abaixo. Sepultados: {0}, Vagas: {1}", maximoSepultados, model.Vagas));
                }
                 */
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Tipo de Dia" : "Editar Tipo de Dia";
                EnviarViewBagEdit();
                return View(model);
            }

            TipoDiaDAO dao = new TipoDiaDAO();
            dao.SaveOrUpdate(model, model.Id);
            return RedirectToAction("Index");
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            TipoDiaDAO dao = new TipoDiaDAO();
            TipoDia model = dao.GetById(Id);

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
            bool pode = new TipoDiaDAO().PodeExcluir(id, out mensagemRetorno);
            if (!pode)
            {
                ModelState.AddModelError("Id", mensagemRetorno);
            }
            
            TipoDiaDAO dao = new TipoDiaDAO();
            if (ModelState.IsValid)
            {
                TipoDia o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                this.FlashMessage(string.Format("Tipo de Dia \"{0}\" excluído com sucesso", descricao));
                return RedirectToAction("Index");
            }
            TipoDia model = dao.GetById(id);
            return View(model);
        }
    }
}
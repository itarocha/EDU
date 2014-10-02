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
using Dardani.EDU.BO.App;

namespace Visao360.Educacao.Controllers
{
    public class TiposSalasController : BaseController
    {
        [Acesso(AcaoId = "tipossalas.index")]
        public ActionResult Index(string searchString)
        {
            IEnumerable<TipoSala> lista = new TipoSalaDAO().GetListagem(searchString);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        [Acesso(AcaoId = "tipossalas.edit")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);

            TipoSala model = novo ? new TipoSala() : new TipoSalaDAO().GetById(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            IEnumerable<ItemStringVO> listaSimNao = EDUListasBuilder.BuildListaSimNao();

            EnviarViewBagEdit();

            ViewBag.Acao = novo ? "Novo Tipo de Sala" : "Editar Tipo de Sala";
            return View(model);
        }

        private void EnviarViewBagEdit()
        {
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
        }

        [HttpPost, ActionName("Edit")]
        [Persistencia]
        public ActionResult EditConfirmed(TipoSala model)
        {
            Boolean novo = (model.Id == 0);

            if (!novo)
            {
                /*
                int maximoSepultados = new TipoSalaDAO().GetMaximoSepultadosPorTipoSalaId(model.Id);
                if (maximoSepultados > model.Vagas)
                {
                    ModelState.AddModelError("TipoSalaId", String.Format("Existem Lotes com esse tipo que possuem sepultados com quantidade superior " +
                        "ao digitado abaixo. Sepultados: {0}, Vagas: {1}", maximoSepultados, model.Vagas));
                }
                 */
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Tipo de Sala" : "Editar Tipo de Sala";
                EnviarViewBagEdit();
                return View(model);
            }

            TipoSalaDAO dao = new TipoSalaDAO();
            dao.SaveOrUpdate(model, model.Id);
            return RedirectToAction("Index");
        }

        [Acesso(AcaoId = "tipossalas.delete")]
        public ActionResult Delete(int Id)
        {
            TipoSalaDAO dao = new TipoSalaDAO();
            TipoSala model = dao.GetById(Id);

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
            string mensagemRetorno;
            bool pode = new TipoSalaDAO().PodeExcluir(id, out mensagemRetorno);
            if (!pode)
            {
                ModelState.AddModelError("Id", mensagemRetorno);
            }

            TipoSalaDAO dao = new TipoSalaDAO();
            if (ModelState.IsValid)
            {
                TipoSala o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                this.FlashMessage(string.Format("Tipo de Sala \"{0}\" excluído com sucesso", descricao));
                return RedirectToAction("Index");
            }
            TipoSala model = dao.GetById(id);
            return View(model);
        }
    }
}
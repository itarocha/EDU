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
using Visao360.Educacao.Models;
using Petra.DAO.Util;

namespace Visao360.Educacao.Controllers
{
    public class SalasController : BaseController
    {
        [Acesso(AcaoId = "salas.index")]
        [SelecionouFilial(MensagemErro = "Para gerenciar Salas, selecione primeiro uma Escola Padrão.")]
        public ActionResult Index(string searchString)
        {   
            IEnumerable<Sala> lista = new SalaDAO().GetListagemByEscolaId(this.EscolaSessao.EscolaId);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        [Acesso(AcaoId = "salas.edit")]
        [SelecionouFilial(MensagemErro = "Para Editar Salas, selecione primeiro uma Escola Padrão.")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);
            SalaVO model = null;
            SalaDAO dao = new SalaDAO();

            if (novo)
            {
                model = new SalaVO();
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
            ViewBag.ListaTipoSala = ComboBuilder.ListaTipoSala();
            ViewBag.Acao = novo ? "Nova Sala" : "Editar Sala";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Persistencia]
        public ActionResult EditConfirmed(SalaVO model)
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
                ViewBag.Acao = novo ? "Nova Sala" : "Editar Sala";
                ViewBag.ListaTipoSala = ComboBuilder.ListaTipoSala();
                return View(model);
            }

            TipoSalaDAO tdao = new TipoSalaDAO();
            SalaDAO dao = new SalaDAO();
            Sala toSave = novo ? new Sala() : dao.GetById(model.Id);

            
            Conversor.Converter(model, toSave, NHibernateBase.Session);

            dao.SaveOrUpdate(toSave, toSave.Id);

            return RedirectToAction("Index");
        }

        [Acesso(AcaoId = "salas.delete")]
        [SelecionouFilial(MensagemErro = "Para excluir Salas, selecione primeiro uma Escola Padrão.")]
        public ActionResult Delete(int Id)
        {
            SalaDAO dao = new SalaDAO();
            Sala model = dao.GetById(Id);

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
            SalaDAO dao = new SalaDAO();
            if (ModelState.IsValid)
            {
                Sala o = dao.GetById(id);
                string descricao = o.Descricao;
                dao.Delete(o);
                this.FlashMessage(string.Format("Sala \"{0}\" excluída com sucesso", descricao));
                return RedirectToAction("Index");
            }
            Sala model = dao.GetById(id);
            return View(model);
        }
    }
}
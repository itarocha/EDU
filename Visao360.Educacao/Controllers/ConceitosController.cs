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

// Horari_o => Conceit_o
// Period_o => Nive_l 

namespace Visao360.Educacao.Controllers
{
    public class ConceitosController : BaseController
    {
        [Acesso(AcaoId = "conceitos.index")]
        public ActionResult Index(string searchString)
        {
            IEnumerable<Conceito> lista = new ConceitoDAO().GetListagem();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        [Acesso(AcaoId = "conceitos.edit")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);
            Conceito model = novo ? new Conceito() : new ConceitoDAO().GetById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            EnviarViewBagEdit();
            ViewBag.Acao = novo ? "Novo Horário" : "Editar Horário";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Persistencia]
        public ActionResult EditConfirmed(Conceito model)
        {
            Boolean novo = (model.Id == 0);
            if (!novo){}

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Conceito" : "Editar Conceito";
                EnviarViewBagEdit();
                return View(model);
            }
            ConceitoDAO dao = new ConceitoDAO();
            
            dao.SaveOrUpdate(model, model.Id);
            return RedirectToAction("Index");
        }

        private void EnviarViewBagEdit()
        {
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
        }

        // Horários
        [Acesso(AcaoId = "conceitos.niveis")]
        // id = ConceitoId
        public ActionResult Niveis(int id)
        {
            Conceito conceito = new ConceitoDAO().GetById(id);
            ViewBag.Conceito = conceito;
            if (conceito == null)
            {
                return HttpNotFound();
            }

            IEnumerable<ConceitoNivelVO> lista = new ConceitoNivelDAO().GetByConceitoId(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        [Acesso(AcaoId = "conceitosniveis.edit")]
        public ActionResult NivelEdit(int ConceitoId, int NivelId = 0)
        {
            Boolean novo = (NivelId == 0);
            ConceitoNivelVO model = null;
            ConceitoNivelDAO dao = new ConceitoNivelDAO();

            if (novo)
            {
                model = new ConceitoNivelVO();
                model.ConceitoId = ConceitoId;
            }
            else
            {
                model = dao.GetVOById(NivelId);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }

            Conceito conceito = new ConceitoDAO().GetById(ConceitoId);
            ViewBag.Conceito = conceito;

            EnviarViewBagConceitos();

            ViewBag.Acao = novo ? "Novo Período" : "Editar Período";
            return View(model);
        }

        private void EnviarViewBagConceitos()
        {
            ViewBag.EscolaId = this.EscolaSessao.EscolaId;
            //ViewBag.ListaNivelAula = ComboBuilder.ListaNivelAula();
        }

        [HttpPost, ActionName("NivelEdit")]
        [Persistencia]
        public ActionResult NivelEditConfirmed(ConceitoNivelVO model)
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
                ViewBag.Acao = novo ? "Novo Nível" : "Editar Nível";

                Conceito Conceito = new ConceitoDAO().GetById(model.ConceitoId);
                @ViewBag.Conceito = Conceito;

                EnviarViewBagConceitos();
                return View(model);
            }

            ConceitoDAO tdao = new ConceitoDAO();
            //NivelAulaDAO padao = new NivelAulaDAO();

            ConceitoNivelDAO dao = new ConceitoNivelDAO();
            ConceitoNivel toSave = novo ? new ConceitoNivel() : dao.GetById(model.Id);
            //EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            //model.EscolaId = e.EscolaId;

            Conversor.Converter(model, toSave, NHibernateBase.Session); //

            dao.SaveOrUpdate(toSave, toSave.Id);

            return Redirect(String.Format("/ConceitoNiveis/{0}", model.ConceitoId));
        }

        [Acesso(AcaoId = "conceitos.delete")]
        public ActionResult Delete(int Id)
        {
            ConceitoDAO dao = new ConceitoDAO();
            Conceito model = dao.GetById(Id);

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
            bool pode = new ConceitoDAO().PodeExcluir(id, out mensagemRetorno);
            if (!pode)
            {
                ModelState.AddModelError("Id", mensagemRetorno);
            }

            ConceitoDAO dao = new ConceitoDAO();
            if (ModelState.IsValid)
            {
                Conceito o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                this.FlashMessage(string.Format("Nível \"{0}\" excluído com sucesso", descricao));
                return RedirectToAction("Index");
            }
            Conceito model = dao.GetById(id);
            return View(model);
        }

        [Acesso(AcaoId = "conceitosniveis.delete")]
        public ActionResult NivelDelete(int ConceitoId, int NivelId = 0)
        {
            Boolean novo = (NivelId == 0);
            ConceitoNivelVO model = null;
            ConceitoNivelDAO dao = new ConceitoNivelDAO();

            if (novo)
            {
                model = new ConceitoNivelVO();
                model.ConceitoId = ConceitoId;
            }
            else
            {
                model = dao.GetVOById(NivelId);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }

            Conceito conceito = new ConceitoDAO().GetById(ConceitoId);
            ViewBag.Conceito = conceito;

            return View(model);
        }

        [HttpPost, ActionName("NivelDelete")]
        [Persistencia]
        public ActionResult NivelDeleteConfirmed(int ConceitoId, int NivelId = 0)
        {
            /*
            string mensagemRetorno;
            bool pode = new ConceitoNivelDAO().PodeExcluir(id, out mensagemRetorno);
            if (!pode)
            {
                ModelState.AddModelError("Id", mensagemRetorno);
            }
            */

            ConceitoNivelDAO dao = new ConceitoNivelDAO();
            if (ModelState.IsValid)
            {
                ConceitoNivel o = dao.GetById(NivelId);
                string descricao = o.Descricao;

                dao.Delete(o);

                this.FlashMessage(string.Format("Conceito \"{0}\" excluído com sucesso", descricao));
                return Redirect(String.Format("/ConceitoNiveis/{0}", ConceitoId));
            }
            ConceitoNivel model = dao.GetById(NivelId);
            return View(model);
        }

    }
}


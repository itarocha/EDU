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
using Dardani.EDU.Entities.VO;
using Petra.Util.Model;
using Dardani.EDU.BO.App;
using Petra.Util.Funcoes;
using Petra.DAO.Util;

namespace Visao360.Educacao.Controllers
{
    public class DisciplinasController : BaseController
    {
        //
        // GET: /Funerarias/
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            //EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            //int escolaId = (e == null) ? 0 : e.EscolaId;

            // Se escola for 0, redirecionar para index de Escolas e enviar mensagem
            //if (escolaId == 0)
           // {
           //     TempData["mensagem"] = "Para gerenciar Calendários é necessário tornar uma Escola Padrão";
           //     return RedirectToAction("Selecionar", "Home"); // RedirectToAction("Index", controllerName: "Escolas");
           // }

            IEnumerable<DisciplinaVO> lista = new DisciplinaDAO().GetListagemVO();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            //ViewBag.EscolaId = e.EscolaId;
            return View(lista);
        }

        [Role(Roles = "Administrador")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);
            DisciplinaVO model = null;
            DisciplinaDAO dao = new DisciplinaDAO();

            if (novo)
            {
                model = new DisciplinaVO();
                model.FlagAtivo = "S";
            }
            else
            {
                model = dao.GetVOById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }

            EnviarViewBagEdit();

            ViewBag.Acao = novo ? "Nova Disciplina" : "Editar Disciplina";
            return View(model);
        }

        private void EnviarViewBagEdit() {
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
            ViewBag.ListaDisciplinaEducacenso = ComboBuilder.ListaDisciplinaEducacenso();
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(DisciplinaVO model)
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
                ViewBag.Acao = novo ? "Nova Disciplina" : "Editar Disciplina";
                EnviarViewBagEdit();

                return View(model);
            }

            DisciplinaDAO dao = new DisciplinaDAO();
            Disciplina toSave = novo ? new Disciplina() : dao.GetById(model.Id);

            /*
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            model.EscolaId = e.EscolaId;
            model.AnoLetivoId = e.AnoLetivoId;
            */
            Conversor.Converter(model, toSave, NHibernateBase.Session);
            dao.SaveOrUpdate(toSave, toSave.Id);
            
            return RedirectToAction("Index");
        }
    }
}


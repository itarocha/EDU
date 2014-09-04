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

namespace Visao360.Educacao.Controllers
{
    public class CalendariosController : BaseController
    {
        //
        // GET: /Funerarias/
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            int escolaId = (e == null) ? 0 : e.EscolaId;

            // Se escola for 0, redirecionar para index de Escolas e enviar mensagem
            if (escolaId == 0)
            {
                this.FlashMessage("Para gerenciar Calendários é necessário tornar uma Escola Padrão");
                return RedirectToAction("Selecionar","Home"); // RedirectToAction("Index", controllerName: "Escolas");
            }

            IEnumerable<Calendario> lista = new CalendarioDAO().GetListagemByEscolaAno(escolaId, e.AnoLetivoAno);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            ViewBag.EscolaId = e.EscolaId;
            return View(lista);
        }

        [Role(Roles = "Administrador")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            CalendarioVO model = null;
            CalendarioDAO dao = new CalendarioDAO();

            if (novo)
            {
                model = new CalendarioVO();
                model.AnoLetivoId = e.AnoLetivoId;
                model.EscolaId = e.EscolaId;
            }
            else
            {
                model = dao.GetCalendarioVO(id, e.EscolaId, e.AnoLetivoId);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }
           // AnoLetivoDAO tedao = new AnoLetivoDAO();
           // IEnumerable<ItemVO> listaAnoLetivo = tedao.BuidListaItemVO();
            //Session["ListaAnoLetivo"] = listaAnoLetivo;

            ViewBag.Acao = novo ? "Novo Calendário" : "Editar Calendário";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(CalendarioVO model)
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
                ViewBag.Acao = novo ? "Novo Calendário" : "Editar Calendário";

                return View(model);
            }

            CalendarioDAO dao = new CalendarioDAO();
            Calendario toSave = novo ? new Calendario() : dao.GetById(model.Id);

            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            model.EscolaId = e.EscolaId;
            model.AnoLetivoId = e.AnoLetivoId;

            Conversor.Converter(model, toSave, NHibernateBase.Session);
            dao.SaveOrUpdate(toSave, toSave.Id);

            return RedirectToAction("Index");
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            CalendarioDAO dao = new CalendarioDAO();
            Calendario model = dao.GetById(Id);

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
            bool existe = new LoteDAO().ExisteLotePorCalendarioId(id);

            if (existe)
            {
                ModelState.AddModelError("Id", "Existem Lotes para esse Tipo de Lote. Exclusão não permitida.");
            }
            */
            CalendarioDAO dao = new CalendarioDAO();
            if (ModelState.IsValid)
            {
                Calendario o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);
                this.FlashMessage(string.Format("Calendário {0} excluído com sucesso", descricao));
                
                return RedirectToAction("Index");
            }
            Calendario model = dao.GetById(id);
            return View(model);
        }

        public ActionResult Eventos(int calendarioId, int mesId) {

            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            int escolaId = (e == null) ? 0 : e.EscolaId;

            // Se escola for 0, redirecionar para index de Escolas e enviar mensagem
            if (escolaId == 0)
            {
                this.FlashMessage("Para gerenciar Calendários é necessário tornar uma Escola Padrão");
                return RedirectToAction("Selecionar", "Home"); // RedirectToAction("Index", controllerName: "Escolas");
            }

            ViewBag.EscolaId = e.EscolaId;

            Calendario cal = new CalendarioDAO().GetById(calendarioId);
            ViewBag.Calendario = cal;

            //IEnumerable<Calendario> lista = new CalendarioDAO().GetListagemByEscolaAno(escolaId, e.AnoLetivoAno);
            
            // Na verdade, se calendário estiver fora do escopo, dá erro
            mesId = ((mesId <= 0) && (mesId >= 13)) ? 8 : mesId;

            TheCalendario calendario = new TheCalendario(calendarioId, e.AnoLetivoAno, mesId);

            return View(calendario);
        }

        [HttpPost]
        public JsonResult EventosPost(EnvioCalendario data)
        {

            int id = /*data.NumeroDia + */data.Mes;

            List<Estilo> fooList = new List<Estilo>();

            fooList.Add(new Estilo { Dia = 3, Cor = "#f0f" });
            fooList.Add(new Estilo { Dia = 8, Cor = "#f00" });
            fooList.Add(new Estilo { Dia = 10, Cor = "#ff0" });
            fooList.Add(new Estilo { Dia = 17, Cor = "#f0f" });
            fooList.Add(new Estilo { Dia = 20, Cor = "#f0f" });
            fooList.Add(new Estilo { Dia = 22, Cor = "#0ff" });
            fooList.Add(new Estilo { Dia = 23, Cor = "#f00" });
            fooList.Add(new Estilo { Dia = 28, Cor = "#ff0" });

            //return Json(fooList.Where(foo => foo.FooId == FooId).ToList(),JsonRequestBehavior.AllowGet);

            return Json(fooList, JsonRequestBehavior.AllowGet);
        }		

    }
}


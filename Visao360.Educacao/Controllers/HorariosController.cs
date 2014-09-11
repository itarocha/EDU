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
    public class HorariosController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            if (!ExisteEscolaSelecionada("Para acessar Horários da Escola, selecione primeiro uma Escola Padrão"))
            {
                // Poderia ter uma espécie de AfterSelect para redirecionar para esta Url... vamos ver!
                return RedirectToAction("Selecionar", "Home");
            };

            ViewBag.EscolaId = this.EscolaSessao.EscolaId;
            IEnumerable<Horario> lista = new HorarioDAO().GetListagemByEscolaId(EscolaSessao.EscolaId);
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
            HorarioVO model = novo ? new HorarioVO() : new HorarioDAO().GetVOById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            if (novo){
                EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            }

            ViewBag.Acao = novo ? "Novo Horário" : "Editar Horário";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(HorarioVO model)
        {
            Boolean novo = (model.Id == 0);
            if (!novo){}

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Horário" : "Editar Horário";

                return View(model);
            }
            HorarioDAO dao = new HorarioDAO();
            Horario toSave = novo ? new Horario() : dao.GetById(model.Id);
            Conversor.Converter(model, toSave, NHibernateBase.Session); //
            dao.SaveOrUpdate(toSave, toSave.Id);
            return RedirectToAction("Index");
        }

        // Horários
        [Role(Roles = "Administrador")]
        // id = HorarioId
        public ActionResult Periodos(int id)
        {
            if (!ExisteEscolaSelecionada("Para acessar Períodos, selecione primeiro uma Escola Padrão"))
            {
                // Poderia ter uma espécie de AfterSelect para redirecionar para esta Url... vamos ver!
                return RedirectToAction("Selecionar", "Home");
            };

            Horario Horario = new HorarioDAO().GetById(id);
            @ViewBag.Horario = Horario;
            if (Horario == null)
            {
                return HttpNotFound();
            }

            IEnumerable<HorarioPeriodoVO> lista = new HorarioPeriodoDAO().GetByHorarioId(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        [Role(Roles = "Administrador")]
        public ActionResult PeriodoEdit(int horarioId, int periodoId = 0)
        {
            Boolean novo = (periodoId == 0);
            HorarioPeriodoVO model = null;
            HorarioPeriodoDAO dao = new HorarioPeriodoDAO();

            if (novo)
            {
                model = new HorarioPeriodoVO();
                model.HorarioId = horarioId;
            }
            else
            {
                model = dao.GetVOById(periodoId);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }

            Horario Horario = new HorarioDAO().GetById(horarioId);
            @ViewBag.Horario = Horario;

            EnviarViewBagHorarios();

            ViewBag.Acao = novo ? "Novo Período" : "Editar Período";
            return View(model);
        }

        private void EnviarViewBagHorarios()
        {
            ViewBag.EscolaId = this.EscolaSessao.EscolaId;
            ViewBag.ListaPeriodoAula = ComboBuilder.ListaPeriodoAula();
        }

        [HttpPost, ActionName("PeriodoEdit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult PeriodoEditConfirmed(HorarioPeriodoVO model)
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
                ViewBag.Acao = novo ? "Novo Período" : "Editar Período";

                Horario Horario = new HorarioDAO().GetById(model.HorarioId);
                @ViewBag.Horario = Horario;

                EnviarViewBagHorarios();
                return View(model);
            }

            HorarioDAO tdao = new HorarioDAO();
            PeriodoAulaDAO padao = new PeriodoAulaDAO();

            HorarioPeriodoDAO dao = new HorarioPeriodoDAO();
            HorarioPeriodo toSave = novo ? new HorarioPeriodo() : dao.GetById(model.Id);
            //EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            //model.EscolaId = e.EscolaId;

            Conversor.Converter(model, toSave, NHibernateBase.Session); //

            dao.SaveOrUpdate(toSave, toSave.Id);

            return Redirect(String.Format("/PeriodosHorario/{0}", model.HorarioId));
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            HorarioDAO dao = new HorarioDAO();
            Horario model = dao.GetById(Id);

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
            bool pode = new HorarioDAO().PodeExcluir(id, out mensagemRetorno);
            if (!pode)
            {
                ModelState.AddModelError("Id", mensagemRetorno);
            }

            HorarioDAO dao = new HorarioDAO();
            if (ModelState.IsValid)
            {
                Horario o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                this.FlashMessage(string.Format("Horário \"{0}\" excluído com sucesso", descricao));
                return RedirectToAction("Index");
            }
            Horario model = dao.GetById(id);
            return View(model);
        }

        [Role(Roles = "Administrador")]
        public ActionResult PeriodoDelete(int horarioId, int periodoId = 0)
        {
            Boolean novo = (periodoId == 0);
            HorarioPeriodoVO model = null;
            HorarioPeriodoDAO dao = new HorarioPeriodoDAO();

            if (novo)
            {
                model = new HorarioPeriodoVO();
                model.HorarioId = horarioId;
            }
            else
            {
                model = dao.GetVOById(periodoId);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }

            Horario Horario = new HorarioDAO().GetById(horarioId);
            @ViewBag.Horario = Horario;

            return View(model);
        }

        [HttpPost, ActionName("PeriodoDelete")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult PeriodoDeleteConfirmed(int horarioId, int periodoId = 0)
        {
            string mensagemRetorno;
            /*
            bool pode = new HorarioPeriodoDAO().PodeExcluir(id, out mensagemRetorno);
            if (!pode)
            {
                ModelState.AddModelError("Id", mensagemRetorno);
            }
            */
            HorarioPeriodoDAO dao = new HorarioPeriodoDAO();
            if (ModelState.IsValid)
            {
                HorarioPeriodo o = dao.GetById(periodoId);
                string descricao = o.PeriodoAula.HoraInicio + " - " + o.PeriodoAula.HoraTermino;

                dao.Delete(o);

                this.FlashMessage(string.Format("Período \"{0}\" excluído com sucesso", descricao));
                return Redirect(String.Format("/PeriodosHorario/{0}", horarioId));
            }
            HorarioPeriodo model = dao.GetById(periodoId);
            return View(model);
        }

    }
}


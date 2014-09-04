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
    public class TurnosEscolaController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            if (!ExisteEscolaSelecionada("Para acessar Turnos da Escola, selecione primeiro uma Escola Padrão"))
            {
                return RedirectToAction("Selecionar", "Home");
            };

            ViewBag.EscolaId = this.EscolaSessao.EscolaId;
            IEnumerable<EscolaTurnoVO> lista = new EscolaTurnoDAO().GetListaVOByEscolaId(this.EscolaSessao.EscolaId);
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
            EscolaTurnoVO model = novo ? new EscolaTurnoVO() : new EscolaTurnoDAO().GetEscolaTurnoVOById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            if (novo)
            {
                EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
                model.EscolaId = e.EscolaId;
            }
            EnviarViewBagTurnos();
            ViewBag.Acao = novo ? "Adicionar Turno" : "Editar Turno da Escola";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(EscolaTurnoVO model)
        {
            Boolean novo = (model.Id == 0);
            if (!novo) { }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Turno" : "Editar Turno";

                return View(model);
            }
            EscolaTurnoDAO dao = new EscolaTurnoDAO();
            EscolaTurno toSave = novo ? new EscolaTurno() : dao.GetById(model.Id);
            Conversor.Converter(model, toSave, NHibernateBase.Session); //
            dao.SaveOrUpdate(toSave, toSave.Id);
            return RedirectToAction("Index");
        }

        private void EnviarViewBagTurnos()
        {
            //EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            //int escolaId = (e == null) ? 0 : e.EscolaId;
            ViewBag.ListaTurno = ComboBuilder.ListaTurno();
        }


        [Role(Roles = "Administrador")]
        public ActionResult Horarios(int id)
        {
            if (!ExisteEscolaSelecionada("Para acessar Turnos da Escola, selecione primeiro uma Escola Padrão"))
            {
                return RedirectToAction("Selecionar", "Home");
            };

            Turno turno = new TurnoDAO().GetById(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            ViewBag.Turno = turno;
            

            IEnumerable<HorarioPeriodoVO> lista = new HorarioPeriodoDAO().GetByHorarioId(id);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        [Role(Roles = "Administrador")]
        public ActionResult HorarioEdit(int turnoId, int horarioId = 0)
        {
            Boolean novo = (horarioId == 0);
            HorarioPeriodoVO model = null;
            HorarioPeriodoDAO dao = new HorarioPeriodoDAO();

            if (novo)
            {
                model = new HorarioPeriodoVO();
                model.HorarioId = turnoId;
                //model.EscolaId = escolaId;
            }
            else
            {
                model = dao.GetVOById(horarioId);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }

            Turno turno = new TurnoDAO().GetById(turnoId);
            ViewBag.Turno = turno;

            EnviarViewBagHorarios();

            ViewBag.Acao = novo ? "Novo Horário" : "Editar Horário";
            return View(model);
        }

        private void EnviarViewBagHorarios()
        {
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            int escolaId = (e == null) ? 0 : e.EscolaId;
            ViewBag.ListaPeriodoAula = ComboBuilder.ListaPeriodoAula();
        }


        [HttpPost, ActionName("HorarioEdit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult HorarioEditConfirmed(HorarioPeriodoVO model)
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
                ViewBag.Acao = novo ? "Novo Horário" : "Editar Horário";

                Turno turno = new TurnoDAO().GetById(model.HorarioId);
                @ViewBag.Turno = turno;

                EnviarViewBagHorarios();
                return View(model);
            }

            TurnoDAO tdao = new TurnoDAO();
            PeriodoAulaDAO padao = new PeriodoAulaDAO();

            HorarioPeriodoDAO dao = new HorarioPeriodoDAO();
            HorarioPeriodo toSave = novo ? new HorarioPeriodo() : dao.GetById(model.Id);
            //EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            //model.EscolaId = e.EscolaId;

            Conversor.Converter(model, toSave, NHibernateBase.Session); //

            //toSave.Turno = tdao.GetById(model.TurnoId);
            //toSave.PeriodoAula = padao.GetById(model.PeriodoAulaId);
            //toSave.Escola = e;

            dao.SaveOrUpdate(toSave, toSave.Id);

            return Redirect(String.Format("/HorarioPeriodos/{0}", model.HorarioId));
        }

        [Role(Roles = "Administrador")]
        public ActionResult Delete(int Id)
        {
            TurnoDAO dao = new TurnoDAO();
            Turno model = dao.GetById(Id);

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
            TurnoDAO dao = new TurnoDAO();
            if (ModelState.IsValid)
            {
                Turno o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                this.FlashMessage(string.Format("Turno \"{0}\" excluído com sucesso", descricao));
                return RedirectToAction("Index");
            }
            Turno model = dao.GetById(id);
            return View(model);
        }
    }
}


﻿using System;
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
using Dardani.EDU.BO.App;

namespace Visao360.Educacao.Controllers
{
    public class CalendariosController : BaseController
    {
        [SelecionouFilial(MensagemErro = "Para gerenciar Calendários, selecione primeiro uma Escola Padrão.")]
        [Acesso(AcaoId = "calendarios.index")]
        public ActionResult Index(string searchString)
        {
            IEnumerable<Calendario> lista = new CalendarioDAO().GetListagemByEscolaAno(this.EscolaSessao.EscolaId, this.EscolaSessao.AnoLetivoAno);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            ViewBag.EscolaId = this.EscolaSessao.EscolaId;
            return View(lista);
        }

        [Acesso(AcaoId = "calendarios.edit")]
        [SelecionouFilial(MensagemErro = "Para editar Calendários, selecione primeiro uma Escola Padrão.")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);
            CalendarioVO model = null;
            CalendarioDAO dao = new CalendarioDAO();

            if (novo)
            {
                model = new CalendarioVO();
                model.AnoLetivoId = this.EscolaSessao.AnoLetivoId;
                model.EscolaId = this.EscolaSessao.EscolaId;
            }
            else
            {
                model = dao.GetCalendarioVO(id, this.EscolaSessao.EscolaId, this.EscolaSessao.AnoLetivoId);
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

            model.EscolaId = this.EscolaSessao.EscolaId;
            model.AnoLetivoId = this.EscolaSessao.AnoLetivoId;

            Conversor.Converter(model, toSave, NHibernateBase.Session);
            dao.SaveOrUpdate(toSave, toSave.Id);

            return RedirectToAction("Index");
        }

        [Acesso(AcaoId = "calendarios.delete")]
        [SelecionouFilial(MensagemErro = "Para excluir Calendários, selecione primeiro uma Escola Padrão.")]
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

        [SelecionouFilial(MensagemErro = "Para editar Eventos do Calendário, selecione primeiro uma Escola Padrão.")]
        [Acesso(AcaoId = "calendarios.eventos")]
        public ActionResult Eventos(int calendarioId, int mesId)
        {
            ViewBag.EscolaId = this.EscolaSessao.EscolaId;

            Calendario cal = new CalendarioDAO().GetById(calendarioId);
            ViewBag.Calendario = cal;

            // Na verdade, se calendário estiver fora do escopo, dá erro
            mesId = ((mesId <= 0) && (mesId >= 13)) ? 8 : mesId;

            TheCalendario calendario = new TheCalendario(calendarioId, this.EscolaSessao.AnoLetivoAno, mesId);

            return View(calendario);
        }

        public JsonResult GetTiposDias()
        {
            IEnumerable<ItemVO> lista = ItemVOBuilders.Instance.BuildListaTipoDia();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Persistencia]
        public JsonResult EventosPost(EnvioCalendario data)
        {
            // Veja  data.TiposEventos int[]
            CalendarioDiaDAO cdao = new CalendarioDiaDAO();
            DateTime dt = new DateTime(data.AnoId, data.MesId, data.DiaId); // ISSO VAI SER ARRAY

            CalendarioDia toDelete = cdao.GetByCalendarioAndDia(data.CalendarioId, dt);
            if (toDelete != null) {
                cdao.Delete(toDelete);
            }

            // Excluir tipo de dia da data

            // Incluir tipo de dia na data

            if (data.TipoDiaId > 0)
            {
                CalendarioDia toSave = new CalendarioDia();
                CalendarioDiaVO model = new CalendarioDiaVO() { CalendarioId = data.CalendarioId, DataEvento = dt, TipoDiaId = data.TipoDiaId };
                Conversor.Converter(model, toSave, NHibernateBase.Session);
                cdao.SaveOrUpdate(toSave, toSave.Id);
            }

            // Tipos de Eventos
            CalendarioDiaEventoDAO cedao = new CalendarioDiaEventoDAO();

            // Limpar Todos pela Data
            IEnumerable<CalendarioDiaEvento> listaToDelete = cedao.GetListagemByCalendarioAndData(data.CalendarioId, dt);
            if (listaToDelete != null)
            {
                foreach(CalendarioDiaEvento xis in listaToDelete){
                    cedao.Delete(xis);
                }
            }

            if (data.TiposEventos != null)
            {
                foreach (int tipoEventoId in data.TiposEventos)
                {
                    CalendarioDiaEvento cde = new CalendarioDiaEvento();

                    CalendarioDiaEventoVO cdevo = new CalendarioDiaEventoVO() { CalendarioId = data.CalendarioId, DataEvento = dt, TipoEventoId = tipoEventoId };
                    Conversor.Converter(cdevo, cde, NHibernateBase.Session);
                    cedao.SaveOrUpdate(cde, cde.Id);
                }
            }

            int id = /*data.NumeroDia + */data.MesId;

            return GetEventosMes(data.CalendarioId, data.AnoId, data.MesId);
        }

        public JsonResult GetEventosMes(int CalendarioId, int AnoId, int MesId) {
            CalendarioMes cm = new CalendarioMes();

            // Isso vai virar função. Vai virar também retorno json!!!
            DateTime dtIni = new DateTime(AnoId, MesId, 1);
            DateTime dtFim = dtIni.AddMonths(1).AddDays(-1);

            // Tipos de Dias do Calendário
            List<EstiloTipoDia> listaEstiloTipoDia = new List<EstiloTipoDia>();
            CalendarioDiaDAO cdao = new CalendarioDiaDAO();
            IEnumerable<CalendarioDiaVO> lista = cdao.GetByListagemByCalendarioAndPeriodo(CalendarioId, dtIni, dtFim);
            foreach (CalendarioDiaVO cd in lista)
            {
                listaEstiloTipoDia.Add(new EstiloTipoDia { Dia = cd.DataEvento.Day, Cor = cd.TipoDiaCor, TipoDiaId = cd.TipoDiaId });
            }
            cm.ListaEstiloTipoDia = listaEstiloTipoDia;

            // Tipos de Eventos do Calendário
            List<EstiloTipoEvento> listaEstiloTipoEvento = new List<EstiloTipoEvento>();
            CalendarioDiaEventoDAO cedao = new CalendarioDiaEventoDAO();
            IEnumerable<CalendarioDiaEventoVO> lista2 = cedao.GetListagemVOByCalendarioAndPeriodo(CalendarioId, dtIni, dtFim);
            foreach (CalendarioDiaEventoVO cde in lista2)
            {
                listaEstiloTipoEvento.Add(new EstiloTipoEvento { Dia = cde.DataEvento.Day, TipoEventoId = cde.TipoEventoId, Simbolo = cde.TipoEventoSimbolo });
            }
            cm.ListaEstiloTipoEvento = listaEstiloTipoEvento;

            return Json(cm, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListaTipoEvento()
        {
            TipoEventoDAO tedao = new TipoEventoDAO();
            IEnumerable<TipoEvento> lista = tedao.GetListagem();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

    }
}


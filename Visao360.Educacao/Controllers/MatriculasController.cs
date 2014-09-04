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
using Dardani.EDU.BO.App;

namespace Visao360.Educacao.Controllers
{
    public class MatriculasController : BaseController
    {
        [Role(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            MatriculaVO model = null;
            MatriculaDAO dao = new MatriculaDAO();

            model = dao.GetVOById(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            TurmaDAO tdao = new TurmaDAO();
            ViewBag.TurmaVO = tdao.GetVOById(model.TurmaId);

            EnviarViewBagEdit();

            ViewBag.Acao = "Editar Matricula";
            return View(model);
        }

        private void EnviarViewBagEdit() {
            ViewBag.ListaTipoSala = ComboBuilder.ListaTipoSala();
            ViewBag.ListaEscolarizacaoEspecial = ComboBuilder.ListaEscolarizacaoEspecial();
            ViewBag.ListaTransportePublico = ComboBuilder.ListaTransportePublico();
            ViewBag.ListaTurmaUnificada = ComboBuilder.ListaTurmaUnificada();
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
        }

        [HttpPost, ActionName("Edit")]
        [Role(Roles = "Administrador")]
        [Persistencia]
        public ActionResult EditConfirmed(MatriculaVO model)
        {
            Boolean novo = (model.Id == 0);

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Nova Matrícula" : "Editar Matrícula";
                EnviarViewBagEdit();
                return View(model);
            }

            MatriculaDAO dao = new MatriculaDAO();
            Matricula toSave = dao.GetById(model.Id);
            
            Conversor.Converter(model, toSave, NHibernateBase.Session);

            dao.SaveOrUpdate(toSave, toSave.Id);

            return Redirect("/TurmaAlunos/"+model.TurmaId);
        }

        [Role(Roles = "Administrador")]
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
        [Role(Roles = "Administrador")]
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

                FlashMessage(string.Format("Sala \"{0}\" excluída com sucesso", descricao));
                return RedirectToAction("Index");
            }
            Sala model = dao.GetById(id);
            return View(model);
        }
    }
}


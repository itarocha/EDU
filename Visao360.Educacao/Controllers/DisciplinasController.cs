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
            DisciplinaDAO dao = new DisciplinaDAO();
            if (!novo)
            {
                /*
                if (dao.PossuiMatrizDisciplina(model.Id))
                {
                    ModelState.AddModelError("Id", "Disciplina não pode ser alterada porque já está sendo utilizada.");
                }
                 */ 
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Nova Disciplina" : "Editar Disciplina";
                EnviarViewBagEdit();

                return View(model);
            }

            Disciplina toSave = novo ? new Disciplina() : dao.GetById(model.Id);

            Conversor.Converter(model, toSave, NHibernateBase.Session);
            dao.SaveOrUpdate(toSave, toSave.Id);
            
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            DisciplinaDAO dao = new DisciplinaDAO();
            Disciplina model = dao.GetById(Id);

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
            bool pode = new DisciplinaDAO().PodeExcluir(id, out mensagemRetorno);
            if (!pode)
            {
                ModelState.AddModelError("Id", mensagemRetorno);
            }

            DisciplinaDAO dao = new DisciplinaDAO();
            if (ModelState.IsValid)
            {
                Disciplina o = dao.GetById(id);
                string descricao = o.Descricao;

                dao.Delete(o);

                this.FlashMessage(string.Format("Tipo de Evento \"{0}\" excluído com sucesso", descricao));
                return RedirectToAction("Index");
            }
            Disciplina model = dao.GetById(id);
            return View(model);
        }

    }
}


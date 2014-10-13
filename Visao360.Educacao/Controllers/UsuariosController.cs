using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Visao360.Educacao.Helpers;
using Visao360.Educacao.Filters;
using Petra.Util.Funcoes;
using System.Data.Common;
using Petra.Util.Model;
using Dardani.GER.BO.NH;
using Dardani.EDU.Entities.VO;
using Dardani.EDU.BO.NH;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.BO.App;
using Petra.DAO.Util;

namespace Visao360.Educacao.Controllers
{
    public class UsuariosController : BaseController
    {

        [Acesso(AcaoId = "usuarios.getusuarios")]
        public ActionResult GetUsuariosJSON(string searchString)
        {
            IEnumerable<Usuario> lista = new List<Usuario>();
            lista = new UsuarioDAO().GetListagem(); // ConstrucaoServices.Instance.GetListaUsuarios(searchString);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [Acesso(AcaoId = "usuarios.index")]
        public ActionResult Index()
        {
            IEnumerable<Usuario> lista = new UsuarioDAO().GetListagem();
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Listagem", lista);
            }
            return View(lista);
        }

        [Acesso(AcaoId = "usuarios.edit")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);
            UsuarioVO model = null;
            UsuarioDAO dao = new UsuarioDAO();

            if (novo)
            {
                model = new UsuarioVO();
            }
            else
            {
                model = dao.GetVOById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }
            }
            ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
            ViewBag.Acao = novo ? "Novo Usuário" : "Editar Usuário";
            ViewBag.Novo = novo;
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Persistencia]
        public ActionResult EditConfirmed(UsuarioVO model)
        {
            Boolean novo = (model.Id == 0);

            if (novo)
            {
                //int maximoSepultados = new EnsinoDAO().GetMaximoSepultadosPorEnsinoId(model.Id);
                if (model.SenhaAcesso != model.SenhaConfirmacao)
                {
                    ModelState.AddModelError("SenhaAcesso", String.Format("Senha diferente da confirmação"));
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Usuário" : "Editar Usuário";
                ViewBag.ListaSimNao = ComboBuilder.ListaSimNao();
                return View(model);
            }

            //TipoSalaDAO tdao = new TipoSalaDAO();
            UsuarioDAO dao = new UsuarioDAO();
            Usuario toSave = novo ? new Usuario() : dao.GetById(model.Id);

            model.NumeroCPF = model.NumeroCPF.Replace(".", "");
            model.NumeroCPF = model.NumeroCPF.Replace("-", "");
            Conversor.Converter(model, toSave, NHibernateBase.Session);

            if (novo) {
                string senhaMd5 = Criptografia.MD5(model.SenhaAcesso);
                toSave.SenhaAcesso = senhaMd5;
            }

            dao.SaveOrUpdate(toSave, toSave.Id);

            return RedirectToAction("Index");
        }

        [Acesso(AcaoId = "usuarios.delete")]
        public ActionResult Delete(int Id)
        {
            UsuarioDAO dao = new UsuarioDAO();
            Usuario model = dao.GetById(Id);

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
            UsuarioDAO dao = new UsuarioDAO();
            if (ModelState.IsValid)
            {
                Usuario o = dao.GetById(id);
                string nome = o.Nome;
                dao.Delete(o);
                this.FlashMessage(string.Format("Usuário \"{0}\" excluído com sucesso", nome));
                return RedirectToAction("Index");
            }
            Usuario model = dao.GetById(id);
            return View(model);
        }


        [Acesso(AcaoId = "usuarios.acoes")]
        public ActionResult Acoes(int id = 0)
        {
            Boolean novo = (id == 0);

            UsuarioDAO edao = new UsuarioDAO();
            Usuario p = edao.GetById(id);
            ViewBag.UsuarioNome = (p == null) ? "" : p.Nome;
            if (p == null)
            {
                return HttpNotFound();
            }
            UsuarioAcaoVO model = edao.GetUsuarioAcao(id);

            // Popula a seção com as disciplinas
            IEnumerable<Acao> acessos = new AcaoDAO().GetListagem();
            Session["ListaItens"] = acessos;

            return View(model);
        }

        [HttpPost, ActionName("Acoes")]
        [Persistencia]
        public ActionResult AcoesConfirmed(UsuarioAcaoVO model)
        {
            UsuarioDAO adao = new UsuarioDAO();
            adao.GravarAcoes(model);
            return RedirectToAction("Index");
        }

    }
}

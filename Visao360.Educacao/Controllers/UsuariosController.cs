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

namespace Visao360.Educacao.Controllers
{
    public class UsuariosController : BaseController
    {

        [Acesso(AcaoId = "usuarios.getusuarios")]
        public ActionResult GetUsuariosJSON(string searchString)
        {
            IEnumerable<Usuario> lista = new List<Usuario>();
            lista = new UsuarioDAO().GetListaUsuarios(searchString); // ConstrucaoServices.Instance.GetListaUsuarios(searchString);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [Acesso(AcaoId = "usuarios.index")]
        public ActionResult Index(string searchString)
        {
            IEnumerable<Usuario> lista = new List<Usuario>();
            lista = new UsuarioDAO().GetListaUsuarios(searchString);// ConstrucaoServices.Instance.GetListaUsuarios(searchString);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Usuarios", lista);
            }

            return View(lista);
        }

        [Acesso(AcaoId = "usuarios.edit")]
        public ActionResult Edit(int id = 0)
        {
            Boolean novo = (id == 0);
            UsuarioVO model = novo ? new UsuarioVO() : new UsuarioDAO().GetUsuarioVOById(id); // ConstrucaoServices.Instance.GetUsuarioVOById(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            model.SenhaAcesso = Criptografia.MD5("temp"); // Para evitar mensagem de campo requerido

            IEnumerable<ItemVO> listaSexos = ItemVOBuilders.Instance.BuildListaSexo();
            IEnumerable<ItemStringVO> listaNiveis = EDUListasBuilder.BuildListaNiveis();
            IEnumerable<ItemStringVO> listaSimNao = EDUListasBuilder.BuildListaSimNao();

            Session["ListaSexos"] = listaSexos;
            Session["ListaNiveis"] = listaNiveis;
            Session["ListaSimNao"] = listaSimNao;

            ViewBag.Acao = novo ? "Novo Usuário" : "Editar Usuário";
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [Persistencia]
        [HandleError(ExceptionType = typeof(DbException), View = "DatabaseError")]
        [HandleError(View = "DatabaseError")]
        public ActionResult EditConfirmed(UsuarioVO model)
        {
            Boolean novo = (model.Id == 0);

            if (!ModelState.IsValid)
            {
                ViewBag.Acao = novo ? "Novo Usuário" : "Editar Usuário";
                return View(model);
            }
            UsuarioDAO fdao = new UsuarioDAO();

            Usuario fToSave = novo ? new Usuario() : fdao.GetById(model.Id);
            //fToSave.Acesso = fToSave.Acesso == null ? new UsuarioAcesso() : fToSave.Acesso;

            if (novo)
            {
                model.SenhaAcesso = Criptografia.MD5(model.NumeroCPF);
            }
            Conversor.Converter(model, fToSave);

            SexoDAO sdao = new SexoDAO();

            //fToSave.Sexo = sdao.GetById(model.SexoId);
            //fToSave.Acesso.Usuario = fToSave;

            fdao.SaveOrUpdate(fToSave, fToSave.Id);

            return RedirectToAction("Index");
        }

        [Acesso(AcaoId = "usuarios.delete")]
        public ActionResult Delete(int id = 0)
        {
            UsuarioDAO dao = new UsuarioDAO();
            Usuario model = dao.GetById(id);

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioDAO dao = new UsuarioDAO();
            if (ModelState.IsValid)
            {
                Usuario o = dao.GetById(id);
                string nome = o.Nome;

                dao.Delete(o);

                TempData["mensagem"] = string.Format("Usuário {0} excluído com sucesso", nome);
                return RedirectToAction("Index");
            }
            Usuario model = dao.GetById(id);
            return View(model);
        }
    }
}

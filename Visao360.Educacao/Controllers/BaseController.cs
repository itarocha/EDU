using Petra.DAO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visao360.Educacao.Helpers;
using Visao360.Educacao.Models;

namespace Visao360.Educacao.Controllers
{
    public class BaseController : Controller
    {
        private EscolaSessao escolaSessao;

        public BaseController()
            : base()
        {
            escolaSessao = new EscolaSessao();
            atualizarEscolaSessao();




            //db = new CemiterioContext();
            /*
            UsuarioLogado u = GerenciadorUsuarioSessao.UsuarioLogado();

            if (u != null) {
                db.UsuarioId = u.Id;
            }
            */
            //db.UsuarioId = 0;
        }

        public EscolaSessao EscolaSessao
        { 
            get { return this.escolaSessao;  } 
        }

        public void FlashMessage(string mensagem){
            TempData["mensagem"] = mensagem;
        }

        private void atualizarEscolaSessao() {
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            this.escolaSessao.EscolaId = (e == null) ? 0 : e.EscolaId;
            this.escolaSessao.EscolaNome = (e == null) ? "" : e.EscolaNome;
            this.escolaSessao.AnoLetivoId = (e == null) ? 0 : e.AnoLetivoId;
            this.escolaSessao.AnoLetivoAno = (e == null) ? 0 : e.AnoLetivoAno;
        }

        public bool ExisteEscolaSelecionada(string mensagemRedirecionamento = "")
        {
            string msg = (mensagemRedirecionamento == "") ? "Para gerenciar este Módulo é necessário tornar uma Escola Padrão" : mensagemRedirecionamento;
            if (this.EscolaSessao.EscolaId == 0)
            {
                this.FlashMessage(msg);
                return false;
            }
            return true;
        }        

        protected override void OnException(ExceptionContext filterContext)
        {
            /*
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }
            */
 
            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }
 
        /*
            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }
*/
            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = new JsonResult 
                { 
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet, 
                    Data = new
                    { 
                        error = true,
                        message = filterContext.Exception.Message
                    } 
                };
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);
             
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    //MasterName = Master,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }
 
            // log the error using log4net.
            //_logger.Error(filterContext.Exception.Message, filterContext.Exception);
 
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
 
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            NHibernateBase.CloseSession();
        }       

        protected override void HandleUnknownAction(string actionName)
        {
            Response.Redirect("/Home/PaginaNaoExiste");
        }

        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}

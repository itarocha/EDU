using Dardani.EDU.BO.NH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visao360.Educacao.Helpers;
using Visao360.Educacao.Models;

//Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
//Action = filterContext.ActionDescriptor.ActionName + " (Logged By: Custom  Action Filter)",
//IP = filterContext.HttpContext.Request.UserHostAddress,
//DateTime = filterContext.HttpContext.Timestamp
namespace Visao360.Educacao.Filters
{
    public class SelecionouFilialAttribute : ActionFilterAttribute, IActionFilter
    {
        public string MensagemErro {get; set;}

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            EscolaSessao e = GerenciadorEscolaSessao.GetEscolaAtual();
            if (e == null)
            {
                if (!string.IsNullOrEmpty(MensagemErro))
                {
                    filterContext.Controller.TempData["mensagem"] = MensagemErro;
                }
                filterContext.Result = new RedirectResult("/Home/Selecionar");
            }
            this.OnActionExecuting(filterContext);
        }
    }
}
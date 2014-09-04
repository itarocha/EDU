using Petra.DAO.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Visao360.Educacao.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class NHSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            //Debug.WriteLine("NHSessionAttribute.OnActionExecuting...");
            base.OnActionExecuting(actionContext);
            var session = NHibernateBase.Session;
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            //Debug.WriteLine("NHSessionAttribute.OnActionExecuted...");
            base.OnActionExecuted(actionExecutedContext);
            //NHibernateBase.CloseSession();
        }

    }
}
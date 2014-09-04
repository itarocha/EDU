using Petra.DAO.Util;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Visao360.Educacao.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=true)]
    public class PersistenciaAttribute : NHSessionAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            //Debug.WriteLine("PersistenciaAttribute.OnActionExecuting Iniciando ação...");
            base.OnActionExecuting(actionContext);
            NHibernateBase.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            //Debug.WriteLine("PersistenciaAttribute.OnActionExecuted Finalizando ação...");
            
            var tx = NHibernateBase.Session.Transaction;

            try
            {
                if (tx != null && tx.IsActive)
                {
                    NHibernateBase.CommitTransaction();
                    NHibernateBase.Session.Flush();
                }
            }
            catch (Exception e) {
                if (tx != null && tx.IsActive)
                    NHibernateBase.RollbackTransaction();
            }
            
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
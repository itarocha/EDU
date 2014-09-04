using Petra.DAO.Util;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Visao360.Educacao
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            LoadNHibernateCfg(false);
        }

        private void LoadNHibernateCfg(bool recriarBanco)
        {
            var cfg = NHibernateBase.ConfigureNHibernate(
                new System.Reflection.Assembly[] {typeof(Dardani.EDU.Entities.Model.Turma).Assembly});
            if (recriarBanco)
            {
                new SchemaExport(cfg).Execute(true, true, false);
            }
        }

    }
}
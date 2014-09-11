using Dardani.EDU.BO.NH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visao360.Educacao.Helpers;

namespace Visao360.Educacao.Filters
{
    public class RoleAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.Request.IsAuthenticated)
                return false;
            
            // O nome do cara é: httpContext.User.Identity.Name;
            string nome = httpContext.User.Identity.Name;
            string userRole;

            UsuarioDAO dao = new UsuarioDAO();
            userRole = dao.GetNivelByUsuario(nome);

            /*
            using (var db = new CemiterioContext())
            {

                userRole = (from obj in db.Usuarios
                               join x in db.UsuariosAcessos on obj.Id equals x.Id into _Acesso
                               from a in _Acesso.DefaultIfEmpty()
                               where a.NomeUsuario == nome
                               select obj.Nivel).SingleOrDefault();
            }
             */ 
            //userRole = "Administrador,Visitante";

            foreach (string definedRole in this.Roles.Split(',')){
                if (definedRole.Equals(userRole)) 
                    return true;
            }

            return false; // base.AuthorizeCore(httpContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/");
            base.HandleUnauthorizedRequest(filterContext);
        }

        protected override HttpValidationStatus OnCacheAuthorization(HttpContextBase httpContext)
        {
            return base.OnCacheAuthorization(httpContext);
        }
    }
}
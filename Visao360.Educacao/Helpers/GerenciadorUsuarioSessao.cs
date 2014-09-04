using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Visao360.Educacao.Models;

namespace Visao360.Educacao.Helpers
{
    public class GerenciadorUsuarioSessao
    {
        private const string VARIAVEL = "usuario_sessao";

        public static UsuarioLogado UsuarioLogado()
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
                return null;

            //var x = HttpContext.Current.Session[VARIAVEL];

            if (HttpContext.Current.Session[VARIAVEL] == null)
            {
                FormsAuthentication.SignOut();
                UsuarioLogado obj = new UsuarioLogado();
                HttpContext.Current.Session[VARIAVEL] = obj;
            }
            return (UsuarioLogado)HttpContext.Current.Session[VARIAVEL];
        }

        public static void ArmazenarUsuarioLogado(UsuarioLogado usuario)
        {
            HttpContext.Current.Session[VARIAVEL] = usuario;
        }

        public static void Limpar()
        {
            HttpContext.Current.Session[VARIAVEL] = null;
        }
    }
}
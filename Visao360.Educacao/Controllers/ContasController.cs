using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Visao360.Educacao.Models;
using Visao360.Educacao.Helpers;
using System.Data;
using System.Data.Entity;
using Petra.Util.Funcoes;
using Dardani.GER.BO.NH;
using Petra.Util.Model;
using Visao360.Educacao.Filters;
using Dardani.EDU.BO.NH;
using Dardani.EDU.Entities.Model;
using Dardani.EDU.Entities.VO;

namespace Visao360.Educacao.Controllers
{
    public class ContasController : BaseController
    {
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogOnModel model, string returnUrl)
        {
            FormsAuthentication.SignOut();
            Session.Clear();

            if (ModelState.IsValid)
            {
                string pwd = Criptografia.MD5(model.Password);

                UsuarioDAO dao = new UsuarioDAO();

                UsuarioVO u = dao.GetUsuarioByNomeSenha(model.UserName, pwd);

                /*
                var usuario = (from obj in db.Usuarios
                               join x in db.UsuariosAcessos on obj.Id equals x.Id into _Acesso
                               from a in _Acesso.DefaultIfEmpty()
                               where a.NomeUsuario == model.UserName &&
                               a.SenhaAcesso == pwd
                               select new
                               {
                                   Id = obj.Id,
                                   Nome = obj.Nome,
                                   NomeUsuario = a.NomeUsuario,
                                   Nivel = obj.Nivel
                               }).SingleOrDefault();
                */

                if (u != null)
                {
                    FormsAuthentication.SetAuthCookie(u.NomeUsuario, false);

                    GerenciadorUsuarioSessao.ArmazenarUsuarioLogado(new UsuarioLogado
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        NomeUsuario = u.NomeUsuario,
                        Nivel = u.Nivel
                    });

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Selecionar", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "O nome do usuário ou senha estão incorretos.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost, ActionName("AlterarSenha")]
        [Persistencia]
        public ActionResult AlterarSenhaConfirmado(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                string oldpwd = Criptografia.MD5(model.OldPassword);

                string nomeUsuario = this.HttpContext.User.Identity.Name;

                UsuarioDAO udao = new UsuarioDAO();
                UsuarioVO u = udao.GetUsuarioByNomeSenha(nomeUsuario, oldpwd);

                /*
                Usuario usuario = (from obj in db.Usuarios
                                   where (obj.Acesso != null) &&
                                         (obj.Acesso.NomeUsuario == nomeUsuario) &&
                                         (obj.Acesso.SenhaAcesso == oldpwd)
                                   select obj).Include(a => a.Acesso).SingleOrDefault();
                */

                if (u != null)
                {
                    u.SenhaAcesso = Criptografia.MD5(model.NewPassword);
                    /////////////////////////////udao.Update(u);
                    /*
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                     */ 
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "A senha atual está incorreta.");
                }
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            var habilitado = FormsAuthentication.IsEnabled;

            FormsAuthentication.SignOut();
            GerenciadorUsuarioSessao.Limpar();
            return RedirectToAction("Index", "Home");
        }


        // **************************************
        // URL: /Account/Register
        // **************************************
        /*
        public ActionResult Register()
        {
            ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                var requireEmailConfirmation = false;
                var token = WebSecurityService.CreateUserAndAccount(model.UserName, model.Password, requireConfirmationToken: requireEmailConfirmation);

                if (requireEmailConfirmation)
                {
                    // Send email to user with confirmation token
                    string hostUrl = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
                    string confirmationUrl = hostUrl + VirtualPathUtility.ToAbsolute("~/Account/Confirm?confirmationCode=" + HttpUtility.UrlEncode(token));

                    var fromAddress = "Your Email Address";
                    var toAddress = model.Email;
                    var subject = "Thanks for registering but first you need to confirm your registration...";
                    var body = string.Format("Your confirmation code is: {0}. Visit <a href=\"{1}\">{1}</a> to activate your account.", token, confirmationUrl);

                    // NOTE: This is just for sample purposes
                    // It's generally a best practice to not send emails (or do anything on that could take a long time and potentially fail)
                    // on the same thread as the main site
                    // You should probably hand this off to a background MessageSender service by queueing the email, etc.
                    MessengerService.Send(fromAddress, toAddress, subject, body, true);

                    // Thank the user for registering and let them know an email is on its way
                    return RedirectToAction("Thanks", "Account");
                }
                else
                {
                    // Navigate back to the homepage and exit
                    WebSecurityService.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
            return View(model);
        }
        */
        /*
        public ActionResult Confirm()
        {
            string confirmationToken = Request.QueryString["confirmationCode"];
            WebSecurityService.Logout();

            if (!string.IsNullOrEmpty(confirmationToken))
            {
                if (WebSecurityService.ConfirmAccount(confirmationToken))
                {
                    ViewBag.Message = "Registration Confirmed! Click on the login link at the top right of the page to continue.";
                }
                else
                {
                    ViewBag.Message = "Could not confirm your registration info";
                }
            }

            return View();
        }
        */
        // **************************************
        // URL: /Account/ChangePassword
        // **************************************
        /*
        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
            return View();
        }
         */
        /*
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurityService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
            return View(model);
        }
        */

        public ActionResult ForgotPassword()
        {
            return View();
        }
        /*
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            var isValid = false;
            var resetToken = string.Empty;

            if (ModelState.IsValid)
            {
                if (WebSecurityService.GetUserId(model.UserName) > -1 && WebSecurityService.IsConfirmed(model.UserName))
                {
                    resetToken = WebSecurityService.GeneratePasswordResetToken(model.UserName);
                    isValid = true;
                }

                if (isValid)
                {
                    string hostUrl = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
                    string resetUrl = hostUrl + VirtualPathUtility.ToAbsolute("~/Account/PasswordReset?resetToken=" + HttpUtility.UrlEncode(resetToken));

                    var fromAddress = "Your Email Address";
                    var toAddress = model.Email;
                    var subject = "Password reset request";
                    var body = string.Format("Use this password reset token to reset your password. <br/>The token is: {0}<br/>Visit <a href='{1}'>{1}</a> to reset your password.<br/>", resetToken, resetUrl);

                    MessengerService.Send(fromAddress, toAddress, subject, body, true);
                }
                return RedirectToAction("ForgotPasswordMessage");
            }
            return View(model);
        }
        */
        public ActionResult ForgotPasswordMessage()
        {
            return View();
        }

        public ActionResult PasswordReset()
        {
            return View();
        }
        /*
        [HttpPost]
        public ActionResult PasswordReset(PasswordResetModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurityService.ResetPassword(model.ResetToken, model.NewPassword))
                {
                    return RedirectToAction("PasswordResetSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The password reset token is invalid.");
                }
            }

            return View(model);
        }
        */
        public ActionResult PasswordResetSuccess()
        {
            return View();
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult Thanks()
        {
            return View();
        }
    }
}

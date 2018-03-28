using InternetShop.Identity.Managers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using InternetShop.Identity.Entities;
using System.Security.Claims;
using InternetShop.Models.IdentityModels;
using Microsoft.Owin.Security.DataProtection;

namespace InternetShop.Controllers
{
    public class AccountController : Controller
    {
        public ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        public ApplicationRoleManager RoleManager => HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        public IAuthenticationManager AuthManager => HttpContext.GetOwinContext().Authentication;

        //Регистрация
        [Route("register")]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { Email = model.Email, UserName = model.Login };
                var identityResult = await UserManager.CreateAsync(user, model.Password);

                if (identityResult.Succeeded)
                {
                    string token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    string callBack = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Url.Scheme);

                    await UserManager.SendEmailAsync(user.Id, "Confirm email", $"<p>To complete registration, " +
                         $"please</p> <a href=\"{callBack}\">click on me</a> ");

                    ViewBag.title = $"На электронный адрес {user.Email} отправлены дальнейшие инструкции по завершению регистрации.";
                    return View("SendConfirmationCode");

                }
                else identityResult.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
            }
            return View("Register");
        }


        /*Проверка кода подтверждения, отправленного на email.
          Также устанавливает значение True в строке EmailConfirm в базе данных для этого пользователя,
          если метод выполнится корректно.*/

        //GET: /Account/ConfirmEmail/
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId != null && token != null)
            {
                var identityResult = await UserManager.ConfirmEmailAsync(userId, token);

                ViewBag.title = "Благодарим за подтверждение электронной почты.";
                return View(identityResult.Succeeded ? "ConfirmationSuccess" : "Error");
            }
            else return View("Error");
        }

        /*Вход в аккаунт*/
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Login, model.Password);
                if (user != null)
                {
                    ClaimsIdentity claimsIdentity = await UserManager.CreateIdentityAsync(user,
                        DefaultAuthenticationTypes.ApplicationCookie);

                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, claimsIdentity);

                    if (!String.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else return Redirect("/Home/Index");
                }
                else ModelState.AddModelError("", "Wrong login or password.");
            }
            ViewBag.returnUrl = returnUrl;
            return View("Login");
        }

        /*Выход из аккаунта*/
        [Route("signout")]
        public ActionResult SignOut()
        {
            AuthManager.SignOut();

            return Redirect("/Home/Index");
        }

        /// <summary>
        /// Восстановление пароля
        /// </summary>

        /*Запрашивает email пользователя для отправки кода подтверждения*/
        [Route("forgot-password")]
        public ActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user != null && (await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    string token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                    string callBack = Url.Action("ResetPassword", "Account", new { userId = user.Id, token }, Request.Url.Scheme);

                    await UserManager.SendEmailAsync(user.Id, "Confirm email", $"<p>To reset your password, " +
                         $"please</p> <a href=\"{callBack}\">click on me</a> ");

                    ViewBag.title = $"На электронный адрес {user.Email} отправлены дальнейшие инструкции для создания нового пароля.";
                    return View("SendConfirmationCode");
                }
            }
            ModelState.AddModelError("", "Email can't be found");
            return View("ForgotPassword",model);
        }

        /*Запрашивает новый пароль у пользователя*/
        //GET: /Account/ResetPassword
        public ActionResult ResetPassword(string userId, string token)
        {
            if (userId != null && token != null)
            {
                ViewBag.userId = userId;
                ViewBag.token = token;
                return View("ResetPassword");
            }
            else return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (model.UserId != null && model.Token != null)
            {
                if (ModelState.IsValid)
                {
                    var identityResult = await UserManager.ResetPasswordAsync(model.UserId, model.Token, model.Password);

                    ViewBag.title = "Ваш пароль изменен.";
                    return View(identityResult.Succeeded ? "ConfirmationSuccess" : "Error");
                }
                else return View("ResetPassword");
            }
            else return View("Error");
        }
    }
}
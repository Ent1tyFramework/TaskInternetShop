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
                    //var provider = new DpapiDataProtectionProvider("InternetShop");
                    //UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));

                    string token = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                    string callBack = /*Url.RouteUrl("ConfirmEmail", new { userId = user.Id, token },
                        protocol: Request.Url.Scheme);*/
                        Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Url.Scheme);

                    await UserManager.SendEmailAsync(user.Id, "Confirm email", $"<p>For a registration competion " +
                         $"please</p> <a href=\"{callBack}\">click on me</a> ");

                    ViewBag.email = user.Email;

                    return View("SendConfirmationCode");

                }
                else identityResult.Errors.ToList().ForEach(error => ModelState.AddModelError("", error));
            }
            return View("Register");
        }

        //[Route("confirm/{userId}/{token}", Name = "ConfirmEmail")]
        public async Task<object> ConfirmEmail(string userId, string token)
        {
            if (userId != null && token != null)
            {
                var identityResult = await UserManager.ConfirmEmailAsync(userId, token);

                string errors = null;
                if (identityResult.Succeeded == true) return View("EmailConfirmationSuccess");
                else
                {
                    identityResult.Errors.ToList().ForEach(error => errors += error + "\n");
                    return errors;
                }
            }
            else return View("Error");
        }

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

        [Route("signout")]
        public ActionResult SignOut()
        {
            AuthManager.SignOut();

            return Redirect("/Home/Index");
        }
    }
}
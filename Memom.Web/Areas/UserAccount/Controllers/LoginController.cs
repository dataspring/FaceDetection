using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using System.Security.Claims;

using MemomMvc52.Areas.UserAccount;
using MemomMvc52.Areas.UserAccount.Models;
using Memom.Service;
using MemomMvc52.Utilities;
using Memom.Entities.Models;


namespace MemomMvc52.Areas.UserAccount.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        UserService userSvc;

        public LoginController(
            UserService userSvc)
        {
            this.userSvc = userSvc;
        }

        public ActionResult Index()
        {
            return View(new LoginInputModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<AuthResult> result;
                result = userSvc.WdaAuthentication(model.Username, model.Password, "Login-through-MVC-App", HttpContext.Request.ServerVariables["REMOTE_ADDR"]);

                if (result.First().AuthOutCome == 1)
                {
                    //authSvc.SignIn(account, model.RememberMe);
                    ControllerContext.HttpContext.Response.Cookies.Add(WebUtil.PrepCookie(model.Username, model.RememberMe));


                    if (result.First().PasswordReset == 1)
                    {
                        // this might mean many things -- 
                        // it might just mean that the user should change the password, 
                        // like the expired password below, so we'd just redirect to change password page
                        // or, it might mean the DB was compromised, so we want to force the user
                        // to reset their password but via a email token, so we'd want to 
                        // let the user know this and invoke ResetPassword and not log them in
                        // until the password has been changed
                        //userAccountService.ResetPassword(account.ID);

                        // so what you do here depends on your app and how you want to define the semantics
                        // of the RequiresPasswordReset property
                    }
                    
                    //if (userAccountService.IsPasswordExpired(account))
                    //{
                    //    return RedirectToAction("Index", "ChangePassword");
                    //}

                   
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or Password");
                }
            }

            return View(model);
        }


    }
}

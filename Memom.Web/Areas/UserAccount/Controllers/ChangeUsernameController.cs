﻿using WdaMvc52.Areas.UserAccount; 
using WdaMvc52.Areas.UserAccount.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BrockAllen.MembershipReboot;

namespace WdaMvc52.Areas.UserAccount.Controllers
{
    [Authorize]
    public class ChangeUsernameController : Controller
    {
        UserAccountService<CustomUserAccount> userAccountService;
        AuthenticationService<CustomUserAccount> authSvc;

        public ChangeUsernameController(
            UserAccountService<CustomUserAccount> userAccountService, AuthenticationService<CustomUserAccount> authSvc)
        {
            this.userAccountService = userAccountService;
            this.authSvc = authSvc;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ChangeUsernameInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.userAccountService.ChangeUsername(User.GetUserID(), model.NewUsername);
                    this.authSvc.SignIn(User.GetUserID());
                    return RedirectToAction("Success");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View("Index", model);
        }

        public ActionResult Success()
        {
            return View("Success", (object)User.Identity.Name);
        }
    }
}

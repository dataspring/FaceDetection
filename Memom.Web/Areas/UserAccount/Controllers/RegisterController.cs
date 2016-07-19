using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;


using MemomMvc52.Areas.UserAccount;
using MemomMvc52.Areas.UserAccount.Models;
using Memom.Service;
using MemomMvc52.Utilities;
using Memom.Service;

namespace MemomMvc52.Areas.UserAccount.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        UserService userSvc;

        public RegisterController(UserService userSvc)
        {
            this.userSvc = userSvc;
        }

        public ActionResult Index()
        {
            return View(new RegisterInputModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(RegisterInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = this.userSvc.WdaRegisterAccount(model.FirstName, model.LastName, model.DisplayName, model.Email, model.Password, Request.ServerVariables["REMOTE_ADDR"]);
                    
                    // add our custom stuff
                    //account.FirstName = model.FirstName;
                    //account.LastName = model.LastName;
                    //account.DisplayName = model.DisplayName;
                    //this.userAccountService.Update(account);

                    //ViewData["RequireAccountVerification"] = this.userAccountService.Configuration.RequireAccountVerification;
                    if (account.IsRegistered <= 0 )
                    {
                        ModelState.AddModelError("", account.Remarks);
                    }
                    else
                        return View("Success", model);
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        public ActionResult Verify()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Verify(string foo)
        //{
        //    try
        //    {
        //        this.userAccountService.RequestAccountVerification(User.GetUserID());
        //        return View("Success");
        //    }
        //    catch (ValidationException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //    }
        //    return View();
        //}

        //public ActionResult Cancel(string id)
        //{
        //    try
        //    {
        //        bool closed;
        //        this.userAccountService.CancelVerification(id, out closed);
        //        if (closed)
        //        {
        //            return View("Closed");
        //        }
        //        else
        //        {
        //            return View("Cancel");
        //        }
        //    }
        //    catch(ValidationException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //    }
        //    return View("Error");
        //}
    }
}

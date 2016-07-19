using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
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
    public class PasswordResetController : Controller
    {
        UserService userSvc;


        public PasswordResetController(UserService userSvc)
        {
            this.userSvc = userSvc;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PasswordResetInputModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = this.userSvc.WdaForgotPassword(model.Email, HttpContext.Request.ServerVariables["REMOTE_ADDR"]);

                    if (account.IsPasswordReset <= 0)
                    {
                        ModelState.AddModelError("", account.Remarks);
                    }
                    else
                        return View("ResetSuccess");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View("Index");
        }


        public ActionResult Success()
        {
            return View();
        }
    }
}

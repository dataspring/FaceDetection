using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using MemomMvc52.Areas.UserAccount;
using MemomMvc52.Areas.UserAccount.Models;
using Memom.Service;
using MemomMvc52.Utilities;
using Memom.Service;
using Memom.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace MemomMvc52.Areas.UserAccount.Controllers
{
    [Authorize]
    public class ChangePasswordController : Controller
    {
        UserService userSvc;
        public ChangePasswordController(UserService userSvc)
        {
            this.userSvc = userSvc;
        }

        public ActionResult Index()
        {
            //var acct = this.userSvc.UserDetails(User.Identity.Name);
            return View(new ChangePasswordInputModel());

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ChangePasswordInputModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    ChangePwdResult changePwd =   this.userSvc.WdaChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword, Request.ServerVariables["REMOTE_ADDR"]);
                    if (changePwd.IsPasswordChanged == 1)
                        return View("Success");
                    else
                        ModelState.AddModelError("ErrorPwdChange", changePwd.Remarks);
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }


    }
}
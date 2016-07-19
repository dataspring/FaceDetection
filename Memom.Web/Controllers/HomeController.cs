using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using MemomMvc52.Models;
using MemomMvc52.Utilities;

namespace MemomMvc52.Controllers
{
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public ActionResult Launch()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Download()
        {
            ViewBag.Message = "Play the game and spot the errors today";

            return View();
        }

        [AllowAnonymous]
        public ActionResult TermsOfUse()
        {
            ViewBag.Message = "for App & WebSite";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Privacy()
        {
            ViewBag.Message = "for App & WebSite";

            return View();
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToRoute("LeaderBoard");
            }
        }
    }
}
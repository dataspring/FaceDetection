using System;
using System.Security.Claims;
using System.Web.Mvc;
using Memom.Service;

namespace MemomMvc52.Areas.UserAccount.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        MemberService _membSvc;

        public HomeController(MemberService membSvc)
        {
            this._membSvc = membSvc;
        }

        public ActionResult Index()
        {
            return View(_membSvc.DashboardScores(User.Identity.Name));
        }

        //[Authorize]
        //[HttpPost]
        //public ActionResult Index(string gender)
        //{

        //    return RedirectToAction("Index");
        //}

    }
}

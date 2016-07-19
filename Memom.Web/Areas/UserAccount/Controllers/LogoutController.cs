using System.Web.Mvc;
using System.Web.Security;
using Memom.Service;

namespace MemomMvc52.Areas.UserAccount.Controllers
{
    public class LogoutController : Controller
    {
        AlbumService gameSvc;

        public LogoutController(AlbumService gameSvc)
        {
            this.gameSvc = gameSvc;
        }
        
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //authSvc.SignOut();
                FormsAuthentication.SignOut();
                return Redirect("/");
            }
            
            return View();
        }

    }
}

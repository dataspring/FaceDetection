using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemomMvc52.Areas.MemomWeb.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Admin")]
    public class FileUploadTestController : Controller
    {
        // GET: MemomWeb/FileUploadTest
        public ActionResult Index()
        {
            return View();
        }

        
    }
}

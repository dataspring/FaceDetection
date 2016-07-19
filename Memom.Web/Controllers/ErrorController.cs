using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemomMvc52.Models;
using MemomMvc52.Utilities;

namespace MemomMvc52.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error404/

        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult UnAuthorized()
        {
            Response.StatusCode = 401;
            return View();
        }

        public ActionResult CustomError()
        {
            Response.StatusCode = 500;
            return View();
        }

    }
}

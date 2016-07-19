using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MemomMvc52.Models;
using Memom.Service;



namespace MemomMvc52.Controllers
{
    public class AboutController : Controller
    {

        // GET: About
        public ActionResult Index()
        {
            ViewBag.Message = "MeMom IntelliFace App: With Face Identification Tech";
            return View();
        }

    }
}
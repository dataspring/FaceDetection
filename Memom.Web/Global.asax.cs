using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using System.Globalization;
using System.Security.Claims;
using System.Web.Http;
using System.Threading;
using System.Web.Helpers;
using MemomMvc52.App_Start;



namespace MemomMvc52
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.ConfigureApiFormatter);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        void Application_PostAuthenticateRequest()
        {
            //if (Request.IsAuthenticated)
            //{
            //    var id = ClaimsPrincipal.Current.Identities.First();
            //    if (User.IsInRole("Admin"))
            //        id.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            //    if (User.IsInRole("User"))
            //        id.AddClaim(new Claim(ClaimTypes.Role, "User"));
            //}
        }
    }
}

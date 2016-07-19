using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MemomMvc52
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            /* --------------------------- User Management {provided through Thinktecture.IdentityManager SPA UI - removed} -------------------------------- */
            routes.MapRoute(
                 name: "UserApp",
                 url: "UserApp/",
                 defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional },
                 namespaces: new string[] { "MemomMvc52.Areas.UserApp.Controllers" }
             );


            /* --------------------------- About Menu Items -------------------------------- */

            routes.MapRoute(
                name: "About",
                url: "about/",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "MemomMvc52.Controllers" }
            );

            routes.MapRoute(
                name: "AlbumDetails",
                url: "gamedetails/",
                defaults: new { controller = "AlbumDetails", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "MemomMvc52.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "contact/",
                defaults: new { controller = "About", action = "Contact", id = UrlParameter.Optional },
                namespaces: new string[] { "MemomMvc52.Controllers" }
            );

            routes.MapRoute(
                name: "Instructions",
                url: "instructions/",
                defaults: new { controller = "About", action = "Instructions", id = UrlParameter.Optional },
                namespaces: new string[] { "MemomMvc52.Controllers" }
            );




            /* --------------------------- Main Menu & Common Items -------------------------------- */

            routes.MapRoute(
                name: "Download",
                url: "download/",
                defaults: new { controller = "Home", action = "Download", id = UrlParameter.Optional },
                namespaces: new string[] { "MemomMvc52.Controllers" }
            );

            routes.MapRoute(
                name: "TermsOfUse",
                url: "termsofuse/",
                defaults: new { controller = "Home", action = "TermsOfUse", id = UrlParameter.Optional },
                namespaces: new string[] { "MemomMvc52.Controllers" }
            );

            routes.MapRoute(
                name: "Privacy",
                url: "privacy/",
                defaults: new { controller = "Home", action = "Privacy", id = UrlParameter.Optional },
                namespaces: new string[] { "MemomMvc52.Controllers" }
            );

            routes.MapRoute(
                name: "CustomError",
                url: "customerror/{action}/{id}",
                defaults: new { controller = "Error", action = "CustomError", id = UrlParameter.Optional },
                namespaces: new string[] { "MemomMvc52.Controllers" }
            );

            routes.MapRoute(
                name: "Error404",
                url: "lookingforawhale/{action}/{id}",
                defaults: new { controller = "Error", action = "Error404", id = UrlParameter.Optional },
                namespaces: new string[] { "MemomMvc52.Controllers" }
            );

            routes.MapRoute(
                name: "UnAuthorized",
                url: "UnAuthorized/",
                defaults: new { controller = "Error", action = "UnAuthorized", id = UrlParameter.Optional },
                namespaces: new string[] { "MemomMvc52.Controllers" }
            );
            

            /* --------------------------- Progressboard Menu Items -------------------------------- */



            /* --------------------------- Leaderboard Menu Items -------------------------------- */

            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new string[] { "MemomMvc52.Controllers" }
             ).DataTokens.Add("Area", "UserApp");
        }
    }
}

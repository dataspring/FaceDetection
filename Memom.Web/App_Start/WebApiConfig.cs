using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MemomMvc52.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // to remove convention based routing ----- http://www.itorian.com/2014/03/attribute-routing-in-mvc-and-web-api.html
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

        }

        public static void ConfigureApiFormatter(HttpConfiguration config)
        {
            // Remove the JSON formatter
            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            // or

            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}

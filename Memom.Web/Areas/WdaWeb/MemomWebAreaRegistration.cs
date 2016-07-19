using System.Web.Mvc;

namespace MemomMvc52.Areas.MemomWeb
{
    public class MemomWebAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MemomWeb";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MemomWeb_default",
                "MemomWeb/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
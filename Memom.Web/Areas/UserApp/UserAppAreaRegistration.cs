using System.Web.Mvc;

namespace MemomMvc52.Areas.UserApp
{
    public class UserAppAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UserApp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {


            context.MapRoute(
                "UserApp_default",
                "UserApp/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
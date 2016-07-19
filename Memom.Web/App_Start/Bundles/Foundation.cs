using System.Web.Optimization;

namespace MemomMvc52
{
    public static class Foundation
    {
        public static Bundle Scripts()
        {
            return new ScriptBundle("~/bundles/foundation").Include(
                      "~/Scripts/foundation/fastclick.js",
                      "~/Scripts/jquery.cookie.js",
                      "~/Scripts/foundation/foundation.js",
                      //"~/Scripts/foundation/foundation.*",
                      //"~/Scripts/foundation/foundation.dropdown.js",
                      "~/Scripts/foundation/foundation.tab.js",
                      "~/Scripts/foundation/foundation.clearing.js",
                      "~/Scripts/foundation/foundation.interchange.js",
                      //"~/Scripts/foundation/foundation.reveal.js",
                      "~/Scripts/foundation/foundation.tooltip.js",
                      "~/Scripts/foundation/foundation.topbar.js",
                      "~/Scripts/foundation/app.js",
                      "~/Scripts/stickyFooter.js");
        }

        public static Bundle Styles()
        {
            //not used and saas takes over !-!-!-!-!-!-!-!-!
            return new StyleBundle("~/stylesheets/foundation/css").Include(
                       "~/stylesheets/foundation/normalize.css",
                       "~/stylesheets/foundation/foundation.css",
                       "~/stylesheets/foundation/foundation.mvc.css",
                       "~/Content/foundation/app.css");
        }
    }
}
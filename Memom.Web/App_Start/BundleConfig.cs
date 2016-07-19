using System.Web;
using System.Web.Optimization;

namespace MemomMvc52
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/dataTable").Include(
            //"~/Scripts/jquery.dataTables*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/stylesheets/site.css",
                        "~/stylesheets/app.css",
                        "~/stylesheets/jQuery.dataTables.css"
                        //"~/Content/foundation-datepicker.css"
                        ));

            #region Foundation Bundles

            //bundles.Add(Foundation.Styles());

            bundles.Add(Foundation.Scripts());

            #endregion
        }
    }
}

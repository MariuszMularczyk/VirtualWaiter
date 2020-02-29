using System.Web;
using System.Web.Optimization;

namespace VirtualWaiter
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region scriptBundle
            var scriptBundle = new ScriptBundle("~/Scripts/jqueryGlobalize");
            scriptBundle.Include(
                 "~/Scripts/jquery-{version}.js",
                 "~/Scripts/jquery.scrollbar.min.js",
                 "~/Scripts/js.cookie.js",
                 "~/Scripts/jquery.unobtrusive-ajax.min.js",
                   "~/Scripts/main.js"

                 );
            // CLDR scripts
            scriptBundle
                .Include("~/Scripts/cldr.js")
                .Include("~/Scripts/cldr/event.js")
                .Include("~/Scripts/cldr/supplemental.js")
                .Include("~/Scripts/cldr/unresolved.js");

            // Globalize 1.x
            scriptBundle
                .Include("~/Scripts/globalize.js")
                .Include("~/Scripts/globalize/message.js")
                .Include("~/Scripts/globalize/number.js")
                .Include("~/Scripts/globalize/currency.js")
                .Include("~/Scripts/globalize/date.js")
                .Include("~/Scripts/globalize.extensions.js");

            bundles.Add(scriptBundle);
            #endregion

            #region Modernizr
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            #endregion

            #region bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/main.min.css"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryBase64").Include(
                      "~/Scripts/jquery.base64.js"
                      ));
            #endregion

            #region jquery
            bundles.Add(new ScriptBundle("~/bundles/jqueryBase64").Include(
                "~/Scripts/jquery.base64.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            #endregion

#if !DEBUG
                                BundleTable.EnableOptimizations = false;
#endif
        }
    }
}

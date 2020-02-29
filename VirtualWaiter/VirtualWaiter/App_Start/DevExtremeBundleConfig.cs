using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace VirtualWaiter
{

    public class DevExtremeBundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {

            var scriptBundle = new ScriptBundle("~/Scripts/DevExtremeBundle");
            var styleBundle = new StyleBundle("~/Content/DevExtremeBundle");

            // NOTE: jQuery may already be included in the default script bundle. Check the BundleConfig.cs file​​​
            //scriptBundle
            //    .Include("~/Scripts/jquery-2.2.3.js");

            // JSZip for client side export
            scriptBundle
                .Include("~/Scripts/jszip.js");

            // DevExtreme + extensions
            scriptBundle
                .Include("~/Scripts/dx.all.js")
                .Include("~/Scripts/aspnet/dx.aspnet.data.js")
                .Include("~/Scripts/aspnet/dx.aspnet.mvc.js");

            // dxVectorMap data (http://js.devexpress.com/Documentation/Guide/Data_Visualization/VectorMap/Providing_Data/#Data_for_Areas)
            //scriptBundle
            //    .Include("~/Scripts/vectormap-data/africa.js")
            //    .Include("~/Scripts/vectormap-data/canada.js")
            //    .Include("~/Scripts/vectormap-data/eurasia.js")
            //    .Include("~/Scripts/vectormap-data/europe.js")
            //    .Include("~/Scripts/vectormap-data/usa.js")
            //    .Include("~/Scripts/vectormap-data/world.js");


            // DevExtreme
            // NOTE: see the available theme list here: http://js.devexpress.com/Documentation/Guide/Themes/Predefined_Themes/                    
            styleBundle
                .Include("~/Content/dx.common.css")
                .Include("~/Content/VirtualWaiter.theme.css");


            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);

            var scriptBundlePL = new ScriptBundle("~/Scripts/DevExtremeBundlePL");
            scriptBundlePL.Include("~/Scripts/globalize.load.pl.js");
            scriptBundlePL.Include("~/Scripts/dx.all.pl.js");
            bundles.Add(scriptBundlePL);

            var scriptBundleDE = new ScriptBundle("~/Scripts/DevExtremeBundleDE");
            scriptBundleDE.Include("~/Scripts/globalize.load.de.js");
            scriptBundleDE.Include("~/Scripts/localization/dx.messages.de.js");
            bundles.Add(scriptBundleDE);

            var scriptBundleEN = new ScriptBundle("~/Scripts/DevExtremeBundleEN");
            scriptBundleEN.Include("~/Scripts/globalize.load.en.js");
            bundles.Add(scriptBundleEN);

        }
    }
}
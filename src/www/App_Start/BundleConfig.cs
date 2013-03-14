﻿using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Transformers;
using System.Web.Optimization;

namespace nTestSwarm
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
#if RELEASE
            BundleTable.EnableOptimizations = true;
            bundles.UseCdn = true;
#else
            BundleTable.EnableOptimizations = false;
            bundles.UseCdn = false;
#endif

            bundles.RegisterStyles();
            bundles.RegisterScripts();
        }

        private static void RegisterScripts(this BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/json2.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/underscore.js",
                "~/Scripts/knockout-{version}.debug.js",
                "~/Scripts/underscore-ko-{version}.js",
                "~/Scripts/jquery.signalR-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/validate").Include(
                "~/Scripts/jquery.validate.js",
                "~/scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive-custom-for-bootstrap.js"));
        }

        private static void RegisterStyles(this BundleCollection bundles)
        {
            var cssTransformer = new CssTransformer();
            var nullOrderer = new NullOrderer();

            // can mix less, sass and css together
            var sytleBundle = new Bundle("~/styles", cssTransformer).Include(
                "~/Content/body.css",
                "~/Content/site.less");

            sytleBundle.Orderer = nullOrderer;

            bundles.Add(sytleBundle);
        }
    }
}
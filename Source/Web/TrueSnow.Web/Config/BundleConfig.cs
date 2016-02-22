namespace TrueSnow.Web.Config
{
    using System.Web.Optimization;

    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-2.2.0.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/kendo").Include("~/Scripts/KendoUI/kendo.custom.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/fileinput.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/custom.js",
                      "~/Scripts/sweetalert-dev.js",
                      "~/Scripts/site.js"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/custom.min.css",
                      "~/Content/fileinput.min.css",
                      "~/Content/sweetalert.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                      "~/Content/KendoUI/kendo.common.min.css",
                      "~/Content/KendoUI/kendo.office365.min.css"));
        }
    }
}

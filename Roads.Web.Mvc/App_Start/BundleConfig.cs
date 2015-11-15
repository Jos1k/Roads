using System.Web;
using System.Web.Optimization;

namespace Roads.Web.Mvc
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Themes/css").Include(
            "~/Content/css/themes/theme-green.css"));

            bundles.Add(new StyleBundle("~/Bootstrap/css").Include(
                        "~/Content/bootstrap/css/bootstrap.css",
                        "~/Content/bootstrap/css/bootstrap-theme.css",
                        "~/Content/bootstrap/css/bootstrap-min.css",
                        "~/Content/bootstrap/css/bootstrap.css.map",
                        "~/Content/bootstrap/css/bootstrap-theme.min.css"));



            bundles.Add(new ScriptBundle("~/jquery/js").Include(
                        "~/Content/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bootstrap/js").Include(
                        "~/Content/bootstrap/js/bootstrap.js",
                        "~/Content/bootstrap/js/bootbox.js",
                        "~/Content/bootstrap/js/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/bootstrap/css").Include(
                        "~/Content/bootstrap/css/bootstrap.min.css", new CssRewriteUrlTransform()).Include(
                        "~/Content/bootstrap/css/bootstrap-theme.css"));

            bundles.Add(new StyleBundle("~/addRoadStepTwo/css").Include(
                        "~/Content/css/addRoadStepTwo.css"));

            bundles.Add(new StyleBundle("~/Settings/css").Include(
            "~/Content/css/spinner.css"));

            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
            "~/Content/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Site/css").Include(
           "~/Content/css/Site.css"));

            bundles.Add(new ScriptBundle("~/SiteSettings/js").Include(
                        "~/Content/Scripts/siteSettings.js"));

            bundles.Add(new StyleBundle("~/loginPage/css").Include(
            "~/Content/css/loginPage.css"));

            bundles.Add(new ScriptBundle("~/AddRoadStepOne/js").Include(
                "~/Content/AngularJS/AddRouteApplication/vendor/angular.js",
                       "~/Content/AngularJS/AddRouteApplication/controllers/AddRoadController.js"));

            bundles.Add(new ScriptBundle("~/FindRoad/js").Include(
                "~/Content/AngularJS/AddRouteApplication/vendor/angular.js",
                       "~/Content/AngularJS/AddRouteApplication/controllers/FindRoadController.js"));

            bundles.Add(new StyleBundle("~/AddRoadStepOne/css").Include(
                        "~/Content/css/loginPage.css",
                        "~/Content/css/Site.css",
                        "~/Content/AngularJS/AddRouteApplication/stylesheets/addRoad.css"));

            bundles.Add(new StyleBundle("~/css/find-road-details").Include(
                "~/Content/css/find-road-details.css"));

            bundles.Add(new ScriptBundle("~/js/find-road-details").Include(
                "~/Content/Scripts/find-road-details.js"));

            bundles.Add(new StyleBundle("~/feebBackInput/css").Include(
           "~/Content/css/feebBackInput.css"));

            bundles.Add(new ScriptBundle("~/AddRoadStepTwo/js").Include(
                        "~/Content/Scripts/AddRoadStepTwo/AddRoadStepTwo.js"));
        }
    }
}
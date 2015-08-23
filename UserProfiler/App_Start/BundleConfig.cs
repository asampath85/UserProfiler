using System.Web;
using System.Web.Optimization;

namespace UserProfiler
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterJavascriptBundles(bundles);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css")
                            .Include("~/Content/bootstrap.css")
                            .Include("~/Content/bootstrap-theme.css")
                            //.Include("~/Content/site.css")
                            );
        }

        private static void RegisterJavascriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery-ui-{version}.js")
                .Include("~/Scripts/Highcharts-4.0.1/js/highcharts.js")
                .Include("~/Scripts/knockout-2.2.0.js")
                .Include("~/Scripts/twitter/TwitterViewModel.js")
                .Include("~/Scripts/Weather/WeatherViewModel.js")
                .Include("~/Scripts/facebook/FacebookViewModel.js")
                .Include("~/Scripts/bootstrap.js"));

        }
    }
}
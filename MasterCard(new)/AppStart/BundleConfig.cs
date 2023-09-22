using System.Web.Optimization;

namespace MasterCard_new_.AppStart
{
    public partial class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js").Include(
                "~/bower_components/jquery/dist/jquery.js",
                "~/bower_components/bootstrap/dist/bootstrap.js"));

            bundles.Add(new StyleBundle("~/css").Include(
                "~/bower_components/bootstrap/dist/css/bootsstrap.css",
                "~/Content/Site.css"));
        }
    }
}

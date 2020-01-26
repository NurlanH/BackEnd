using System.Web;
using System.Web.Optimization;

namespace DevFormAz
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/js").Include(
                        "~/Public/Jquery/jquery-3.4.1.min.js",
                        "~/Public/Particle/jquery.particleground.min.js",
                        "~/Public/Bootstrap/js/bootstrap.min.js",
                        "~/Public/Script/MainScript.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Public/Bootstrap/css/bootstrap.min.css",
                      "~/Public/FontAwesome/css/all.css",
                      "~/Public/Animation/animate.css",
                      "~/Public/Css/MainStyle.css"
                      ));
        }
    }
}

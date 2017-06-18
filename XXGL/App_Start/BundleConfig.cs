using System.Web;
using System.Web.Optimization;

namespace XXGL
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Basic/js").Include(
                    "~/Scripts/gentelella/jquery/jquery.min.js",
                    "~/Scripts/gentelella/bootstrap/js/bootstrap.min.js",
                    "~/Scripts/gentelella/fastclick/fastclick.js",
                    "~/Scripts/gentelella/nprogress/nprogress.js",
                    "~/Scripts/gentelella/iCheck/icheck.min.js",
                    "~/Scripts/gentelella/build/js/custom.min.js"
                ));

            bundles.Add(new StyleBundle("~/Basic/css").Include(
                      "~/Scripts/gentelella/bootstrap/css/bootstrap.css",
                      "~/Scripts/gentelella/font-awesome/css/font-awesome.css",
                       "~/Scripts/gentelella/nprogress/nprogress.css",
                      "~/Scripts/gentelella/iCheck/skins/flat/grey.css",
                      "~/Scripts/gentelella/build/css/custom.min.css"
                      ));



            // 将 EnableOptimizations 设为 false 以进行调试。有关详细信息，
            // 请访问 http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}

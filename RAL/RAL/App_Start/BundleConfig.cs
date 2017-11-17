using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace RAL
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterJavascriptBundles(bundles);
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                            .Include("~/Content/bootstrap.css")
                            //.Include("~/Content/bootstrap-theme.css")
                            .Include("~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/css-datetimepicker")
                            .Include("~/Content/bootstrap-datetimepicker.min.css"));

            /*bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));*/
        }

        private static void RegisterJavascriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js")
                            .Include("~/Scripts/jquery-{version}.js")
                            //.Include("~/Scripts/jquery-ui-{version}.js")
                            .Include("~/Scripts/SiteScripts.js")
                            .Include("~/Scripts/moment.min.js")
                            .Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/js-datetimepicker")
                            .Include("~/Scripts/bootstrap-datetimepicker.min.js"));

            bundles.Add(new ScriptBundle("~/js-AHistory")
                .Include("~/Scripts/local/jquery.tablesorter.js")
                .Include("~/Scripts/local/AHistory.js"));

            bundles.Add(new ScriptBundle("~/js1")
                            .Include("~/Scripts/jquery.unobtrusive-ajax.js")
                            .Include("~/Scripts/jquery.validate.js")
                            .Include("~/Scripts/jquery.validate.unobtrusive.js"));
                            //.Include("~/Scripts/MicrosoftAjax.js")
                            //.Include("~/Scripts/MicrosoftMvcAjax.debug.js"));
        }
    }

}
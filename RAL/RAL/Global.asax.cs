using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using RAL.Infrastructure;
using RAL_DAL;

namespace RAL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());

            ModelBinders.Binders.Add(typeof(User), new CurrentUserBinder());
            ModelBinders.Binders.Add(typeof(List<Watcheda>), new WatchedListBinder());
            ModelBinders.Binders.Add(typeof(List<Plan>), new PlanListBinder());
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            CacheHelper.setCurrentUser(HttpContext.Current);
            //CacheHelper.removeAuthCookies(HttpContext.Current);
        }
    }
}

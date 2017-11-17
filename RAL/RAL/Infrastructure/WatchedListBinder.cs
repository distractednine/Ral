using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RAL_DAL;

namespace RAL.Infrastructure
{
    public class WatchedListBinder: IModelBinder
    {
        const string watchedListKey = "curWatchedList";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            List<Watcheda> watchedList = (List<Watcheda>)controllerContext.HttpContext.Session[watchedListKey];

            return watchedList;
        }

        public static void setWatchedList(ControllerContext controllerContext, List<Watcheda> watchedList)
        {
            controllerContext.HttpContext.Session[watchedListKey] = watchedList;
        }

        public static void setWatchedList(HttpContext httpContext, List<Watcheda> watchedList)
        {
            httpContext.Session[watchedListKey] = watchedList;
        }

        public static void deleteWatchedList(ControllerContext controllerContext)
        {
            controllerContext.HttpContext.Session[watchedListKey] = null;
        }
    }
}
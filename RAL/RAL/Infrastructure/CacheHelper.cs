using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using RAL_DAL;
using RAL.Models.Interfaces;

namespace RAL.Infrastructure
{
    public static class CacheHelper
    {
        public static IRalRepository repository;

        public static void setCurrentUser(HttpContext httpContext)
        {
            int curId;

            try
            {
                curId = Int32.Parse(httpContext.Request.Cookies["userID"].Value.ToString());
            }
            catch (Exception)
            {
                return;
            }

            User curUser = repository.users.FirstOrDefault(u => u.id == curId);

            if (curUser != null)
            {
                cacheCurrentUser(curUser, httpContext);
            }
        }

        public static void cacheCurrentUser(User currentUser, ControllerContext controllerContext)
        {
            CurrentUserBinder.setCurrentUser(controllerContext, currentUser);
            WatchedListBinder.setWatchedList(controllerContext, currentUser.watcheda.ToList<Watcheda>());
            PlanListBinder.setPlanList(controllerContext, currentUser.plans.ToList<Plan>());
        }

        public static void cacheCurrentUser(User currentUser, HttpContext httpContext)
        {
            CurrentUserBinder.setCurrentUser(httpContext, currentUser);
            WatchedListBinder.setWatchedList(httpContext, currentUser.watcheda.ToList<Watcheda>());
            PlanListBinder.setPlanList(httpContext, currentUser.plans.ToList<Plan>());
        }

        public static void deleteCurrentUserCache(ControllerContext controllerContext)
        {
            CurrentUserBinder.deleteCurrentUser(controllerContext);
            WatchedListBinder.deleteWatchedList(controllerContext);
            PlanListBinder.deletePlanList(controllerContext);

            removeAuthCookies(controllerContext);
        }

        public static void removeAuthCookies(HttpContext httpContext)
        {
            if (httpContext.Request.Cookies[".ASPXAUTH"] != null)
            {
                HttpCookie myCookie = new HttpCookie(".ASPXAUTH");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                httpContext.Response.Cookies.Add(myCookie);
            }
        }

        public static void removeAuthCookies(ControllerContext controllerContext)
        {
            if (controllerContext.HttpContext.Request.Cookies[".ASPXAUTH"] != null)
            {
                HttpCookie myCookie = new HttpCookie(".ASPXAUTH");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                controllerContext.HttpContext.Response.Cookies.Add(myCookie);
            }
        }
    }
}
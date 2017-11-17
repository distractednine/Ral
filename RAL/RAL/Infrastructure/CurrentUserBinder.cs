using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RAL_DAL;

namespace RAL.Infrastructure
{
    public class CurrentUserBinder: IModelBinder
    {
        const string userKey = "curUser";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            User user = (User)controllerContext.HttpContext.Session[userKey];

            return user;
        }

        public static void setCurrentUser(ControllerContext controllerContext, User user)
        {
            controllerContext.HttpContext.Session[userKey] = user;
        }

        public static void setCurrentUser(HttpContext httpContext, User user)
        {
            httpContext.Session[userKey] = user;
        }

        public static void deleteCurrentUser(ControllerContext controllerContext)
        {
            controllerContext.HttpContext.Session[userKey] = null;
        }
    }
}
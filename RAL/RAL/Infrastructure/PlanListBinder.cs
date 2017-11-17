using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RAL_DAL;

namespace RAL.Infrastructure
{
    public class PlanListBinder: IModelBinder
    {
        const string planListKey = "curPlanList";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            List<Plan> planList = (List<Plan>)controllerContext.HttpContext.Session[planListKey];

            return planList;
        }

        public static void setPlanList(ControllerContext controllerContext, List<Plan> planList)
        {
            controllerContext.HttpContext.Session[planListKey] = planList;
        }

        public static void setPlanList(HttpContext httpContext, List<Plan> planList)
        {
            httpContext.Session[planListKey] = planList;
        }

        public static void deletePlanList(ControllerContext controllerContext)
        {
            controllerContext.HttpContext.Session[planListKey] = null;
        }
    }
}
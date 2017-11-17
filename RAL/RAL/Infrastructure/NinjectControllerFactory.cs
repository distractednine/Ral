using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Ninject;
using RAL.Models;
using RAL.Models.Interfaces;

namespace RAL.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            addBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        void addBindings()
        {
            ninjectKernel.Bind<IRalRepository>().To<EfRalRepository>();
            CacheHelper.repository = new EfRalRepository();
        }

    }
}
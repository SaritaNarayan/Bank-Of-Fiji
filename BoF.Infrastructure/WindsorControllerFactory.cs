using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace RFIS.Infrastructure
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {

        private readonly IWindsorContainer windsorContainer;

        public WindsorControllerFactory(IWindsorContainer windsorContainer)
        {
            this.windsorContainer = windsorContainer;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                return (IController)windsorContainer.Resolve(controllerType);
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            var disposableController = controller as IDisposable;
            if (disposableController != null)
            {
                disposableController.Dispose();
            }

            windsorContainer.Release(controller);
        }

    }
}


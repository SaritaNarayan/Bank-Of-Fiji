using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using BoF.Web.Helpers;

namespace BoF.Web.Helpers
{
    public static class UrlExtensions
    {
        public static string Action(this UrlHelper urlHelper, string actionName, string controllerName)
        {
            return urlHelper.Action(actionName);
        }

        public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, object>> actionExpression) where TController : Controller
        {
            return urlHelper.Action(actionExpression, null);
        }

        public static string Action(this UrlHelper urlHelper, string controllerName, object values)
        {
            var fullControllerName = controllerName;
            const string actionName = "Index";

            if (SecurityHelper.ActionIsAllowedForUser(fullControllerName, actionName) == false)
            {
                return string.Empty;
            }

            return urlHelper.Action(actionName, controllerName, values);
        }

        public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, object>> actionExpression, object values) where TController : Controller
        {
            var fullControllerName = typeof(TController).GetFullControllerName();
            var actionName = actionExpression.GetActionName();

            if (SecurityHelper.ActionIsAllowedForUser(fullControllerName, actionName) == false)
            {
                return string.Empty;
            }

            var controllerName = typeof(TController).GetControllerName();
            return urlHelper.Action(actionName, controllerName, values);
        }

        public static string AreaAction<TController>(this UrlHelper urlHelper, Expression<Func<TController, object>> actionExpression, string areaName) where TController : Controller
        {
            var fullControllerName = typeof(TController).GetFullControllerName();
            var actionName = actionExpression.GetActionName();

            if (SecurityHelper.ActionIsAllowedForUser(fullControllerName, actionName) == false)
            {
                return string.Empty;
            }

            var controllerName = typeof(TController).GetControllerName();
            var routeValueDictionary = new RouteValueDictionary { { "area", areaName } };
            return urlHelper.Action(actionName, controllerName, routeValueDictionary);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using BoF.Web.Models.Security;

namespace BoF.Web.Helpers
{
    public class SettingsHelper
    {

        public static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList();
        }

        public List<ControllerModel> GetControllerNames()
        {
            var controllersModels = new List<ControllerModel>();

            var controllers = GetSubClasses<Controller>();

            foreach (var controller in controllers)
            {
                var controllersModel = new ControllerModel { ControllerName = controller.Name.Replace("Controller", ""), StartPage = "Index" };
                MemberInfo inf = controller;
                var attributes = inf.GetCustomAttributes(typeof(PrettyNameAttribute), false);
                foreach (var attribute in attributes)
                {
                    var prettyNameAttribute = (PrettyNameAttribute)attribute;
                    controllersModel.DisplayName = prettyNameAttribute.PrettyName;
                }

                controllersModels.Add(controllersModel);
            }

            return controllersModels;
        }

        public static List<ControllerModel> GetControllerSettings()
        {
            var controllerModels = new List<ControllerModel>();

            var controllers = GetSubClasses<Controller>();

            foreach (var controller in controllers)
            {
                var controllerModel = new ControllerModel { ControllerName = controller.Name.Replace("Controller", ""), StartPage = "Index" };
                MemberInfo inf = controller;
                var attributes = inf.GetCustomAttributes(typeof(SettingsAttributes), false);
                foreach (var attribute in attributes)
                {
                    var settingsAttributes = (SettingsAttributes)attribute;
                    controllerModel.DisplayName = settingsAttributes.DisplayName;
                    controllerModel.ForNavigation = settingsAttributes.ForNavigation;
                    controllerModel.OrderNumber = settingsAttributes.OrderNumber;
                }

                if (controllerModel.ForNavigation)
                {
                    controllerModels.Add(controllerModel);
                }
            }

            return controllerModels.OrderBy(x => x.OrderNumber).ToList();
        }

        public static List<ActionModel> GetActionsForAllControllers(List<Type> types)
        {
            var models = new List<ActionModel>();
            foreach (var type in types)
            {
                var actionModels = GetActionMethods(type, typeof(ActionResult));
                foreach (var actionModel in actionModels)
                {
                    models.Add(actionModel);
                }
            }

            return models;
        }

        public static List<ActionModel> GetActionMethods(Type controller, Type actionResult)
        {
            var methods = controller.GetMethods(BindingFlags.Public | BindingFlags.Instance);

            var actionModels = new List<ActionModel>();

            foreach (var methodInfo in methods)
            {
                var actionModel = new ActionModel();
                actionModel.ControllerName = controller.Name.Replace("Controller", "");
                actionModel.ActionName = methodInfo.Name;

                if (methodInfo.ReturnType.IsAssignableFrom(actionResult))
                {
                    var attributes = methodInfo.GetCustomAttributes(typeof(SettingsAttributes), false);
                    foreach (var attribute in attributes)
                    {
                        var settingsAttribute = (SettingsAttributes)attribute;
                        actionModel.DisplayName = settingsAttribute.DisplayName;
                        actionModel.ForNavigation = settingsAttribute.ForNavigation;
                        actionModel.OrderNumber = settingsAttribute.OrderNumber;
                    }
                }

                if (actionModel.ForNavigation)
                {
                    actionModels.Add(actionModel);
                }
            }

            return actionModels;
        }
    
    
   }
}
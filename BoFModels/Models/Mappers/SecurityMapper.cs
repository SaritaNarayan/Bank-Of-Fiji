using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Domain.Entities;
using BoF.Web.Models.Security;

namespace BoF.BoFModels.Models.Mappers
{
    public class SecurityMapper
    {
        public static ControllerModel MapModule(Module module)
        {
            var controllerModel = new ControllerModel();
            controllerModel.Id = module.Id;
            controllerModel.DisplayName = module.DisplayName;
            controllerModel.ForNavigation = module.ForNavigation;
            controllerModel.ControllerName = module.ControllerName;
            controllerModel.OrderNumber = module.OrderNumber;
            controllerModel.Roles = module.ControllerRoles;

           /*
            string rolestring = "";
            if (module.ControllerRoles.Count > 0)
            {
                for (int i = 0; i < module.ControllerRoles.Count; i++)
                {
                    var role = module.ControllerRoles[i];
                    rolestring = rolestring + role;
                    if (i < module.ControllerRoles.Count - 1)
                    {
                        rolestring = rolestring + ", ";
                    }
                }
            }
            controllerModel.RoleString = rolestring;
            */
            IList<RoleModel> roleModels = new List<RoleModel>();
           /*
            if (module.ControllerRoles != null)
            {
                foreach (var controllerRole in module.ControllerRoles)
                {
                    var roleModel = new RoleModel { RoleModelName = controllerRole };
                    roleModels.Add(roleModel);
                }

                controllerModel.RolesObjects = roleModels.Cast<object>().ToArray();
            }
            */
            controllerModel.StartPage = module.StartPage;

            IList<ActionModel> actionModels = new List<ActionModel>();
            foreach (var moduleAction in module.ModuleActions)
            {
                actionModels.Add(MapModuleAction(moduleAction));
            }
            controllerModel.ActionModels = actionModels;

            return controllerModel;
        }

        public static Module ReverseMapModule(ControllerModel controllerModel)
        {
            var module = new Module();
            module.ControllerName = controllerModel.ControllerName;
            module.DisplayName = controllerModel.DisplayName;
            module.ForNavigation = controllerModel.ForNavigation;
            module.OrderNumber = controllerModel.OrderNumber;
            module.StartPage = controllerModel.StartPage;

            IList<ModuleAction> moduleActions = new List<ModuleAction>();
            if (controllerModel.ActionModels != null)
            {
                foreach (var actionModel in controllerModel.ActionModels)
                {
                    moduleActions.Add(ReverseMapModuleAction(actionModel));
                }
            }
            module.ModuleActions = moduleActions;

            return module;
        }

        public static ActionModel MapModuleAction(ModuleAction moduleAction)
        {
            var actionModel = new ActionModel();
            actionModel.Id = moduleAction.Id;
            actionModel.ControllerName = moduleAction.Module.ControllerName;
            actionModel.DisplayName = moduleAction.DisplayName;
            actionModel.ForNavigation = moduleAction.ForNavigation;
            actionModel.ActionName = moduleAction.ActionName;
            actionModel.OrderNumber = moduleAction.OrderNumber;
            actionModel.Roles = moduleAction.ActionRoles;
            string rolestring = "";
            if (moduleAction.ActionRoles.Count > 0)
            {
                for (int i = 0; i < moduleAction.ActionRoles.Count; i++)
                {
                    var role = moduleAction.ActionRoles[i];
                    rolestring = rolestring + role;
                    if (i < moduleAction.ActionRoles.Count - 1)
                    {
                        rolestring = rolestring + ", ";
                    }
                }
            }
            actionModel.RoleString = rolestring;

            IList<RoleModel> roleModels = new List<RoleModel>();
            if (moduleAction.ActionRoles != null)
            {
                foreach (var actionRole in moduleAction.ActionRoles)
                {
                    var roleModel = new RoleModel { RoleModelName = actionRole };
                    roleModels.Add(roleModel);
                }

                actionModel.RolesObjects = roleModels.Cast<object>().ToArray();
            }

            return actionModel;
        }

        public static ModuleAction ReverseMapModuleAction(ActionModel actionModel)
        {
            var moduleAction = new ModuleAction();
            moduleAction.ActionName = actionModel.ActionName;
            moduleAction.DisplayName = actionModel.DisplayName;
            moduleAction.ForNavigation = actionModel.ForNavigation;
            moduleAction.OrderNumber = actionModel.OrderNumber;
            moduleAction.ActionRoles = actionModel.Roles;
            return moduleAction;
        }

    }
}
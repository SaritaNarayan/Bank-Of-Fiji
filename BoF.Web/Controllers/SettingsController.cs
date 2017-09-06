using System.Web.Mvc;
using NHibernate;
//using BoF.Infrastructure.Repository;

namespace BoF.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISession _session;
        public SettingsController(ISession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            return View();
        }

        //[SettingsAttributes(true, "Function Role Management", 1, "51331D90-F90D-4C28-8B07-0368CA95BBD7")]
        //public ActionResult FunctionRoleManagement(int[] selectedActions, int[] selectedControllers, string[] selectedRoles)
        //{
        //    ISettingsLogic settingsLogic = new SettingsLogic(_session);

        //    #region Update Stuff

        //    selectedActions = selectedActions ?? new int[] { };
        //    ViewData["selectedActions"] = selectedActions;

        //    selectedControllers = selectedControllers ?? new int[] { };
        //    ViewData["selectedControllers"] = selectedControllers;

        //    selectedRoles = selectedRoles ?? new string[] { };
        //    ViewData["selectedRoles"] = selectedRoles;

        //    if ((selectedActions.Any() || selectedControllers.Any()) && selectedRoles.Any())
        //    {
        //        // 1. Get Actions and Controllers contained in the selected...
        //        var selectedModules = settingsLogic.GetAllModules().Where(o => selectedControllers.Contains(o.Id));
        //        var selectedModuleActions = settingsLogic.GetAllModuleActions().Where(o => selectedActions.Contains(o.Id));

        //        // 2. Loop through each (Action and Controller) and add the selectedRoles
        //        foreach (var module in selectedModules)
        //        {
        //            module.ControllerRoles = selectedRoles;
        //        }
        //        foreach (var action in selectedModuleActions)
        //        {
        //            action.ActionRoles = selectedRoles;
        //        }
        //        // 3. Save
        //        settingsLogic.UpdateModules(selectedModules);
        //        settingsLogic.UpdateModuleActions(selectedModuleActions);
        //    }

        //    #endregion

        //    #region Display Stuff

        //    var roleService = new RoleService();
        //    ViewData["AllRoles"] = roleService.GetAllRoles();

        //    var listOfModules = settingsLogic.GetAllModules();
        //    var listOfModuleActions = settingsLogic.GetAllModuleActions();

        //    var listOfControllers = new List<ControllerModel>();

        //    if (listOfModules.Count > 0)
        //    {
        //        foreach (var module in listOfModules)
        //        {
        //            var controllersModel = Models.Mappers.SecurityMapper.MapModule(module);
        //            listOfControllers.Add(controllersModel);
        //        }
        //    }

        //    ViewData["ListOfControllers"] = listOfControllers;
        //    IList<ActionModel> actionModels = new List<ActionModel>();

        //    if (listOfModuleActions.Count > 0)
        //    {
        //        foreach (var module in listOfModuleActions)
        //        {
        //            var actionModel = Models.Mappers.SecurityMapper.MapModuleAction(module);
        //            actionModels.Add(actionModel);
        //        }
        //    }
        //    //sort actionmodels list here and send to another list.
        //    var orderedList = actionModels.OrderBy(x => x.ControllerName).ThenBy(x => x.DisplayName).ToList();
        //    ViewData["ListOfActions"] = orderedList;

        //    #endregion

        //    return View();
        //}

        ////  [SettingsAttributes(true, "Manage Modules, Actions and Roles", 1, "51331D90-F90D-4C28-8B07-0368CA95BBD7")]
        //[HttpPost]
        //public ActionResult FunctionRoleManagement(int[] selectedActions, int[] selectedControllers, string[] selectedRoles, FormCollection formCollection)
        //{
        //    ISettingsLogic settingsLogic = new SettingsLogic(_session);

        //    #region Update Stuff

        //    List<string> actionsList = null;
        //    List<string> rolesList = null;
        //    foreach (var key in formCollection.AllKeys)
        //    {
        //        var value = formCollection[key];
        //        if (key == "actionChkBx")
        //        {
        //            actionsList = new List<string>(value.Split(','));
        //        }
        //        else if (key == "roleChkBx")
        //        {
        //            rolesList = new List<string>(value.Split(','));
        //        }
        //    }

        //    var actionintList = new List<int>();

        //    if (actionsList != null)
        //    {
        //        foreach (var action in actionsList)
        //        {
        //            if (action != "false")
        //            {
        //                actionintList.Add(Convert.ToInt32(action));
        //            }
        //        }
        //    }

        //    selectedActions = new int[actionintList.Count];
        //    for (int i = 0; i < actionintList.Count; i++)
        //    {
        //        selectedActions[i] = actionintList[i];
        //    }

        //    //selectedActions = selectedActions ?? new int[] { };
        //    ViewData["selectedActions"] = selectedActions;

        //    selectedControllers = selectedControllers ?? new int[] { };
        //    ViewData["selectedControllers"] = selectedControllers;

        //    var rolesstringList = new List<string>();
        //    foreach (var role in rolesList)
        //    {
        //        if (role != "false")
        //        {
        //            rolesstringList.Add(role);
        //        }
        //    }

        //    selectedRoles = new string[rolesstringList.Count];
        //    for (int i = 0; i < rolesstringList.Count; i++)
        //    {
        //        selectedRoles[i] = rolesstringList[i];
        //    }

        //    //selectedRoles = selectedRoles ?? new string[] { };
        //    ViewData["selectedRoles"] = selectedRoles;

        //    if ((selectedActions.Any() || selectedControllers.Any()) && selectedRoles.Any())
        //    {
        //        // 1. Get Actions and Controllers contained in the selected...
        //        var selectedModules = settingsLogic.GetAllModules().Where(o => selectedControllers.Contains(o.Id));
        //        var selectedModuleActions = settingsLogic.GetAllModuleActions().Where(o => selectedActions.Contains(o.Id));

        //        // 2. Loop through each (Action and Controller) and add the selectedRoles
        //        foreach (var module in selectedModules)
        //        {
        //            module.ControllerRoles = selectedRoles;
        //        }
        //        foreach (var action in selectedModuleActions)
        //        {
        //            action.ActionRoles = selectedRoles;
        //        }

        //        // 3. Save
        //        settingsLogic.UpdateModules(selectedModules);
        //        settingsLogic.UpdateModuleActions(selectedModuleActions);
        //    }

        //    #endregion

        //    #region Display Stuff

        //    var roleService = new RoleService();
        //    ViewData["AllRoles"] = roleService.GetAllRoles();

        //    var listOfModules = settingsLogic.GetAllModules();
        //    var listOfModuleActions = settingsLogic.GetAllModuleActions();
        //    var listOfControllers = new List<ControllerModel>();

        //    if (listOfModules.Count > 0)
        //    {
        //        foreach (var module in listOfModules)
        //        {
        //            var controllersModel = Models.Mappers.SecurityMapper.MapModule(module);
        //            listOfControllers.Add(controllersModel);
        //        }
        //    }
        //    ViewData["ListOfControllers"] = listOfControllers;

        //    IList<ActionModel> actionModels = new List<ActionModel>();
        //    if (listOfModuleActions.Count > 0)
        //    {
        //        foreach (var module in listOfModuleActions)
        //        {
        //            var actionModel = Models.Mappers.SecurityMapper.MapModuleAction(module);

        //            actionModels.Add(actionModel);
        //        }
        //    }

        //    var orderedList = actionModels.OrderBy(x => x.ControllerName).ThenBy(x => x.DisplayName).ToList();
        //    ViewData["ListOfActions"] = orderedList;

        //    #endregion

        //    return View();
        //}

        //[SettingsAttributes(true, "Add Modules and Actions", 4, "0D7C30EC-06AF-47CB-8240-86325091251F")]
        //public ActionResult AddModulesAndActions()
        //{
        //    ISettingsLogic settingsLogic = new SettingsLogic(_session);

        //    //Get a list of all modules in the database
        //    var listOfModules = settingsLogic.GetAllModules();

        //    //Get a list of actions in the database
        //    var listOfActions = settingsLogic.GetAllModuleActions();

        //    //Get a list of all controllers in code
        //    var controllerModels = SettingsHelper.GetControllerSettings();

        //    //Get a list of all actions in code
        //    var actionModels = SettingsHelper.GetActionsForAllControllers(SettingsHelper.GetSubClasses<Controller>());
        //    var modules = new List<Module>();

        //    // For each of the controllers in code
        //    foreach (var controllerModel in controllerModels)
        //    {
        //        bool goAheadModule = true;

        //        // For each of modules in the database
        //        foreach (var module in listOfModules)
        //        {
        //            if (module.ControllerName == controllerModel.ControllerName)
        //            {
        //                // If it is already in then don't go ahead
        //                goAheadModule = false;
        //            }
        //        }

        //        // If this controller is not in the system then go ahead
        //        if (goAheadModule)
        //        {
        //            if (controllerModel.ActionModels == null)
        //            {
        //                controllerModel.ActionModels = new List<ActionModel>();
        //            }

        //            //For each of the actions in code
        //            foreach (var actionModel in actionModels)
        //            {
        //                bool goAheadAction = true;

        //                if (listOfActions != null)
        //                {
        //                    //For each of the actions in the database
        //                    foreach (var moduleAction in listOfActions)
        //                    {
        //                        if (moduleAction.ActionName == actionModel.ActionName && moduleAction.Module.ControllerName == actionModel.ControllerName)
        //                        {
        //                            goAheadAction = false;
        //                        }
        //                    }
        //                }

        //                if (goAheadAction)
        //                {
        //                    if (actionModel.ControllerName == controllerModel.ControllerName)
        //                    {
        //                        controllerModel.ActionModels.Add(actionModel);
        //                    }
        //                }
        //            }
        //            modules.Add(Models.Mappers.SecurityMapper.ReverseMapModule(controllerModel));
        //        }
        //    }
        //    foreach (var module in modules)
        //    {
        //        foreach (var moduleAction in module.ModuleActions)
        //        {
        //            moduleAction.Module = module;
        //        }
        //    }
        //    settingsLogic.AddModules(modules);
        //    Response.Redirect("/Security");
        //    return View(controllerModels);
        //}
    }
}

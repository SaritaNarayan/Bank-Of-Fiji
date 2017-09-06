using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NHibernate;
using BoF.Application;
using BoF.Domain.Entities;
using BoF.Web.Helpers;
using BoF.Web.Models.Security;
using BoF.Web.Filters;
using System.Configuration;

namespace BoF.Web.Controllers
{

    [SettingsAttributes(true, "CommonController", 2, "E1576F64-3D15-4F53-B78B-E3B96AC68C9C")]
    [UserActionFilter]
    public class CommonController : Controller
    {
        private readonly ISession session;

        public CommonController(ISession session)
        {
            this.session = session;
        }

        //[SettingsAttributes(true, "MainMenu", 9, "7DB083B0-DBD2-4EB7-A9DE-7402DB99D981")]
        //[ChildActionOnly]
        //public ActionResult MainMenu()
        //{
        //    ISettingsLogic settingsLogic = new SettingsLogic(session);
        //    var listOfModules = settingsLogic.GetAllModules();

        //    if (listOfModules.Count > 0)
        //    {
        //        var listOfControllers = new List<ControllerModel>();

        //        foreach (var module in listOfModules)
        //        {
        //            var controllersModel = new ControllerModel
        //            {
        //                DisplayName = module.DisplayName,
        //                ForNavigation = module.ForNavigation,
        //                ControllerName = module.ControllerName,
        //                OrderNumber = module.OrderNumber,
        //                StartPage = module.StartPage
        //            };

        //            if (SecurityHelper.ActionIsAllowedForUser("BoF.Web.Controllers." + module.ControllerName + "Controller", "Index"))
        //            {   
        //                if (controllersModel.ControllerName != "Supervisor" && controllersModel.ControllerName != "Auditor" && controllersModel.ControllerName != "AuditManager")
        //                        listOfControllers.Add(controllersModel);
                        
        //                else
        //                {
                          
        //                    listOfControllers.Add(controllersModel);
        //                }
        //            }
        //        }

        //        return PartialView(listOfControllers);
        //    }
        //    else
        //    {
        //        var listOfControllers = SettingsHelper.GetControllerSettings();
        //        return PartialView(listOfControllers);
        //    }
        //}

        //[ChildActionOnly]
        //public ActionResult SubMenu()
        //{
        //    var lts = SettingsHelper.GetSubClasses<Controller>();
        //    var listOfMethods = SettingsHelper.GetActionsForAllControllers(lts); //settings.GetActionMethods(this.ControllerContext.Controller.GetType(), typeof(ActionResult));)
        //    return PartialView(listOfMethods);
        //}

        //TODO:




        //public ActionResult AddModules()
        //{
        //    var listOfControllers = SettingsHelper.GetControllerSettings();

        //    IList<Module> modules = new List<Module>();

        //    foreach (var controllersModel in listOfControllers)
        //    {
        //        var module = new Module()
        //        {
        //            ControllerName = controllersModel.ControllerName,
        //            DisplayName = controllersModel.DisplayName,
        //            ForNavigation = controllersModel.ForNavigation,
        //            OrderNumber = controllersModel.OrderNumber,
        //            StartPage = controllersModel.StartPage,
        //        };
        //        modules.Add(module);
        //    }

        //    var settingsLogic = new SettingsLogic();
        //    settingsLogic.AddModules(modules);

        //    Response.Redirect("/");
        //    return View();
        //}

        //public ActionResult AddModuleActions()
        //{
        //    var subClasses = SettingsHelper.GetSubClasses<Controller>();
        //    var listOfMethods = SettingsHelper.GetActionsForAllControllers(subClasses);

        //    return View();
        //}

        //[SettingsAttributes(true, "Add Modules and Actions", 4, "0D7C30EC-06AF-47CB-8240-86325091251F")]
        //public ActionResult AddModulesAndActions()
        //{
        //    ISettingsLogic settingsLogic = new SettingsLogic(session);

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

        //    Response.Redirect("/Admin");
        //    return View(controllerModels);
        //}

    }
}

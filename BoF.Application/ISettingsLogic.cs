using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Domain.Entities;

namespace BoF.Application
{
    public interface ISettingsLogic
    {
        IList<Module> GetAllModules();
        bool AddModules(IList<Module> controllers);
        bool AddModuleActions(IList<ModuleAction> actionResults);
        bool UpdateModules(IEnumerable<Module> modules);
        IList<ModuleAction> GetAllModuleActions();
        bool UpdateModuleActions(IEnumerable<ModuleAction> moduleActions);
        



    }
}
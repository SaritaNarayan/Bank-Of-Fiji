using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Domain.Entities;
using BoF.Domain;
using BoF.Domain.Repository;
using NHibernate;



namespace BoF.Application
{
    public class SettingsLogic : ISettingsLogic
    {

        #region Members

        private readonly IRepository<Module> _moduleRepository;
        private readonly IRepository<ModuleAction> _moduleActionRepository;
       

        #endregion

        #region Constructors

        public SettingsLogic(ISession session)
        {
            if (session.IsOpen == false)
                session = FluentNHibernateSessionManager.OpenSession();

            _moduleRepository = new Repository<Module>(session);
            _moduleActionRepository = new Repository<ModuleAction>(session);
          

        }

        public SettingsLogic()
        {
            _moduleRepository = new Repository<Module>();
            _moduleActionRepository = new Repository<ModuleAction>();
            
        }

        public SettingsLogic(IRepository<Module> moduleRepository)
        {
            if (moduleRepository == null)
                throw new ArgumentNullException("moduleRepository");

            _moduleRepository = moduleRepository;
        }

        public SettingsLogic(IRepository<ModuleAction> moduleActionRepository)
        {
            if (moduleActionRepository == null)
                throw new ArgumentNullException("moduleActionRepository");

            _moduleActionRepository = moduleActionRepository;
        }

        

        #endregion

        #region Methods

        public IList<Module> GetAllModules()
        {
            var m = _moduleRepository.GetList();

            var modules = new List<Module>();

            m = m.OrderBy(x => x.OrderNumber);

            if (m != null)
            {
                foreach (var module in m)
                {
                    modules.Add(module);
                }
            }

            return modules;
        }

        public bool AddModules(IList<Module> controllers)
        {
            try
            {
                _moduleRepository.BeginTransaction();
                foreach (var controller in controllers)
                {
                    foreach (var moduleAction in controller.ModuleActions)
                    {
                        _moduleActionRepository.Save(moduleAction);
                    }
                    if (controller.Id == 0)
                    {
                        controller.CreateTimestamp();
                    }
                    else
                    {
                        controller.UpdateTimestamp();
                    }
                    _moduleRepository.SaveOrUpdate(controller);
                }

                _moduleRepository.CommitTransaction();


                return true;
            }
            catch (Exception)
            {
                _moduleRepository.RollbackTransaction();
                throw;
            }
        }

        public bool AddModuleActions(IList<ModuleAction> actionResults)
        {
            try
            {
                _moduleActionRepository.BeginTransaction();
                foreach (var actionResult in actionResults)
                {
                    if (actionResult.Id == 0)
                    {
                        actionResult.CreateTimestamp();
                    }
                    else
                    {
                        actionResult.UpdateTimestamp();
                    }
                    _moduleActionRepository.Save(actionResult);
                }

                _moduleActionRepository.CommitTransaction();

                return true;
            }
            catch (Exception)
            {
                _moduleActionRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateModules(IEnumerable<Module> modules)
        {
            try
            {
                _moduleRepository.BeginTransaction();
                foreach (var module in modules)
                {
                    if (module.Id == 0)
                    {
                        module.CreateTimestamp();
                    }
                    else
                    {
                        module.UpdateTimestamp();
                    }

                    _moduleRepository.SaveOrUpdate(module);
                }

                _moduleRepository.CommitTransaction();

                return true;
            }
            catch (Exception)
            {
                _moduleRepository.RollbackTransaction();
                throw;
            }
        }

        public IList<ModuleAction> GetAllModuleActions()
        {
            var m = _moduleActionRepository.GetList();

            var moduleActions = new List<ModuleAction>();

            if (m != null)
            {
                foreach (var module in m)
                {
                    moduleActions.Add(module);
                }
            }

            return moduleActions;
        }

        public bool UpdateModuleActions(IEnumerable<ModuleAction> moduleActions)
        {
            try
            {
                _moduleActionRepository.BeginTransaction();
                foreach (var moduleAction in moduleActions)
                {
                    if (moduleAction.Id == 0)
                    {
                        moduleAction.CreateTimestamp();
                    }
                    else
                    {
                        moduleAction.UpdateTimestamp();
                    }
                    _moduleActionRepository.SaveOrUpdate(moduleAction);
                }

                _moduleActionRepository.CommitTransaction();

                return true;
            }
            catch (Exception)
            {
                _moduleActionRepository.RollbackTransaction();
                throw;
            }
        }




        #endregion
      
    }
}
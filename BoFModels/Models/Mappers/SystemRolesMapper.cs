using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Application;
using BoF.Domain.Entities;

namespace BoF.BoFModels.Models.Mappers
{
    public class SystemRolesMapper
    {
        private readonly ISession _session;

        public SystemRolesMapper(ISession session)
        {
            this._session = session;
        }

        public SystemRolesModel SystemRolesToSystemRolesModel(SystemRoles SystemRoles)
        {
            IApplicationLogic applicationLogic = new ApplicationLogic(_session);
            var SystemRolesModel = new SystemRolesModel();

            SystemRolesModel.ID = SystemRoles.Id;
            SystemRolesModel.Role = SystemRoles.Role;
            SystemRolesModel.Status = SystemRoles.Status;

            return SystemRolesModel;
        }

        public SystemRoles SystemRolesModelToSystemRoles(SystemRolesModel SystemRolesModel)
        {
          
            var SystemRoles = new SystemRoles();

            SystemRoles.Id = SystemRolesModel.ID;
            SystemRoles.Role = SystemRolesModel.Role;
            SystemRoles.Status = SystemRolesModel.Status;
            return SystemRoles;
        }
    }
}
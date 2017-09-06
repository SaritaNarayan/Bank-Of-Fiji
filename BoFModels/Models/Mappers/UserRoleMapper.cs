using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoF.Application;
using BoF.Domain.Entities;

namespace BoF.BoFModels.Models.Mappers
{
    public class UserRoleMapper
    {
        private readonly ISession _session;

        public UserRoleMapper(ISession session)
        {
            this._session = session;
        }

        public UserRoleModel UserRoleToUSerRoleModel(UserRole UserRole)
        {
            IApplicationLogic applicationLogic = new ApplicationLogic(_session);
            var UserRoleModel = new UserRoleModel();

            UserRoleModel.ID = UserRole.Id;
            UserRoleModel.Token = UserRole.Token;
            UserRoleModel.UserRoleStatus = UserRole.UserRoleStatus;

            return UserRoleModel;
        }

        public UserRole UserRoleModelToUSerRole(UserRoleModel UserRoleModel)
        {
            var UserRole = new UserRole();

            UserRole.Id = UserRoleModel.ID;
            UserRole.Token = UserRoleModel.Token;
            UserRole.UserRoleStatus = UserRoleModel.UserRoleStatus;

            return UserRole;
        }
    }
}
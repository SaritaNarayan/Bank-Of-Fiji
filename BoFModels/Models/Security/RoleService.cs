using BoF.BoFModels.Models;
using System;
using System.Collections.Generic;
using System.Web.Security;

namespace BoF.Web.Models.Security
{
    public class RoleService : IRoleService
    {
        private readonly RoleProvider roleProvider;

        public RoleService(RoleProvider roleProvider)
        {
            this.roleProvider = roleProvider ?? SystemRolesModel.Provider;
        }

        public RoleService()
        {
            this.roleProvider = SystemRolesModel.Provider;
        }

        public IList<RoleModel> GetAllRoles()
        {
            IList<RoleModel> roleModels = new List<RoleModel>();
            var roles = roleProvider.GetAllRoles();

            foreach (var role in roles)
            {
                var roleModel = new RoleModel { RoleModelName = role };
                roleModels.Add(roleModel);
            }

            return roleModels;
        }

        public bool CreateRole(string roleName)
        {
            if (!roleProvider.RoleExists(roleName))
                roleProvider.CreateRole(roleName);

            return roleProvider.RoleExists(roleName);
        }

        public void AddUsersToRole(string[] users, string[] roles)
        {
            roleProvider.AddUsersToRoles(users, roles);
        }

        public void RemoveUsersToRole(string[] users, string[] roles)
        {
            roleProvider.RemoveUsersFromRoles(users, roles);
        }

        public IList<RegisterModel> GetUserRole(string username)
        {
            var roles = roleProvider.GetRolesForUser(username);

            IList<RegisterModel> registerModels = new List<RegisterModel>();
            foreach (var r in roles)
            {
                var registerModel = new RegisterModel { UserName = username, RoleModelName = r };
                registerModels.Add(registerModel);
            }

            return registerModels;
        }

        public void DeleteRole(string roleName)
        {
            roleProvider.DeleteRole(roleName, true);
        }
    }
}
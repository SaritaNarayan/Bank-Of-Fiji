using System.Collections.Generic;

namespace BoF.Web.Models.Security
{
    public interface IRoleService
    {
        IList<RoleModel> GetAllRoles();
        bool CreateRole(string roleName);
        void AddUsersToRole(string[] users, string[] roles);
        void RemoveUsersToRole(string[] users, string[] roles);
        IList<RegisterModel> GetUserRole(string username);
        void DeleteRole(string roleName);
    }
}
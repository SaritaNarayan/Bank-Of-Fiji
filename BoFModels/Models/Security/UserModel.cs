using System.Collections.Generic;

namespace BoF.Web.Models.Security
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserLoginName { get; set; }
        public string UserFullName { get; set; }
        public string UserType { get; set; }
        public IList<RoleModel> RoleModels { get; set; }
        public IList<string> Roles { get; set; }
    }
}
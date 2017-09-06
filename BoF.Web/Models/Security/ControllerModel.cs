using System.Collections.Generic;

namespace BoF.Web.Models.Security
{
    public class ControllerModel
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string DisplayName { get; set; }
        public string StartPage { get; set; }
        public bool ForNavigation { get; set; }
        public int OrderNumber { get; set; }
        public string ActionName { get; set; }
        public IList<ActionModel> ActionModels { get; set; }
        public IList<string> Roles { get; set; }
        public IEnumerable<object> RolesObjects { get; set; }
        public string RoleString { get; set; }
    }
}
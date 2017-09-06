using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class Module : Entity
    {
        public virtual string ControllerName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string StartPage { get; set; }
        public virtual bool ForNavigation { get; set; }
        public virtual int OrderNumber { get; set; }
        public virtual IList<string> ControllerRoles { get; set; }
        public virtual IList<ModuleAction> ModuleActions { get; set; }
    }
}
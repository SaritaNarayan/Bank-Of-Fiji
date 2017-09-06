using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class ModuleAction:Entity
    {
        public virtual string ActionName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual bool ForNavigation { get; set; }
        public virtual int OrderNumber { get; set; }
        public virtual IList<string> ActionRoles { get; set; }
        public virtual Module Module { get; set; }
    
    }
}
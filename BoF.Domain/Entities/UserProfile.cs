using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class UserProfile:Entity
    {
        public virtual String Username { get; set; }
        public virtual String Role { get; set; }
        public virtual bool Status { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
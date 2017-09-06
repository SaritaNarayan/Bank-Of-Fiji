using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class UserRole : Entity
    {
        
        public virtual String Token { get; set; }
        public virtual String UserRoleStatus { get; set; }
        

        //public virtual ActiveDirectory ActiveDirectory { get; set; }        
        public virtual SystemRoles Roles { get; set; }
      
    }
}
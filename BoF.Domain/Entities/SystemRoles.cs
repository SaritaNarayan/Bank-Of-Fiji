using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class SystemRoles: Entity
    {
        
        public virtual String Role { get; set; }
        public virtual Boolean Status { get; set; }
        //public virtual int MyProperty { get; set; }




    }
}
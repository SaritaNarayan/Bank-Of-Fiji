using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BoF.Web.Models
{
    public class SystemRolesModel
    {
        public static RoleProvider Provider { get; internal set; }
        public int ID { get; set; }
        public   String Role { get; set; }
        public   Boolean Status { get; set; }
        public SelectList GetRoles { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Web.Models
{
    public class UserRoleModel
    {
        public int ID { get; set; }
        public   String Token { get; set; }
        public   String UserRoleStatus { get; set; }
        //public   ActiveDirectoryModel ActiveDirectory { get; set; }
        public   SystemRolesModel Roles { get; set; }
    }
}
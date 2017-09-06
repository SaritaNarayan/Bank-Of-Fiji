using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoF.BoFModels.Models
{
    public class UserProfileModel
    {
        public int ID { get; set; }
        public String Username { get; set; }
        public String Role { get; set; }
        public bool Status { get; set; }
        public virtual CustomerModel CustomerModel { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BoF.Web.Models
{
    public class UserLoginDetailsModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Login { get; set; }
        public DateTime LogOff { get; set; }
        public string MachineIPAddress { get; set; }
        public int PasswordChanged { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

    }
}
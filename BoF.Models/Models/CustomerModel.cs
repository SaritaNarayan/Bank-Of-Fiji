﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Web.Models
{
    public class CustomerModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String TinNumber { get; set; }
        public String Gender { get; set; }
        public String Email { get; set; }
        public String HomeAddress { get; set; }
        public String City { get; set; }
        public DateTime MobileNumber { get; set; }
        public String Occupation { get; set; }
        public String EmployerName { get; set; }
    }
}
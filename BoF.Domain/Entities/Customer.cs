using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class Customer:Entity
    {
        public virtual int ID { get; set; }
        public virtual String FirstName { get; set; }
        public virtual String LastName { get; set; }
        public virtual String Username { get; set; }
        public virtual String Password { get; set; }
        public virtual String DateOfBirth { get; set; }
        public virtual String TinNumber { get; set; }
        public virtual String Gender { get; set; }
        public virtual String Email { get; set; }
        public virtual String HomeAddress { get; set; }
        public virtual String City { get; set; }
        public virtual String MobileNumber { get; set; }
        public virtual String Occupation { get; set; }
        public virtual String EmployerName { get; set; }
    }
}
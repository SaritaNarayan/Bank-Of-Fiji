using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class Account:Entity
    {
        //public virtual DateTime StartDate { get; set; }
        public virtual Decimal Balance { get; set; }
        public virtual bool Status { get; set; }
        public virtual int AccountNumber { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual AccountType AccountType { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class AccountType:Entity
    {
        public virtual int Id { get; set; }
        public virtual String AccountName { get; set; }
        public virtual Decimal InterestRate { get; set; }
    }
}
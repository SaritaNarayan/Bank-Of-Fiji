using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class ScheduleTrans
    {
        public virtual int FromAccount { get; set; }
        public virtual String Biller { get; set; }
        public virtual String BillerAccNo { get; set; }
        public virtual String Desc { get; set; }
        public virtual Decimal Amount { get; set; }
        public virtual DateTime NextRunDate { get; set; }
        public virtual String Frequency { get; set; }
        //public virtual AccountType AccountType { get; set; }
    }
}
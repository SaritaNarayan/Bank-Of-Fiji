using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Domain.Entities
{
    public class Transaction:Entity
    {
        public virtual Decimal TransAmount { get; set; }
        public virtual String TransDetails { get; set; }
        public virtual DateTime TransDateTime { get; set; }
        public virtual Decimal Debit { get; set; }
        public virtual Decimal Credit { get; set; }
        public virtual Decimal RunningCapital { get; set; }
        public virtual Account Account { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}
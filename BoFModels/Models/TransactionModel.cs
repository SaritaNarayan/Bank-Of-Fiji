using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.BoFModels.Models
{
    public class TransactionModel
    {
        public Decimal TransAmount { get; set; }
        public String TransDetails { get; set; }
        public DateTime TransDateTime { get; set; }
        public Decimal Debit { get; set; }
        public Decimal Credit { get; set; }
        public Decimal RunningCapital { get; set; }
        public virtual AccountModel AccountModel { get; set; }
        public virtual TransactionTypeModel TransactionTypeModel { get; set; }
    }
}
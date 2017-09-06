using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoFModels.Models
{
    public class StatementModel
    {
        public string TransAmont { get; set; }
        public string TransDetails { get; set; }
        public String TransactionDate { get; set; }
        public String Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal RunningCapital { get; set; }
    }
}
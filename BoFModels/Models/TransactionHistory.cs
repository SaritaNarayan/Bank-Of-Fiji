using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoFModels.Models
{
    public class TransactionHistory
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public String TransactionDate { get; set; }
        public String Details { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}
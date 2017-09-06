using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoFModels.Models
{
    public class CustomerAcc
    {
        public string CustomerName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
    }
}
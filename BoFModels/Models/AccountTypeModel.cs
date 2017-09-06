using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.BoFModels.Models
{
    public class AccountTypeModel
    {
        public int Id { get; set; }
        public String AccountName { get; set; }
        public Decimal InterestRate { get; set; }
    }
}
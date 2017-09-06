using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoFModels.Models
{
    public class ScheduleTransModel
    {
        public int FromAccount { get; set; }
        public String Biller { get; set; }
        public String BillerAccNo { get; set; }
        public String Desc { get; set; }
        public Decimal Amount { get; set; }
        public DateTime NextRunDate { get; set; }
        public String Frequency { get; set; }
    }
}
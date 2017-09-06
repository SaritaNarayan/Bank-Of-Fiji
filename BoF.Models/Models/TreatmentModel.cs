using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.Web.Models
{
    public class TreatmentModel
    {
        public  DateTime TreatmentStartDate { get; set; }
        public  DateTime TreatmentEndDate { get; set; }
        public  string Regime { get; set; }
        public  string OtherProphylaxis { get; set; }

    }


}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoF.BoFModels.Models
{
    public class AccountModel
    {
        //public DateTime StartDate { get; set; }
        public Decimal Balance { get; set; }
        public bool Status { get; set; }
        public int AccountNumber { get; set; }
        public SelectList GetAccounts { get; set; }
        public virtual CustomerModel CustomerModel { get; set; }
        public virtual AccountTypeModel AccountTypeModel { get; set; }
    }
}
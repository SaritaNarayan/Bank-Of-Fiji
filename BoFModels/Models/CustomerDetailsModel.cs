using BoFModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoF.BoFModels.Models
{
    public class CustomerDetailsModel
    {
        public CustomerModel CustomerModel { get; set; }
        public AccountModel AccountModel { get; set; }
        public AccountTypeModel AccountTypeModel { get; set; }
        public TransactionModel TransactionModel { get; set; }
        public TransactionTypeModel TransactionTypeModel { get; set; }
        public UserProfileModel UserProfileModel { get; set; }
        public ScheduleTransModel ScheduleTransModel { get; set; }
    }
}
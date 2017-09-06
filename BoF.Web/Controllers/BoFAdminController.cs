using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoF.BoFModels.Models;
using NHibernate;
using BoF.Application;
using BoF.Domain.Entities;
using BoF.BoFModels.Models.Mappers;
using System.Security.Cryptography;
using System.Text;

namespace BoF.Web.Controllers
{
    public class BoFAdminController : Controller
    {
        private readonly ISession session;
        public IApplicationLogic applicationlogic { get; set; }
        private readonly CustomerMapper CustomerMapper;
        private readonly AccountMapper AccountMapper;
        private readonly AccountTypeMapper AccountTypeMapper;
        private readonly UserProfileMapper UserProfileMapper;
        // GET: BoFAdmin

        public BoFAdminController(ISession session)
        {
            this.session = session;

            if (applicationlogic == null) { applicationlogic = new ApplicationLogic(session); }
            CustomerMapper = new CustomerMapper(session);
            AccountMapper = new AccountMapper(session);
            AccountTypeMapper = new AccountTypeMapper(session);
            UserProfileMapper = new UserProfileMapper(session);
        }
        public ActionResult BoFAdmin()
        {
            var Gender = new SelectList(new[]
          {
                new { ID = "1", Name = "-- Please Select --"},
                new { ID = "Male ", Name = "Male"},
                new { ID = "Female", Name = "Female"},
                new { ID = "Rather not say", Name = "Rather Not Say"},

            }, "ID", "Name", 1);

            var AccountType = new SelectList(new[]
        {
                new { ID = "1", Name = "-- Please Select --"},
                new { ID = "Simple ", Name = "Simple"},
                new { ID = "Savings", Name = "Savings"},

            }, "ID", "Name", 1);

            ViewData["Gender"] = Gender;
            ViewData["AccountType"] = AccountType;
            return View();
        }

        public ActionResult CreateUser(CustomerDetailsModel CustomerDetailsModel)
        {
            IApplicationLogic applicationlogic = new ApplicationLogic(session);
            var customerentity = new Customer();
            var customermodel = CustomerMapper.CustomerModelToCustomer(CustomerDetailsModel.CustomerModel);

            var password = customermodel.Password;
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] oBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] eBytes = md5.ComputeHash(oBytes);
            customermodel.Password = BitConverter.ToString(eBytes);
             
            customerentity = applicationlogic.CreateUser(customermodel);
           
            var accountentity = new Account();
            var accountmodel = AccountMapper.AccountModelToAccount(CustomerDetailsModel.AccountModel);
            accountentity = applicationlogic.AddAccount(accountmodel);

            //var userroleentity = new UserProfile();
            //CustomerDetailsModel.UserProfileModel.Role = "Customer";
            //var userrolemodel = UserProfileMapper.UserProfileModelToUserProfile(CustomerDetailsModel.UserProfileModel);
            //userroleentity = applicationlogic.AddUserRole(userrolemodel);

            return RedirectToAction("BoFAdmin", "BoFAdmin");
        }

        public ActionResult CheckPassword(CustomerDetailsModel CustomerDetailsModel)
        {
            IApplicationLogic applicationlogic = new ApplicationLogic(session);
            var customerentity = new Customer();
            var customermodel = CustomerMapper.CustomerModelToCustomer(CustomerDetailsModel.CustomerModel);

            var password = customermodel.Password;
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] oBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] eBytes = md5.ComputeHash(oBytes);
            customermodel.Password = BitConverter.ToString(eBytes);

            customerentity = applicationlogic.CreateUser(customermodel);
            //var accountentity = new Account();

            //var accountmodel = AccountMapper.AccountModelToAccount(CustomerDetailsModel.AccountModel);
            //accountentity = applicationlogic.AddAccount(accountmodel);


            return RedirectToAction("BoFAdmin", "BoFAdmin");
        }

        public ActionResult SideBar()
        {
            return PartialView("_SideBar");
        }
        public ActionResult ShowMyPartialView()
        {
            return PartialView("_LayoutBoF");
        }

        public ActionResult BoFUserList()
        {
            return View();
        }

        public ActionResult GetCustomerDetails()
        {
            IList<CustomerModel> CustomerDetails = new List<CustomerModel>();

            var customerDetails = applicationlogic.GetCustomerDetails();

            foreach (var _customerdetails in customerDetails)
            {
                var customerModel = CustomerMapper.CustomerToCustomerModel(_customerdetails);
                CustomerDetails.Add(customerModel);
            }

            List<string> CusDetails = new List<string>();
            foreach (var item in CustomerDetails)
            {
                var aa = item.Id.ToString();
                CusDetails.Add(aa);
                var a = item.FirstName;
                CusDetails.Add(a);
                var g = item.TinNumber;
                CusDetails.Add(g);
                var c = item.Email;
                CusDetails.Add(c);
                var d = item.MobileNumber;
                CusDetails.Add(d);
                var e = item.HomeAddress;
                CusDetails.Add(e);
                var f = item.City;
                CusDetails.Add(f);
                
            }

            return Json(new
            {
                DivId = CusDetails
            }
              , JsonRequestBehavior.AllowGet);
        }

        public ActionResult Interest_Charges()
        {
            return View();
        }

        public ActionResult AddSimpleInterest(CustomerDetailsModel CustomerDetailsModel)
        {
            IApplicationLogic applicationlogic = new ApplicationLogic(session);
            var accounttypeentity = new AccountType();

            CustomerDetailsModel.AccountTypeModel.AccountName = "Simple";
            CustomerDetailsModel.AccountTypeModel.Id = 1;
            var accounttypemodel = AccountTypeMapper.AccountTypeModelToAccountType(CustomerDetailsModel.AccountTypeModel);
            accounttypeentity = applicationlogic.AddSimpleInterest(accounttypemodel);

            //var simpleInterest = accounttypeentity.InterestRate;

            //return Json(new
            //{
            //    DivID = simpleInterest
            //}
            //    , JsonRequestBehavior.AllowGet);
            return RedirectToAction("Interest_Charges", "BoFAdmin");
            //return View();
        }
        public ActionResult AddSavingsInterest(CustomerDetailsModel CustomerDetailsModel)
        {
            IApplicationLogic applicationlogic = new ApplicationLogic(session);
            var accounttypeentity = new AccountType();

            CustomerDetailsModel.AccountTypeModel.AccountName = "Savings";
            CustomerDetailsModel.AccountTypeModel.Id = 2;
            var accounttypemodel = AccountTypeMapper.AccountTypeModelToAccountType(CustomerDetailsModel.AccountTypeModel);
            accounttypeentity = applicationlogic.AddSavingsInterest(accounttypemodel);

            //var simpleInterest = accounttypeentity.InterestRate;

            //return Json(new
            //{
            //    DivID = simpleInterest
            //}
            //    , JsonRequestBehavior.AllowGet);
            return RedirectToAction("Interest_Charges", "BoFAdmin");
            //return View();
        }

        public ActionResult GetSimpleInterest()
        {
            IList<AccountTypeModel> SimpleInterest = new List<AccountTypeModel>();
            var simpleInterest = applicationlogic.GetSimpleInterest();

            return Json(new
            {
                DivId = simpleInterest
            }
             , JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSavingsInterest()
        {
            IList<AccountTypeModel> SimpleInterest = new List<AccountTypeModel>();
            var savingsInterest = applicationlogic.GetSavingsInterest();

            return Json(new
            {
                DivId = savingsInterest
            }
             , JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerDetail(int id)
        {
            IList<CustomerModel> CustomerDetails = new List<CustomerModel>();
            IList<AccountModel> AccountDetails = new List<AccountModel>();
            IList<AccountTypeModel> AccountTypeDetails = new List<AccountTypeModel>();

            var customerDetails = applicationlogic.GetCustomerDetail(id);
            var accountDetails = applicationlogic.GetAccountDetails(id);

            var accountId = applicationlogic.GetAccountId(id);
            var accountTypeId = applicationlogic.GetTypeAccountId(accountId);

            var accountTypeDesc = applicationlogic.GetAccountTypeDetails(accountTypeId);
            //Add
            foreach (var _customerdetails in customerDetails)
            {
                var customerModel = CustomerMapper.CustomerToCustomerModel(_customerdetails);
                CustomerDetails.Add(customerModel);
            }

            List<string> CusDetails = new List<string>();
            foreach (var item in CustomerDetails)
            {
                var aa = item.Id.ToString();
                CusDetails.Add(aa);
                var a = item.FirstName;
                CusDetails.Add(a);
                var c = item.Email;
                CusDetails.Add(c);
                var d = item.MobileNumber;
                CusDetails.Add(d);
                var e = item.HomeAddress;
                CusDetails.Add(e);
                var f = item.City;
                CusDetails.Add(f);
            }

            return Json(new
            {
                DivId = CusDetails
            }
              , JsonRequestBehavior.AllowGet);
        }
    }
}
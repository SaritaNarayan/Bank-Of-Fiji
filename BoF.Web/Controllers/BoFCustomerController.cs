using BoF.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoF.Domain.Entities;
using BoF.BoFModels.Models.Mappers;
using BoF.BoFModels.Models;
using NHibernate;
using System.Data;
using BoFModels.Models;
using System.IO;
using System.Net.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Net;
using System.Text;
using BoF.Web.Database_Access_Layer;

namespace BoF.Web.Controllers
{
    public class BoFCustomerController : Controller
    {
        Database_Access_Layer.db dblayer = new Database_Access_Layer.db();
        Database_Access_Layer.Transaction trans = new Database_Access_Layer.Transaction();
        Database_Access_Layer.Statement statement = new Database_Access_Layer.Statement();

        private readonly ISession session;
        public IApplicationLogic applicationlogic { get; set; }
        private readonly CustomerMapper CustomerMapper;
        private readonly AccountMapper AccountMapper;
        private readonly AccountTypeMapper AccountTypeMapper;
        private readonly UserProfileMapper UserProfileMapper;
        private readonly TransactionMapper TransactionMapper;
        // GET: BoFAdmin

        public BoFCustomerController(ISession session)
        {
            this.session = session;

            if (applicationlogic == null) { applicationlogic = new ApplicationLogic(session); }
            CustomerMapper = new CustomerMapper(session);
            AccountMapper = new AccountMapper(session);
            AccountTypeMapper = new AccountTypeMapper(session);
            UserProfileMapper = new UserProfileMapper(session);
            UserProfileMapper = new UserProfileMapper(session);
            TransactionMapper = new TransactionMapper(session);
        }
        // GET: BoFCustomer
        public ActionResult BoFCustomer()
        {
            return View();
        }
        public ActionResult ShowMyPartialView()
        {
            return PartialView("_LayoutBoFCustomer");
        }
        public ActionResult BoFTransferFund()
        {
            CustomerDetailsModel RTM = new CustomerDetailsModel();
            var sysmodel = new AccountModel();
            //sysmodel.GetAccounts = getAccountList();
            RTM.AccountModel = sysmodel;

            var Bank = new SelectList(new[]
       {
                new { ID = "1", Name = "-- Please Select --"},
                new { ID = "Bank of Fiji", Name = "Bank of Fiji"},
                new { ID = "ANZ", Name = "ANZ"},
                new { ID = "Westpac", Name = "Westpac"},

            }, "ID", "Name", 1);

            ViewData["Bank"] = Bank;
            return View();
        }

        public ActionResult BillPay()
        {
            var Vendor = new SelectList(new[]
         {
                new { ID = "1", Name = "-- Please Select --"},
                new { ID = "FEA ", Name = "FEA"},
                new { ID = "Water Authority", Name = "Water Authority"},
                new { ID = "Connect - TFL", Name = "Connect - TFL"},

            }, "ID", "Name", 1);

            var When = new SelectList(new[]
         {
                new { ID = "1", Name = "-- Please Select --"},
                new { ID = "Now ", Name = "Now"},
                new { ID = "Later", Name = "Later"},

            }, "ID", "Name", 1);

            var Frequency = new SelectList(new[]
        {
                new { ID = "1", Name = "-- Please Select --"},
                new { ID = "Once ", Name = "Once"},
                new { ID = "Fortnightly", Name = "Fortnightly"},
                new { ID = "Monthly", Name = "Monthly"},
                new { ID = "Yearly", Name = "Yearly"},

            }, "ID", "Name", 1);

            

            ViewData["Vendor"] = Vendor;
            ViewData["When"] = When;
            ViewData["Frequency"] = Frequency;           

            return View();
        }

        public SelectList getAccountList()
        {
            IApplicationLogic applicationlogic = new ApplicationLogic(session);
            var accounts = applicationlogic.GetListofAccounts();

            Account account = new Account();
            account.Id = -1;
            account.AccountNumber = 1;
            accounts.Add(account);

            IEnumerable<Account> sortedRoles = accounts.OrderBy(d => d.Id);
            IList<Account> _sortedRoles = sortedRoles.ToList();
            return new SelectList(_sortedRoles, "Id", "AccountNumber");
        }

        public JsonResult GetCustomerAccounts(int id)
        {
            DataSet ds = dblayer.GetCustomerDetails(id);
            List<CustomerAcc> searchlist = new List<CustomerAcc>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                searchlist.Add(new CustomerAcc
                {
                    CustomerName = dr["FirstName"].ToString(),
                    AccountNumber = dr["AccountNumber"].ToString(),
                    AccountName = dr["AccountName"].ToString(),
                    Balance = Decimal.Parse(dr["Balance"].ToString())
                });
            }

            List<string> mystring = new List<string>();

            foreach (var i in searchlist)
            {
                var a = i.CustomerName;
                mystring.Add(a);
                var b = i.AccountNumber;
                mystring.Add(b);
                var c = i.AccountName;
                mystring.Add(c);
                var d = i.Balance.ToString();
                mystring.Add(d);
            }
            return Json(new
            {
                DivID = mystring
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerAccount(int id)
        {
            DataSet ds = dblayer.GetCustomerDetails(id);
            List<CustomerAcc> searchlist = new List<CustomerAcc>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                searchlist.Add(new CustomerAcc
                {
                    CustomerName = dr["FirstName"].ToString(),
                    AccountNumber = dr["AccountNumber"].ToString(),
                    AccountName = dr["AccountName"].ToString(),
                    Balance = Decimal.Parse(dr["Balance"].ToString())
                });
            }

            List<string> mystring = new List<string>();

            foreach (var i in searchlist)
            {
                
                var b = i.AccountNumber;
                mystring.Add(b);
                
            }
            return Json(new
            {
                DivID = mystring
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FundTransfer(CustomerDetailsModel CustomerDetailsModel)
        {
            IApplicationLogic applicationlogic = new ApplicationLogic(session);
            //var transactionentity = new Transaction();

            //CustomerDetailsModel.TransactionModel.TransDateTime = DateTime.Now;
            //CustomerDetailsModel.TransactionModel.TransactionTypeModel.
            //var transactionmodel = TransactionMapper.TransactionModelToTransaction(CustomerDetailsModel.TransactionModel);
            //transactionentity = applicationlogic.AddTransaction(transactionmodel);

            var TransAmount = CustomerDetailsModel.TransactionModel.TransAmount;
            var TransDetails = CustomerDetailsModel.TransactionModel.TransDetails;
            var TransDate = DateTime.Now;
            var AccountNumber = CustomerDetailsModel.AccountModel.AccountNumber;
            var Account_id = applicationlogic.GetaccountId(AccountNumber);
            var TransTypeId = 6;

            applicationlogic.insertTransaction(TransAmount, TransDetails, Account_id, TransTypeId);
            applicationlogic.updateAccountBalance(Account_id, TransAmount);
            //var simpleInterest = accounttypeentity.InterestRate;

            //return Json(new
            //{
            //    DivID = simpleInterest
            //}
            //    , JsonRequestBehavior.AllowGet);
            return RedirectToAction("TransactionHistory", "BoFCustomer");
        }
        public JsonResult GetTransaction(int id)
        {
            DataSet ds = trans.GetTransaction(id);
            List<TransactionHistory> searchlist = new List<TransactionHistory>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                searchlist.Add(new TransactionHistory
                {                    
                    AccountNumber = dr["AccountNumber"].ToString(),
                    AccountType = dr["AccountType"].ToString(),
                    TransactionDate = dr["TransactionDate"].ToString(),
                    Details = dr["Details"].ToString(),
                    Amount = Decimal.Parse(dr["Amount"].ToString())
                });
            }

            List<string> mystring = new List<string>();

            foreach (var i in searchlist)
            {
                var b = i.AccountNumber;
                mystring.Add(b);
                var c = i.AccountType;
                mystring.Add(c);
                var d = i.TransactionDate;
                mystring.Add(d);
                var e = i.Details;
                mystring.Add(e);
                var f = i.Amount.ToString();
                mystring.Add(f);

            }
            return Json(new
            {
                DivID = mystring
            }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetStatement(int id)
        //{
        //    IApplicationLogic applicationlogic = new ApplicationLogic(session);
        //    var acc_id = applicationlogic.GetAccountId(id);

        //    DataSet ds = statement.GetStatement(acc_id);
        //    List<StatementModel> searchlist = new List<StatementModel>();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        searchlist.Add(new StatementModel
        //        {
        //            TransAmont = dr["TransAmont"].ToString(),
        //            TransDetails = dr["TransDetails"].ToString(),
        //            TransactionDate = dr["TransactionDate"].ToString(),
        //            Debit = dr["Debit"].ToString(),
        //            Credit = Decimal.Parse(dr["Credit"].ToString(),
        //            RunningCapital = Decimal.Parse(dr["RunningCapital"].ToString())
        //        });
        //    }

        //    List<string> mystring = new List<string>();

        //    foreach (var i in searchlist)
        //    {
        //        var b = i.AccountNumber;
        //        mystring.Add(b);
        //        var c = i.AccountType;
        //        mystring.Add(c);
        //        var d = i.TransactionDate;
        //        mystring.Add(d);
        //        var e = i.Details;
        //        mystring.Add(e);
        //        var f = i.Amount.ToString();
        //        mystring.Add(f);

        //    }
        //    return Json(new
        //    {
        //        DivID = mystring
        //    }, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult TransactionHistory()
        {
            return View();
        }

        public ActionResult Statement()
        {
            return View();
        }

        public ActionResult ApplyLoan()
        {
            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].FileName != "")
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/";
                    string filename = Path.GetFileName(Request.Files[upload].FileName);
                    Request.Files[upload].SaveAs(Path.Combine(path, filename));
                }


            }


            var accountSid = "ACe963c504b9356c6203abdf843233e733";
            // Your Auth Token from twilio.com/console
            var authToken = "72747582f4207fc813fb98258cb3c2ee";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                to: new PhoneNumber("+6799123084"),
                from: new PhoneNumber("+18317773170"),
                body: "Your Application was Successfully Sumbitted. Upon consideration we will notify you by Email. BOF reserves right to decline your application. All Terms and Conditions available at our website www.bof.com.fj");

            try
            {
                new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("softmates.assistance@gmail.com", "soft786110")
                }.Send(new MailMessage { From = new MailAddress("no-reply@bof.com.fj", "Bank Of Fiji"), To = { "hrazasalman@gmail.com" }, Subject = "Application Notification", Body = "Your Application was Successfully Sumbitted. Upon consideration we will notify you by Email. BOF reserves right to decline your application. All Terms and Conditions available at our website www.bof.com.fj", BodyEncoding = Encoding.UTF8 });

            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult BillPayment(CustomerDetailsModel CustomerDetailsModel)
        {
            IApplicationLogic applicationlogic = new ApplicationLogic(session);       
                        
            var AccountNumber = CustomerDetailsModel.AccountModel.AccountNumber;
            var biller = CustomerDetailsModel.ScheduleTransModel.Biller;
            var billerAccNo = CustomerDetailsModel.ScheduleTransModel.BillerAccNo;
            var desc = CustomerDetailsModel.ScheduleTransModel.Desc;
            var TransAmount = CustomerDetailsModel.ScheduleTransModel.Amount;
            var nextRunDate = CustomerDetailsModel.ScheduleTransModel.NextRunDate.ToString();
            var Freq = CustomerDetailsModel.ScheduleTransModel.Frequency;

            var Account_id1 = applicationlogic.GetaccountId(AccountNumber);
            var Account_id = 2;
            applicationlogic.insertBillPay(Account_id, biller, billerAccNo, desc, TransAmount, nextRunDate, Freq);
            //applicationlogic.updateAccountBalance(Account_id, TransAmount);

            
            return RedirectToAction("BoFCustomer", "BoFCustomer");
        }
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public ActionResult Statements()
        {
            return View();
        }
    }
}
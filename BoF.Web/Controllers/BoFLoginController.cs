using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoF.BoFModels.Models;
using BoF.Application;
using NHibernate;
using BoF.BoFModels.Models.Mappers;
using System.DirectoryServices;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace BoF.Web.Controllers
{
    public class BoFLoginController : Controller
    {
       
        private readonly ISession session;
        public IApplicationLogic applicationlogic { get; set; }
        //private readonly CustomerMapper CustomerMapper;
        public BoFLoginController(ISession session)
        {
            this.session = session;

            if (applicationlogic == null) { applicationlogic = new ApplicationLogic(session); }
            //CustomerMapper = new CustomerMapper(session);
            //CostingSheetDetailMapper = new CostingSheetDetailMapper(session);
        }
        // GET: BoFLogin
        [HttpPost]
        public ActionResult Index(CustomerModel model)
        {
            //string AdPath = "LDAP://DCBEXCHANGE.datecfiji.com.fj/DC=datecfiji,DC=com,DC=fj";
            //ActiveDirectoryAuthentication AdAuth = new ActiveDirectoryAuthentication(AdPath);
            string Username = model.Username;
            string Password = model.Password;
            
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] oBytes = ASCIIEncoding.Default.GetBytes(Password);
            Byte[] eBytes = md5.ComputeHash(oBytes);
            Password = BitConverter.ToString(eBytes);

            var isUser = applicationlogic.AuthenticateUser(Username, Password);

            try
            {
                if (isUser == 1)
                {
                    var customerId = applicationlogic.getCustomerId(Username);
                    Session["CustomerId"] = customerId;
                    var role = applicationlogic.getRole(customerId);
                    Session["Username"] = Username;

                    if (role.Equals("Admin"))
                    {
                        //Session["Role"] = "Account Manager";
                        return RedirectToAction("BoFAdmin", "BoFAdmin");
                    }
                    ///else
                    else if (role.Equals("Customer"))
                    {
                        //Session["Role"] = "Admin";
                        return RedirectToAction("BoFCustomer", "BoFCustomer");
                    }
                    //else
                    //    ModelState.AddModelError("", "Username or password is incorrect");
                    //return RedirectToAction("Index", "Logon");
                }
                else {
                    ModelState.AddModelError("", "Username or password is incorrect");
                    return RedirectToAction("Index", "Logon");
                }

            }
            catch (DirectoryServicesCOMException)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Logon");
        }

    }
}
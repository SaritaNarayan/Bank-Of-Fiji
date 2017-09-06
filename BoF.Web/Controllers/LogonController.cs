using System.Web.Mvc;
using NHibernate;
using BoF.Application;
using BoF.Web.Models.Security;
using BoF.Web.Models;
using System.DirectoryServices;
using System.Net.Mime;
using System.DirectoryServices.AccountManagement;
using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web;
using Novacode;
using System.Linq;
using System.Diagnostics;

namespace BoF.Web.Controllers
{
    public class LogonController : Controller
    {

        public IMembershipService MembershipService { get; set; }
        public IRoleService RoleService { get; set; }
        private readonly ISession session;
        public IApplicationLogic applicationlogic { get; set; }


        #region Constructors

        public LogonController(ISession session)
        {
            this.session = session;

            if (applicationlogic == null) { applicationlogic = new ApplicationLogic(session); }

        }

        #endregion

        public ActionResult Index()
        {
            //CreateQuoteTemplate();

            return View();
        }

        public void CreateQuoteTemplate()
        {
            string filename = @"C:\Users\sharma.adarsh\Desktop\Quote-Template.docx";
            string headlineText = "\t\t\t\t\tQUOTATION";
            string imageName = "1496229952_icons_exit2.png";
            string imgUrl = Request.MapPath("/Content/images/Datec/" + imageName);

            var headerCompanyName = new Formatting();
            headerCompanyName.FontFamily = new System.Drawing.FontFamily("Calibri");
            headerCompanyName.Size = 16D;

            var headerCompanyDetails = new Formatting();
            headerCompanyDetails.FontFamily = new System.Drawing.FontFamily("Calibri");
            headerCompanyDetails.Size = 7D;

            //Title
            var headlineFormat = new Formatting();
            headlineFormat.FontFamily = new System.Drawing.FontFamily("Verdana");
            headlineFormat.Size = 14D;
            headlineFormat.Bold = true;



            var paragrph = new Formatting();
            paragrph.FontFamily = new System.Drawing.FontFamily("Cambria");
            paragrph.Size = 10D;

            var pricingTableTitle = new Formatting();
            pricingTableTitle.FontFamily = new System.Drawing.FontFamily("Cambria");
            pricingTableTitle.Size = 10D;
            pricingTableTitle.Italic = true;
            pricingTableTitle.Bold = true;
            pricingTableTitle.FontColor = System.Drawing.Color.DarkBlue;

            var terms = new Formatting();
            terms.FontFamily = new System.Drawing.FontFamily("Cambria");
            terms.Size = 10D;
            terms.Italic = true;
            terms.Bold = true;
            terms.FontColor = System.Drawing.Color.DarkBlue;

            var bulletPoint = new Formatting();
            bulletPoint.FontFamily = new System.Drawing.FontFamily("Cambria");
            bulletPoint.Size = 9D;
            bulletPoint.Bold = true;

            string headerCompany = "\t\t\tDatec(Fiji)Ltd";

            //Header details for Suva office
            string headercompDetails = "\t\t\t\t\tHead Office:";
            string headercompDetailsAddress = "\t\t\t\t\t68 Gordon Street";
            string headercompDetailsBox = "\t\t\t\t\tPO Box 12577, Suva";
            string headercompDetailsCountry = "\t\t\t\t\tFiji Islands";
            string headercompDetailsPhone = "\t\t\t\t\tTel:+679-3314411";
            string headercompDetailsFax = "\t\t\t\t\tFax:+679-3300162";

            //Header details for Nadi
            string headercompDetailsNadi = "\t\t\t\t\t\t\t\t\t\tDatec Nadi Office:";
            string headercompDetailsAddressNadi = "\t\t\t\t\t\t\t\t\t\tFirst Floor, Sunlight Building.";
            string headercompDetailsRdNadi = "\t\t\t\t\t\t\t\t\t\tDeo Street,Namaka.";
            string headercompDetailsCountryNadi = "\t\t\t\t\t\t\t\t\t\tFiji Islands";
            string headercompDetailsPhoneNadi = "\t\t\t\t\t\t\t\t\t\tTel: +679-6720181";
            string headercompDetailsFaxNadi = "\t\t\t\t\t\t\t\t\t\tFax: +679-6720194";


            string para1 = "Dear Valued Customer";
            string para2 = "Thank you for your interest in Datec's range of products and services.";
            string para3 = "Please find below is our quotation for your perusal.";
            string tableTitle = "1.     Pricing";
            string termsTitle = "2.     Terms & Conditions:";

            string para4 = "We value your Business and Datec wishes to provide better services to avoid any Business disruptions.";
            string para5 = "Thanking you for your cooperation in advance.";
            string para6 = "We are looking forward to your purchase order";
            string para7 = "Kind Regards";

            using (DocX doc = DocX.Create(filename))
            {
                //Add Header and Footer.

                //image test

                //Novacode.Image img = doc.AddImage(@"body_bg.jpg");


                doc.AddHeaders();
                doc.AddFooters();

                //Table with 5 rows and 4 cols
                Table t = doc.AddTable(5, 4);
                t.Alignment = Alignment.center;

                //Add Content to table.
                t.Rows[0].Cells[0].Paragraphs.First().Append("TO:").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                t.Rows[1].Cells[0].Paragraphs.First().Append("ORGANISATION:").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                t.Rows[2].Cells[0].Paragraphs.First().Append("FROM:").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                t.Rows[3].Cells[0].Paragraphs.First().Append("DATE:").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                t.Rows[4].Cells[0].Paragraphs.First().Append("SUBJECT:").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                t.Rows[0].Cells[2].Paragraphs.First().Append("E-MAIL:").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                t.Rows[1].Cells[2].Paragraphs.First().Append("PHONE:").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                t.Rows[2].Cells[2].Paragraphs.First().Append("E-MAIL:").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                t.Rows[3].Cells[2].Paragraphs.First().Append("PHONE:").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                t.Rows[4].Cells[2].Paragraphs.First().Append("FAX:").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);

                //COntent for Table2
                Table x = doc.AddTable(7, 5);
                x.Alignment = Alignment.center;

                x.Rows[0].Cells[0].Paragraphs.First().Append("No.").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                x.Rows[0].Cells[1].Paragraphs.First().Append("Description").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                x.Rows[0].Cells[2].Paragraphs.First().Append("Qty").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                x.Rows[0].Cells[3].Paragraphs.First().Append("Unit VIP FJD").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                x.Rows[0].Cells[4].Paragraphs.First().Append("Total VIP FJD").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                x.Rows[1].MergeCells(0, 1);
                x.Rows[3].MergeCells(0, 4);
                x.Rows[3].Paragraphs.First().Append("Sub Total").Font(new System.Drawing.FontFamily("Cambria")).FontSize(10D);
                x.Rows[4].MergeCells(0, 1);
                x.Rows[4].Paragraphs.First().Append("Accessories & Optional Items");



                //Get default header
                Header header_default = doc.Headers.odd;
                Header header = doc.Headers.odd;
                //Get default footer
                Footer footer_default = doc.Footers.odd;

                Image img = doc.AddImage(imgUrl);

                Picture pic = img.CreatePicture();
                pic.Height = 90;
                pic.Width = 160;





                var list = doc.AddList(listType: ListItemType.Bulleted);

                doc.AddListItem(list, "Price: All prices are VAT inclusive");

                doc.AddListItem(list, "Withholding Tax / Royalty Withholding / Service Tax: ");
                doc.AddListItem(list, "It is the responsibility of Customer to cater for the above Taxes if required by FRCA.", 1);
                doc.AddListItem(list, "Royalty withholding Tax - the use of, or the right to use, any copyright, patent, design or model, plan, secret formula or process, trademark, or other like property or right.", 1);
                doc.AddListItem(list, "Non-Miscellaneous Withholding Tax - Applicable to all Software License Support Subscription and Maintenance Renewals.", 1);

                doc.AddListItem(list, "Order confirmation & Payment: ");
                doc.AddListItem(list, "For customers having Account with Datec", 1);
                doc.AddListItem(list, "Hardware: 50% with Purchase Order and 50% upon Delivery. For customers who don’t have an account with Datec Fiji Ltd or are first time customers, the requirement is 100% upfront.", 2);
                doc.AddListItem(list, "Software: Purchase Order with mandatory 100% advance payments ", 2);
                doc.AddListItem(list, "Services: 50% on Commencement and 50% on Final Sign-Off", 2);
                doc.AddListItem(list, "Other customers require 100% advance payment", 1);

                doc.AddListItem(list, "Cancellation Fee:If for any reason the Customer elects to cancel the order, a levy of 15% on the quoted amount will apply. Certain items are sold on no-return basis. ");
                doc.AddListItem(list, "Quote Validity: The price is only valid for 21 days or until end of month, whichever comes first. Please do advise if price extension is required.");
                doc.AddListItem(list, "Currency Fluctuations: Prices quoted are based on current market price and the rate of exchange at the time of quotation. Datec reserves the right to adjust the pricing at the time of invoicing if the currency fluctuates beyond 3%.");
                doc.AddListItem(list, "Lead time for delivery: The ETA on the printer is 2-3 weeks from date of order however the ETA can change when/if the supplier run out of stock.  It is recommended that stock availability is rechecked before order is placed.");
                doc.AddListItem(list, "Installation: Included to just setup and connect a single user and test. The rest of the users are to be connected by IT Administrator. The procedure would be shown by our technical personnel.");
                doc.AddListItem(list, "Warranty: The warranty period on all equipment supplied by Datec Fiji Ltd will be 12 months from the date of delivery, unless otherwise specified. Datec will not be held responsible for any direct damage or loss caused to the Customer or to its related third parties due to improper usage of the hardware and software supplied by Datec. Customers may be entitled to a full replacement equivalent to the purchased item, if claims are logged with and verified by Datec within 7 days of the delivery date. In all other cases, the faulty items (if faults are covered under warranty) will be repaired. A replacement with a similar machine or equally priced item may be offered if faulty items are not repairable. Cash refunds will not be offered.");
                doc.AddListItem(list, "Definition of a Warranty Case:Warranty case is defined as a failure or malfunctioning of hardware/machines, excluding the following conditions:");
                doc.AddListItem(list, "Mechanical damage (including unintentional damage caused by misuse, liquid ingression, impact, use of unoriginal parts, downloading third party software, servicing or modification of hardware performed by people other than the Datec authorized Service Centre;", 1);
                doc.AddListItem(list, "Defects caused by violation of operating instructions provided in the hardware documentation;", 1);
                doc.AddListItem(list, "Defects caused by normal wear and tear of hardware or aging of its parts;", 1);
                doc.AddListItem(list, "Defects caused by computer viruses, use of third party software, accessories or other parts that are not approved by the Manufacturer as documented in the instructions manual;", 1);
                doc.AddListItem(list, "Defects:All Dead On Arrival (DOA) product complaints are to be notified to Datec Fiji Ltd or Datec Helpdesk within 7 days of delivery.");

                ////Header
                //Paragraph para = header_default.InsertParagraph();

                ////para.Direction = Direction.LeftToRight;
                //para.AppendPicture(pic);

                // Paragraph company = header_default.InsertParagraph(headerCompany, false, headerCompanyName);
                header_default.Pictures.Add(pic);
                Paragraph DetailsNadi = header_default.InsertParagraph(headercompDetailsNadi, false, headerCompanyDetails);
                Paragraph DetailsAddressNadi = header_default.InsertParagraph(headercompDetailsAddressNadi, false, headerCompanyDetails);
                Paragraph DetailsRdNadi = header_default.InsertParagraph(headercompDetailsRdNadi, false, headerCompanyDetails);
                Paragraph DetailsCountryNadi = header_default.InsertParagraph(headercompDetailsCountryNadi, false, headerCompanyDetails);
                Paragraph DetailsPhoneNadi = header_default.InsertParagraph(headercompDetailsPhoneNadi, false, headerCompanyDetails);
                Paragraph DetailsFaxNadi = header_default.InsertParagraph(headercompDetailsFaxNadi, false, headerCompanyDetails);
                //company.Direction = Direction.LeftToRight;

                Paragraph compDetails = header.InsertParagraph(headercompDetails, false, headerCompanyDetails);
                Paragraph compDetailsAddress = header.InsertParagraph(headercompDetailsAddress, false, headerCompanyDetails);
                Paragraph compDetailsBox = header.InsertParagraph(headercompDetailsBox, false, headerCompanyDetails);
                Paragraph compDetailsCountry = header.InsertParagraph(headercompDetailsCountry, false, headerCompanyDetails);
                Paragraph compDetailsPhone = header.InsertParagraph(headercompDetailsPhone, false, headerCompanyDetails);
                Paragraph compDetailsFax = header.InsertParagraph(headercompDetailsFax, false, headerCompanyDetails);


                doc.InsertParagraph(headlineText, false, headlineFormat);

                doc.InsertTable(t);

                doc.InsertParagraph();
                doc.InsertParagraph(para1, false, paragrph).AppendLine();
                doc.InsertParagraph(para2, false, paragrph).AppendLine();
                doc.InsertParagraph(para3, false, paragrph).AppendLine();

                doc.InsertParagraph(tableTitle, false, pricingTableTitle).AppendLine();
                doc.InsertTable(x);
                doc.InsertSectionPageBreak();
                doc.InsertParagraph(termsTitle, false, terms).AppendLine();
                doc.InsertList(list);
                doc.InsertParagraph();
                doc.InsertParagraph(para4, false, paragrph).AppendLine();
                doc.InsertParagraph(para5, false, paragrph).AppendLine();
                doc.InsertParagraph(para6, false, paragrph).AppendLine();
                doc.InsertParagraph(para7, false, paragrph).AppendLine();




                doc.Save();

                Process.Start("WINWORD.EXE", filename);

            }
        }

        [HttpPost]
        //public ActionResult Index(LoginModel model)
        //{           
        //   // ActiveDirectoryAuthentication AdAuth = new ActiveDirectoryAuthentication(AdPath);
        //    string Username = model.Username;
        //    string Password = model.Password;


        //    try
        //    {
        //        //if (true == AdAuth.AdAuthentication(Username, Password))
        //        {
        //            Session["Username"] = Username;
        //            string Role = GetActiveDirectory(Username);
        //            Session["TestRole"] = Role;

        //            if (Role.Equals("Administrator"))
        //            {
        //                Session["Role"] = "Administrator";
        //                return RedirectToAction("Index", "BankAdmin");
        //            }
        //            else if (Role.Equals("Customer"))
        //            {
        //                Session["Role"] = "Customer";
        //                return RedirectToAction("Index", "Customer");
        //            }
        //            else
        //                ModelState.AddModelError("", "Username or password is incorrect");
        //            return RedirectToAction("Index");
        //        } 
        //        else {
        //            ModelState.AddModelError("", "Username or password is incorrect");
        //            return RedirectToAction("Index");
        //        }

        //    }
        //    catch (DirectoryServicesCOMException)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return RedirectToAction("Index");

        //}

        //public string GetActiveDirectory(String username)
        //{
        //    string Role;
        //    IApplicationLogic app = new ApplicationLogic(session);
        //    //Role = app.GetRole(username);
        //    //ActiveID = app.GetActiveId(username);
        //    //RoleID = app.GetRoleId(ActiveID);            
        //    //return Role;
        //}

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

            return RedirectToAction("Index","Logon");
        }
        public class Users
        {
            public string Email { get; set; }
            public string DisplayName { get; set; }
        }

        public List<String> GetMail(string username, string pwd)
        {
            List<String> lstADUsers = new List<String>();
            string domain = "datecfiji";
            string AdPath = "LDAP://DCBEXCHANGE.datecfiji.com.fj/DC=datecfiji,DC=com,DC=fj";
            string DomainUsername = domain + @"\" + username;
            string nameer = "sen";
            DirectoryEntry entry = new DirectoryEntry(AdPath, username, pwd);

            try
            {
             //   object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + nameer + "*)";
                search.PropertiesToLoad.Add("name");
                search.PropertiesToLoad.Add("mail");
                SearchResult result;
                SearchResultCollection resultCol = search.FindAll();

                if (resultCol != null)
                {   
                    for(int counter = 0; counter<resultCol.Count; counter++)
                    {
                        result = resultCol[counter];
                        if(result.Properties.Contains("name") && result.Properties.Contains("mail"))
                        {
                           // Users objuser = new Users();
                            var DisplayName = (String)result.Properties["name"][0];
                            lstADUsers.Add(DisplayName);
                            var Email = (String)result.Properties["mail"][0];
                            lstADUsers.Add(Email);
                        }
                    }
                   
                }
                

                return lstADUsers;
            }
            catch (System.Exception e)
            {
                List<String> lstADUser = new List<String>();
                return lstADUser;
            }
           
        }



    }
        //public ActionResult Index(LogOnModel model, string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Membership.ValidateUser(model.UserName, model.Password))
        //        {
        //            MembershipUser usr = Membership.GetUser(model.UserName);
        //            Session["User"] = usr;

        //            FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
        //            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
        //                && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
        //            {
        //                //if (SecurityHelper.UserIsInRole(model.UserName, "Customer"))
        //                //{
        //                //    return RedirectToAction("Index", "Customer");
        //                //}
        //                //else if (SecurityHelper.UserIsInRole(model.UserName, "Finance") | SecurityHelper.UserIsInRole(model.UserName, "Engineer") | SecurityHelper.UserIsInRole(model.UserName, "CEO") | SecurityHelper.UserIsInRole(model.UserName, "Administrator"))
        //                //{
        //                //    return RedirectToAction("Index", "Admin");
        //                //}
        //                //else if (SecurityHelper.UserIsInRole(model.UserName, "Verification"))
        //                //{
        //                //    return RedirectToAction("Index", "Verification");
        //                //}

        //                    return RedirectToAction("Index", "Home");

        //                //return Redirect(returnUrl);
        //            }
        //            else
        //            {
        //                //if (SecurityHelper.UserIsInRole(model.UserName, "Customer"))
        //                //{
        //                //    return RedirectToAction("Index", "Customer");
        //                //}
        //                //else if (SecurityHelper.UserIsInRole(model.UserName, "Finance") | SecurityHelper.UserIsInRole(model.UserName, "Engineer") | SecurityHelper.UserIsInRole(model.UserName, "CEO") | SecurityHelper.UserIsInRole(model.UserName, "Administrator"))
        //                //{
        //                //    return RedirectToAction("Index", "Admin");
        //                //}
        //                //else if (SecurityHelper.UserIsInRole(model.UserName, "Verification"))
        //                //{
        //                //    return RedirectToAction("Index", "Verification");
        //                //}
        //                //else
        //                //{
        //                    return RedirectToAction("Index", "Home");

        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "The user name or password provided is incorrect.");
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //private void AuthenticateThisRequest()
        //{
        //    //NOTE:  if the user is already logged in (e.g. under a different user account)
        //    //       then this will NOT reset the identity information.  Be aware of this if
        //    //       you allow already-logged in users to "re-login" as different accounts 
        //    //       without first logging out.
        //    if (HttpContext.User.Identity.IsAuthenticated) return;

        //    var name = FormsAuthentication.FormsCookieName;
        //    var cookie = Response.Cookies[name];
        //    if (cookie != null)
        //    {
        //        var ticket = FormsAuthentication.Decrypt(cookie.Value);
        //        if (ticket != null && !ticket.Expired)
        //        {
        //            string[] roles = (ticket.UserData as string ?? "").Split(',');
        //            HttpContext.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(ticket), roles);
        //        }
        //    }
        //}

        //public string GetIPAddress()
        //{
        //    IPHostEntry host;
        //    string localIP = "";
        //    host = Dns.GetHostEntry(Dns.GetHostName());

        //    foreach (IPAddress ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        //        {
        //            localIP = ip.ToString();
        //        }
        //    }
        //    return localIP;
        //}

        //public static string getclientIP()
        //{
        //    string result = string.Empty;
        //    string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (!string.IsNullOrEmpty(ip))
        //    {
        //        string[] ipRange = ip.Split(',');
        //        int le = ipRange.Length - 1;
        //        result = ipRange[0];
        //    }
        //    else
        //    {
        //        result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// method to get Client ip address
        ///// </summary>
        ///// <param name="GetLan"> set to true if want to get local(LAN) Connected ip address</param>
        ///// <returns></returns>
        //public static string GetVisitorIPAddress(bool GetLan = false)
        //{
        //    string visitorIPAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        //    if (String.IsNullOrEmpty(visitorIPAddress))
        //        visitorIPAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        //    if (string.IsNullOrEmpty(visitorIPAddress))
        //        visitorIPAddress = System.Web.HttpContext.Current.Request.UserHostAddress;

        //    if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
        //    {
        //        GetLan = true;
        //        visitorIPAddress = string.Empty;
        //    }

        //    if (GetLan)
        //    {
        //        if (string.IsNullOrEmpty(visitorIPAddress))
        //        {
        //            //This is for Local(LAN) Connected ID Address
        //            string stringHostName = Dns.GetHostName();
        //            //Get Ip Host Entry
        //            IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
        //            //Get Ip Address From The Ip Host Entry Address List
        //            IPAddress[] arrIpAddress = ipHostEntries.AddressList;

        //            try
        //            {
        //                visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
        //            }
        //            catch
        //            {
        //                try
        //                {
        //                    visitorIPAddress = arrIpAddress[0].ToString();
        //                }
        //                catch
        //                {
        //                    try
        //                    {
        //                        arrIpAddress = Dns.GetHostAddresses(stringHostName);
        //                        visitorIPAddress = arrIpAddress[0].ToString();
        //                    }
        //                    catch
        //                    {
        //                        visitorIPAddress = "127.0.0.1";
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return visitorIPAddress;
        //}       
    }

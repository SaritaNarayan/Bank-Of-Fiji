using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using NHibernate;
using BoF.Application;
using BoF.Domain.Entities;
using BoF.Web.Helpers;
using BoF.Web.Models;
using BoF.Web.Models.Mappers;
using BoF.Web.Models.Security;
using System.Net;
using BoF.Web.Filters;

namespace BoF.Web.Controllers
{
    [UserActionFilter]
    public class LogoffController : Controller
    {
        public IMembershipService MembershipService { get; set; }
        public IRoleService RoleService { get; set; }
        public IApplicationLogic applicationlogic { get; set; }

        #region Constructors

        public LogoffController(ISession session)
        {
            if (applicationlogic == null) { applicationlogic = new ApplicationLogic(session); }
        }

        #endregion

        public ActionResult Index()
        {

            // MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
            MembershipUser currentUser = Membership.GetUser(Session["User"].ToString(), true);
           // var userLoginDetails = applicationlogic.GetUserLoginDetailsByUserName(currentUser.UserName);
           // userLoginDetails.LogOff = DateTime.Now;
            ///var UserLoginDetails = applicationlogic.UpdateUserLoginDetails(userLoginDetails);

            FormsAuthentication.SignOut();


            Session["User"] = null;
            Session["UserLoginDetails"] = null;
            Session["UserPasswordChanged"] = null;

            return RedirectToAction("Index", "Logon");
        }
    }
}

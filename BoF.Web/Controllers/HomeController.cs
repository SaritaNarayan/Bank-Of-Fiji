using System.Web.Mvc;
using BoF.Web.Helpers;
using BoF.Web.Filters;
using NHibernate;
using BoF.Application;
using BoF.Web.Models;
using System.Collections.Generic;
using System.Linq;
using BoF.Web.Models.Mappers;

namespace BoF.Web.Controllers
{
    [SettingsAttributes(true, "Home", 1, "13BCE087-0387-4F42-B4FD-922B34B40B54")]
    [UserActionFilter]

    public class HomeController : Controller
    {
        #region Member Variables

        private readonly ISession session;
        public IApplicationLogic applicationlogic { get; set; }

        #endregion


        #region Constructors
        public HomeController(ISession session)
        {
            this.session = session;

            if (applicationlogic == null) { applicationlogic = new ApplicationLogic(session); }
        }

        #endregion

        [SettingsAttributes(true, "Main Page", 1, "AD0AD8F8-E424-42C6-BC03-96FCEFE45598")]
        public ActionResult Index()
        {
            return View("Index");
        }

       
    }
}

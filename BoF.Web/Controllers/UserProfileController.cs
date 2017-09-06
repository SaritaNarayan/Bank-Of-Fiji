using System.Web.Mvc;
using NHibernate;
using BoF.Application;
using BoF.Web.Models.Security;


namespace BoF.Web.Controllers
{
    public class UserProfileController : Controller
    {

        public IMembershipService MembershipService { get; set; }
        public IRoleService RoleService { get; set; }
        private readonly ISession session;
        public IApplicationLogic applicationlogic { get; set; }


        #region Constructors

        public UserProfileController(ISession session)
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
       public ActionResult ShowMyPartialView()
        {
            return PartialView("_LayoutBoF");
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EOPS.Web.Controllers
{
    public class SharedController : Controller
    {
        // GET: _LayoutBoF
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SideBar()
        {
            return PartialView("_Sidebar");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoF.Web.Helpers;
//using BoF.Web.Filters;

namespace BoF.BoFModels.Controllers
{
    [SettingsAttributes(true, "Error", 3, "69111F07-13F6-41BD-BF86-D3318980D090")]
//[UserActionFilter]
    public class ErrorController : Controller
    {
        // GET: /Error/
        [SettingsAttributes(true, "Error Index", 2, "BB46BC13-02F0-4597-A4C2-630478D36E41")]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [SettingsAttributes(true, "Http Error", 2, "BB46BC13-02F0-4597-A4C2-630478D36E41")]
        public ActionResult HttpError()
        {
            Exception ex = null;
            try
            {
                ex = (Exception)HttpContext.Application[Request.UserHostAddress.ToString()];
            }
            catch
            {
            }

            if (ex != null)
            {
                ViewData["Description"] = ex.Message;
            }
            else
            {
                ViewData["Description"] = "An error occurred.";
            }
            ViewData["Title"] = "Oops. We're sorry. An error occurred and we're on the case.";
            return View("Error");
        }

        [SettingsAttributes(true, "Http 404", 2, "BB46BC13-02F0-4597-A4C2-630478D36E41")]
        public ActionResult Http404()
        {
            ViewData["Title"] = "The page you requested was not found";
            ViewData["Description"] = "Please check the URL and try again.";

            return View("Error");
        }
    }
}

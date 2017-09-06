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

namespace BoF.Web.Filters
{
    public class UserActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
            string controllerName = "";
            string ServerName = HttpContext.Current.Request.Headers["host"];

            if (routeValues != null)
            {
                if (routeValues.ContainsKey("controller"))
                {
                    controllerName = (string)filterContext.RouteData.Values["controller"];
                }
            }

            if (HttpContext.Current.Session["User"] == null)
            {
                //To update the user logoff time on the UserLogInDetails table, i have redirected it to LOGOFF page
                  HttpContext.Current.Response.Redirect("http://" + ServerName + "/Logon/Index");
                

            }
            else
            {
                if (!controllerName.Equals("Logoff"))
                    if (!controllerName.Equals("Admin"))
                    {
                        if (HttpContext.Current.Session["UserPasswordChanged"] != null && Convert.ToInt32(HttpContext.Current.Session["UserPasswordChanged"].ToString()) < 1)
                        {
                            HttpContext.Current.Response.Redirect("http://" + ServerName + "/Admin/ChangePassword");
                        }
                    }
                    else
                    {
                        if (routeValues.ContainsKey("action"))
                        {
                            var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();

                            if (!actionName.Equals("ChangePassword"))
                            {
                                if (HttpContext.Current.Session["UserPasswordChanged"] != null && Convert.ToInt32(HttpContext.Current.Session["UserPasswordChanged"].ToString()) < 1)
                                {
                                    HttpContext.Current.Response.Redirect("http://" + ServerName + "/Admin/ChangePassword");
                                }
                            }
                        }
                    }
            }

        }
    }
}
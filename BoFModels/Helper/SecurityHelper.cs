using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentSecurity;
using BoF.Web.Models;
using BoF.Web.Models.Security;

namespace BoF.BoFModels.Helpers
{
    public class SecurityHelper
    {
        public static bool ActionIsAllowedForUser(string controllerName, string actionName)
        {
            var configuration = SecurityConfiguration.Current;
            var policyContainer = configuration.PolicyContainers.GetContainerFor(controllerName, actionName);
            if (policyContainer != null)
            {
                var context = SecurityContext.Current;
                var results = policyContainer.EnforcePolicies(context);
                var truth = results.All(x => x.ViolationOccured == false);
                return truth;
            }
            return true;
        }

        public static bool UserIsAuthenticated()
        {
            var currentUser = Current.User;
            if (HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity.IsAuthenticated; //currentUser != null;
            }
            else
            {
                return false;
            }
        }

        public static IEnumerable<object> UserRoles()
        {
            var currentUser = Current.User;
            if (currentUser != null)
            {
                if (currentUser.Roles != null)
                {
                    return currentUser.Roles.Cast<object>().ToArray();
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public static bool UserIsInRole(string role)
        {
            var r = false;

            var currentUser = Current.User;
            if (currentUser != null && currentUser.RoleModels != null)
            {
                foreach (var roleModel in currentUser.RoleModels)
                {
                    if (roleModel.RoleModelName == role)
                    {
                        r = true;
                    }
                }
            }

            return r;
        }

        public static bool UserIsInRole(string username, string role)
        {
            var r = false;

            var currentUser = Current.User;
            if (currentUser != null && currentUser.RoleModels != null && currentUser.RoleModels.Count > 0)
            {
                foreach (var roleModel in currentUser.RoleModels)
                {
                    if (roleModel.RoleModelName == role)
                    {
                        r = true;
                    }
                }
            }
            else
            {
                IRoleService roleService = new RoleService();
                var registerModels = roleService.GetUserRole(username);

                foreach (var registerModel in registerModels)
                {
                    if (registerModel.RoleModelName == role)
                    {
                        r = true;
                    }
                }
            }

            return r;
        }

        public static IList<string> UserRolesStringList()
        {
            IList<string> roles = new List<string>();
            var currentUser = Current.User;
            if (currentUser != null)
            {
                if (currentUser.RoleModels != null)
                {
                    foreach (var roleModel in currentUser.RoleModels)
                    {
                        var role = roleModel.RoleModelName;
                        roles.Add(role);
                    }
                    return roles;//currentUser.RoleModels.Cast<string>().ToArray();
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }

    public static class Current
    {
        public static UserModel User
        {
            get
            {
                if (HttpContext.Current.User != null) {
                    var u = new UserModel { UserFullName = HttpContext.Current.User.Identity.Name, UserLoginName = HttpContext.Current.User.Identity.Name, UserType = HttpContext.Current.User.Identity.Name };

                    IRoleService roleService = new RoleService();
                    var registerModels = roleService.GetUserRole(HttpContext.Current.User.Identity.Name);

                    IList<RoleModel> roleModels = new List<RoleModel>();
                    IList<string> roles = new List<string>();

                    foreach (var registerModel in registerModels)
                    {
                        var roleModel = new RoleModel { RoleModelName = registerModel.RoleModelName };
                        roleModels.Add(roleModel);
                        roles.Add(registerModel.RoleModelName);
                    }
                    
                    u.RoleModels = roleModels;
                    u.Roles = roles;

                    //var user = HttpContext.Current.User as UserModel; //HttpContext.Current.Session["CurrentUser"] as IUser;
                    return u;
                }
                else
                {
                    return new UserModel();
                }
            }
            set
            {
                //HttpContext.Current.Session["CurrentUser"] = value;
                HttpContext.Current.User = value as IPrincipal;
            }
        }
    }

    public static class MvcExtensions
    {
        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace("Controller", string.Empty);
        }

        public static string GetFullControllerName(this Type controllerType)
        {
            return controllerType.FullName;
        }

        public static string GetActionName(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression)actionExpression.Body).Method.Name;
        }
    }

    public static class TagBuilderExtensions
    {
        public static TagBuilder AddAttributes(this TagBuilder tagBuilder, object htmlAttributes)
        {
            return tagBuilder.AddAttributes(htmlAttributes, false);
        }

        public static TagBuilder AddAttributes(this TagBuilder tagBuilder, object htmlAttributes, bool replaceExistingAttributes)
        {
            var attributes = new RouteValueDictionary(htmlAttributes);
            tagBuilder.MergeAttributes(attributes, replaceExistingAttributes);
            return tagBuilder;
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using FluentSecurity;
using FluentSecurity.Policy;
using BoF.Application;
using BoF.Web.Controllers;
using BoF.Web.Models;
using BoF.Web.Models.Security;

namespace BoF.Web
{
    public static class Bootstrapper
    {
        //public static ISecurityConfiguration SetupFluentSecurity()
        //{
        //    ISettingsLogic settingsLogic = new SettingsLogic();
        //    var listOfModules = settingsLogic.GetAllModules();

        //    var listOfControllers = new List<ControllerModel>();

        //    if (listOfModules.Count > 0)
        //    {
        //        foreach (var module in listOfModules)
        //        {
        //            var controllersModel = Models.Mappers.SecurityMapper.MapModule(module);

        //            listOfControllers.Add(controllersModel);
        //        }
        //    }

        //    SecurityConfigurator.Configure(configuration =>
        //    {
        //        configuration.GetAuthenticationStatusFrom(Helpers.SecurityHelper.UserIsAuthenticated);
        //        configuration.GetRolesFrom(Helpers.SecurityHelper.UserRoles);


        //        foreach (var controllerModel in listOfControllers)
        //        {

        //            var roles = new object[2];
        //            roles[0] = "Administrator";


                   
        //        }

        //        configuration.For<SettingsController>().RequireAnyRole("Administrator");
        //        configuration.For<AdminController>().RequireAnyRole("Administrator");
        //        //configuration.For<ReportingController>().RequireAnyRole("Administrator");
        //        //configuration.For<IzendaStaticResourcesController>().RequireAnyRole("Administrator");
        //        configuration.For<HomeController>().Ignore();
        //        configuration.For<ErrorController>().Ignore();
        //        configuration.For<CommonController>().Ignore();
        //        configuration.For<LogonController>().Ignore();
        //        configuration.For<LogoffController>().Ignore();
        //        configuration.For<AdminController>(x => x.ChangePassword()).Ignore();
        //        configuration.For<AdminController>(x => x.ChangePasswordSuccess()).Ignore();
        //        //configuration.IgnoreMissingConfiguration();
        //    });

        //    return SecurityConfiguration.Current;
        //}
    }

    public class AdministratorPolicy : RequireAnyRolePolicy
    {
        public AdministratorPolicy() : base(new List<object> { Role.Administrator }.ToArray()) { }
    }

    public class SecureOnlyPolicy : ISecurityPolicy
    {
        public PolicyResult Enforce(ISecurityContext context)
        {
            return HttpContext.Current.Request.IsSecureConnection ?
                PolicyResult.CreateSuccessResult(this) :
                PolicyResult.CreateFailureResult(this, "Access denied!");
        }
    }

}
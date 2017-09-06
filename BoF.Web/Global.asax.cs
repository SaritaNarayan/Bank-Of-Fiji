using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentSecurity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using BoF.Domain.Entities;
using BoF.Web.Helpers;
using BoF.Web.Models.Security;
using GenericTemp.Web;
using System.Web.Optimization;
using System.IO;
using System.Web.Security;
using BoF.Application;

namespace BoF.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {

        private static ISessionFactory _sessionFactory;
        public IMembershipService MembershipService { get; set; }
        public IApplicationLogic applicationlogic { get; set; }

        public ISessionFactory CreateSessionFactory()
        {
            if (_sessionFactory == null)
            {
                var autoPersistenceModel = CreateMapping();

                var cfg = MsSqlConfiguration.MsSql2012
                        .ConnectionString(c => c.FromAppSetting("connectionString"))
                        .AdoNetBatchSize(256);

                _sessionFactory = Fluently.Configure()
                    .Database(cfg)
                    .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "web"))
                    .Mappings(m => m.AutoMappings.Add(autoPersistenceModel))
                    //.ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            }

            return _sessionFactory;
        }


        private static AutoPersistenceModel CreateMapping()
        {
            AutoPersistenceModel autoPersistenceModel = null;

            autoPersistenceModel = AutoMap
                .Assembly(System.Reflection.Assembly.GetAssembly(typeof(Entity)))
                .Where(t => t.IsSubclassOf(typeof(Entity)))
                .Conventions.AddFromAssemblyOf<CascadeAll>();
            ;

            return autoPersistenceModel;
        }
        /// <summary>
        /// Builds schema (database tables) if export parameter is set to true.
        /// </summary>
        /// <param name="config"></param>

        //private static void BuildSchema(Configuration config)
        //{
        //    new SchemaExport(config).Create(true, true);
        //}

        readonly WindsorContainer applicationWideWindsorContainer = new WindsorContainer();

        protected void Application_Start()
        {
            // RouteTable.Routes.MapPageRoute("rs.aspx", "{*aspx}", "~/Reporting/rs.aspx", false, null, new RouteValueDictionary { { "aspx", new SpecificFileRouterConstraint("aspx", "rs.aspx") } });
            //Bootstrapper.SetupFluentSecurity();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);           
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(applicationWideWindsorContainer));

            _sessionFactory = CreateSessionFactory();            
            applicationWideWindsorContainer.Install(new WindsorInstaller(_sessionFactory));
        }

        protected void Application_BeginRequest()
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);
        }

        protected void Application_EndRequest()
        {
            CurrentSessionContext.Unbind(_sessionFactory);
        }

        protected void Application_OnEnd()
        {
            applicationWideWindsorContainer.Dispose();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            Application[HttpContext.Current.Request.UserHostAddress.ToString()] = ex;
        }

        protected void Session_End()
        {
            try
            {
                MembershipUser usr = (MembershipUser)(Session["User"]);

                var session = _sessionFactory.OpenSession();

                if (applicationlogic == null) { applicationlogic = new ApplicationLogic(session); }

                //var userLoginDetails = applicationlogic.GetUserLoginDetailsByUserName(usr.UserName);

                ////userLoginDetails = applicationlogic.UpdateUserLoginDetails(userLoginDetails);
                //int retValue = applicationlogic.UpdateUserLoginDetailsByQuery("UPDATE UserLoginDetails SET LogOff = '" + DateTime.Now + "' WHERE Id = " + userLoginDetails.Id);

                FormsAuthentication.SignOut();
                Session["User"] = null;
                Session["UserLoginDetails"] = null;
                Session["UserPasswordChanged"] = null;

                string ServerName = HttpContext.Current.Request.Headers["host"];
                HttpContext.Current.Response.Redirect("http://" + ServerName + "/Logon/Index");
                
            }
            catch (Exception ex)
            {

            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                    "Default", // Route name
                    "{controller}/{action}/{id}", // URL with parameters
                    new { controller = "Security", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                    new[] { "BoF.Web.Controllers" }
                );

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{file}.css");
            routes.MapRoute("HomeDefault", "{*pathInfo}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
        
    }   

    public class SpecificFileRouterConstraint : IRouteConstraint
    {
        private string extensionToBeRouted = null;
        private string fileToBeRouted = null;

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return !String.IsNullOrEmpty(extensionToBeRouted) && !String.IsNullOrEmpty(fileToBeRouted) && values[extensionToBeRouted] != null && values[extensionToBeRouted].ToString().ToLower().Contains(fileToBeRouted);
        }

        public SpecificFileRouterConstraint() { }

        public SpecificFileRouterConstraint(string extension, string fileName)
        {
            extensionToBeRouted = extension.ToLower();
            fileToBeRouted = fileName.ToLower();
        }
    }
}
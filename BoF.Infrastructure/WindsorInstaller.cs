using System.Web.Mvc;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web;
using NHibernate;


/*Thanks for downloading this Castle Windsor package.
You can find full list of changes in changes.txt

Documentation:		- http://docs.castleproject.org/Windsor.MainPage.ashx
Discusssion group: 	- http://groups.google.com/group/castle-project-users
StackOverflow tags:	- castle-windsor, castle

Issue tracker: 		- http://issues.castleproject.org/dashboard */

namespace RFIS.Infrastructure
{
    public class WindsorInstaller : IWindsorInstaller
    {
        private readonly ISessionFactory _sessionFactory;

        public WindsorInstaller(ISessionFactory sessionFactory)
        {
            this._sessionFactory = sessionFactory;
        }


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register all controllers from this assembly

            container.Register(
                Types.FromThisAssembly()
                .BasedOn<IController>()
                .Configure(c => c.LifestyleTransient())

            /*
            container.Register(Types.FromThisAssembly()
                           .BasedOn<Controller>()
                             .LifestyleTransient()*/

            );

            // Register HttpContext(Base) and HttpRequest(Base) so it automagically can be injected using IoC
            container.AddFacility<FactorySupportFacility>();
            container.Register(Component.For<HttpRequestBase>().LifeStyle.Transient
                .UsingFactoryMethod(() => new HttpRequestWrapper(HttpContext.Current.Request)));
            container.Register(Component.For<HttpContextBase>().LifeStyle.Transient
                .UsingFactoryMethod(() => new HttpContextWrapper(HttpContext.Current)));

            container.Register(Component.For<ISession>().UsingFactoryMethod(() => _sessionFactory.GetCurrentSession()).LifeStyle.Transient);
        }
    }
}
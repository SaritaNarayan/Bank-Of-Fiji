using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web;
using NHibernate;
using Castle.Facilities.FactorySupport;


namespace BoF.Web
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
            container.Register(Types.FromThisAssembly()
                           .BasedOn<Controller>()
                             .LifestyleTransient()

           );
            container.AddFacility<FactorySupportFacility>();
            container.Register(Component.For<HttpRequestBase>().LifeStyle.Transient
                .UsingFactoryMethod(() => new HttpRequestWrapper(HttpContext.Current.Request)));
            container.Register(Component.For<HttpContextBase>().LifeStyle.Transient
                .UsingFactoryMethod(() => new HttpContextWrapper(HttpContext.Current)));

            container.Register(Component.For<ISession>().UsingFactoryMethod(() => _sessionFactory.GetCurrentSession(), true).LifeStyle.Transient);
        }
    }
}
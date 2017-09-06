using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BoF.Web.Startup))]
namespace BoF.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

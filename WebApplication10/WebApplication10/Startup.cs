using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication10.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace WebApplication10
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

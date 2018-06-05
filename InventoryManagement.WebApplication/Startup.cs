using InventoryManagement.Identity;
using InventoryManagement.WebApplication;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

namespace InventoryManagement.WebApplication
{
    public class Startup : IdentityStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureCookieAuth(app);
        }
    }
}

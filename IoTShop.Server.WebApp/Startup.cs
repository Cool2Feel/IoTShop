using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IoTShop.Server.WebApp.Startup))]
namespace IoTShop.Server.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

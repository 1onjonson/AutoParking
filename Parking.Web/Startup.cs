using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Parking.Web.Startup))]
namespace Parking.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

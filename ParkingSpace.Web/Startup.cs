using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ParkingSpace.Web.Startup))]
namespace ParkingSpace.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

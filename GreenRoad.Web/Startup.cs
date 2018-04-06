using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreenRoad.Web.Startup))]
namespace GreenRoad.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShmffPortal.Startup))]
namespace ShmffPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(xinyue01.Startup))]
namespace xinyue01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(fangshui.Startup))]
namespace fangshui
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

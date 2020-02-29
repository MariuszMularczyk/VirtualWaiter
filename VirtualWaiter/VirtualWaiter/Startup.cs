using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VirtualWaiter.Startup))]
namespace VirtualWaiter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

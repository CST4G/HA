using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(healthApp.Startup))]
namespace healthApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

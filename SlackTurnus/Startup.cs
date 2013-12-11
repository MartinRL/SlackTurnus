using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SlackTurnus.Startup))]
namespace SlackTurnus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

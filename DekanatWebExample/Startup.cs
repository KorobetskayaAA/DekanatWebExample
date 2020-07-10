using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DekanatWebExample.Startup))]
namespace DekanatWebExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PracaInz.Startup))]
namespace PracaInz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

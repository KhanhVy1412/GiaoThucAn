using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLBB.Startup))]
namespace QLBB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

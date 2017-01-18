using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Muzoteka.Startup))]
namespace Muzoteka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

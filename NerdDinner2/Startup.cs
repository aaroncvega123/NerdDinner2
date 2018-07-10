using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NerdDinner2.Startup))]
namespace NerdDinner2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

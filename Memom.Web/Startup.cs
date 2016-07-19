using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MemomMvc52.Startup))]
namespace MemomMvc52
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

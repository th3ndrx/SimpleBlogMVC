using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleBlogMVC.Startup))]
namespace SimpleBlogMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

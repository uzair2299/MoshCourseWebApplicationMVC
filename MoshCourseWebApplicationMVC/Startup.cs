using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoshCourseWebApplicationMVC.Startup))]
namespace MoshCourseWebApplicationMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

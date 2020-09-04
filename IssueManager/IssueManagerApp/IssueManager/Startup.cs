using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IssueManager.Startup))]
namespace IssueManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

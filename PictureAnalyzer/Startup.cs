using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PictureAnalyzer.Startup))]
namespace PictureAnalyzer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

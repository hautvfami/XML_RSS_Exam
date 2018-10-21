using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XML.Startup))]
namespace XML
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

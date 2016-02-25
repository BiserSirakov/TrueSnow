using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrueSnow.Web.Startup))]

namespace TrueSnow.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

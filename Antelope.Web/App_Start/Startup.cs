using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(Antelope.Web.Startup))]

namespace Antelope.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}
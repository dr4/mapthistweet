using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MapThisTweet.Startup))]

namespace MapThisTweet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}

using MapThisTweet.DataProviders;
using System.Web.Http;
using System.Web.Mvc;

namespace MapThisTweet
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            CitiesRepository.Start();
            TweetsRepository.Start();
        }
    }
}

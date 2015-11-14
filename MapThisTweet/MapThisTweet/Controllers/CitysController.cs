using MapThisTweet.DataProviders;
using MapThisTweet.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace MapThisTweet.Controllers
{
    public class CitysController : ApiController
    {
        public IEnumerable<CityContainer> GetAllCitys()
        {
            return CitiesRepository.SelectAll();
        }
    }
}

using MapThisTweet.DataProviders;
using MapThisTweet.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace MapThisTweet.Controllers
{
    public class CitiesController : ApiController
    {
        [HttpGet, Route("api/cities")]
        public IEnumerable<CityContainer> GetAllCitys()
        {
            return CitiesRepository.SelectAll();
        }
    }
}

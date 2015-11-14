using MapThisTweet.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MapThisTweet.Controllers
{
    public class CitysController : ApiController
    {
        private static CityContainer[] allCities;
        public static void Initialize()
        {
            string citiesFilePath = Path.Combine(HttpRuntime.AppDomainAppPath, @"data\cities-1447495410782.json");

            var serializer = new JsonSerializer();

            using (var streamReader = new StreamReader(citiesFilePath))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                allCities = serializer.Deserialize<City[]>(jsonReader)
                                      .Where(c => c.Results != null && c.Results.Length > 0)
                                      .Select(c => new CityContainer
                                      {
                                          Id = c.Id,
                                          Name = c.Name,
                                          ChineseName = c.ChineseName,
                                          Location = c.Results[0].Geometry.Location
                                      })
                                      .ToArray();
            }
        }

        public IEnumerable<CityContainer> GetAllCitys()
        {
            return allCities;
        }
    }
}

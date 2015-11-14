using MapThisTweet.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MapThisTweet.DataProviders
{
    public static class CitiesRepository
    {
        public static CityContainer[] allCities;
        public static long[] allCityIds;

        public static void Start()
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

            allCityIds = allCities.Select(c => c.Id).ToArray();
        }

        public static IEnumerable<CityContainer> SelectAll()
        {
            return allCities;
        }
    }

    internal class City
    {
        [JsonProperty(PropertyName = "city")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "cn")]
        public string ChineseName { get; set; }
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "results")]
        public Result[] Results { get; set; }
    }

    internal class Result
    {
        [JsonProperty(PropertyName = "geometry")]
        public Geometry Geometry { get; set; }
    }

    internal class Geometry
    {
        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }
    }
}
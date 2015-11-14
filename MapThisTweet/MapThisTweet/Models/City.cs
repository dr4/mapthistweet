using Newtonsoft.Json;
using System.Collections.Generic;

namespace MapThisTweet.Models
{
    public class City
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


    public class CityContainer
    {
        [JsonProperty(PropertyName = "city")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "cn")]
        public string ChineseName { get; set; }
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }
    }

    public class Result
    {
        [JsonProperty(PropertyName = "geometry")]
        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }
    }

    public class Location
    {
        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }
        [JsonProperty(PropertyName = "lng")]
        public double Longitude { get; set; }
    }
}
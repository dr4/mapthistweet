using Newtonsoft.Json;

namespace MapThisTweet.Models
{
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

    public class Location
    {
        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }
        [JsonProperty(PropertyName = "lng")]
        public double Longitude { get; set; }
    }
}
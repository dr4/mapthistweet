using Newtonsoft.Json;
using System;

namespace MapThisTweet.Models
{
    public class TweetContainer
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "retweetCount")]
        public int RetweetCount { get; set; }
        [JsonProperty(PropertyName = "userName")]
        public string ScreenName { get; set; }
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "cityId")]
        public long CityId { get; set; }
    }
}
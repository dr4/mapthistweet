using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweetinvi;

namespace MapThisTweet.Controllers
{
    public class TweetsController : ApiController
    {
        private static Models.Tweet[] tweets =
        {
            new Models.Tweet { Id = 1, Title = "First" },
            new Models.Tweet { Id = 2, Title = "Second" },
            new Models.Tweet { Id = 3, Title = "Third" }
        };
        public IEnumerable<Models.Tweet> GetAllTweets()
        {
            Auth.SetUserCredentials("CONSUMER_KEY", "CONSUMER_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");
            return tweets;
        }

        public IHttpActionResult GetTweet(long id)
        {
            var tweet = tweets.FirstOrDefault((t) => t.Id == id);
            if (tweet == null)
            {
                return NotFound();
            }
            return Ok(tweet);
        }
    }
}

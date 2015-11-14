using MapThisTweet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MapThisTweet.Controllers
{
    public class TweetsController : ApiController
    {
        private static Tweet[] tweets =
        {
            new Tweet { Id = 1, Title = "First" },
            new Tweet { Id = 2, Title = "Second" },
            new Tweet { Id = 3, Title = "Third" }
        };
        public IEnumerable<Tweet> GetAllTweets()
        {
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

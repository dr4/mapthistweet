using MapThisTweet.DataProviders;
using MapThisTweet.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace MapThisTweet.Controllers
{
    public class TweetsController : ApiController
    {
        [HttpGet, Route("api/tweets/{hashTag}")]
        public IEnumerable<TweetContainer> GetTweets(string hashTag = null)
        {
            return TweetsRepository.SelectAll(hashTag);
        }

        [HttpGet, Route("api/tweets")]
        public IEnumerable<TweetContainer> GetAllTweets()
        {
            return GetTweets();
        }
    }
}

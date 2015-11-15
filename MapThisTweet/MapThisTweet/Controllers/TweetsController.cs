using MapThisTweet.DataProviders;
using MapThisTweet.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace MapThisTweet.Controllers
{
    public class TweetsController : ApiController
    {
        public static bool IsFun { get; set; }

        [HttpGet, Route("api/tweets/{hashTag}")]
        public IEnumerable<TweetContainer> GetTweets(string hashTag = null)
        {
            if (!IsFun)
            {
                return TweetsRepository.Select(hashTag);
            }
            else
            {
                return FunTweetsRepository.Select();
            }
        }

        [HttpGet, Route("api/tweets")]
        public IEnumerable<TweetContainer> GetAllTweets()
        {
            return GetTweets();
        }
    }
}

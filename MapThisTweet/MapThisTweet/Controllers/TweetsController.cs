using MapThisTweet.DataProviders;
using MapThisTweet.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace MapThisTweet.Controllers
{
    public class TweetsController : ApiController
    {
        public IEnumerable<TweetContainer> GetAllTweets()
        {
            return TweetsRepository.SelectAll();
        }
    }
}

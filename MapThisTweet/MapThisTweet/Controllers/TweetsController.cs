using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tweetinvi;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Interfaces.Streaminvi;

namespace MapThisTweet.Controllers
{
    public class TweetsController : ApiController
    {
        private static bool registered;
        private static readonly ConcurrentQueue<TweetContainer> queue = new ConcurrentQueue<TweetContainer>();

        public IEnumerable<TweetContainer> GetAllTweets()
        {
            if (!registered)
            {
                registered = true;
                // TODO: add credentials
                
                Stream_SampleStreamExample();
                return new TweetContainer[0];
            }
            else
            {
                stream.StopStream();

                var list = new List<TweetContainer>();

                TweetContainer tweet;
                while(queue.TryDequeue(out tweet))
                {
                    list.Add(tweet);
                }

                return list;
            }
        }

        public IHttpActionResult GetTweet(long id)
        {
            TweetContainer tweet = null;
            if (tweet == null)
            {
                return NotFound();
            }
            return Ok(tweet);
        }

        private static IFilteredStream stream;

        public static void Stream_SampleStreamExample()
        {
            stream = Stream.CreateFilteredStream();

            stream.MatchingTweetReceived += (sender, args) =>
            {
                var tweet = args.Tweet;
                if (!tweet.IsRetweet)
                {
                    var tweetContainer = new TweetContainer
                    {
                        CreatedAt = tweet.CreatedAt,
                        ScreenName = tweet.CreatedBy.ScreenName,
                        Id = tweet.Id,
                        IsRetweet = tweet.IsRetweet,
                        RetweetCount = tweet.RetweetCount,
                        Text = tweet.Text
                    };

                    queue.Enqueue(tweetContainer);
                }
            };

            stream.AddTweetLanguageFilter(Language.English);
            stream.AddTrack("china");

            stream.StartStreamMatchingAllConditionsAsync();
        }

        public class TweetContainer
        {
            public DateTime CreatedAt { get; set; }
            public long Id { get; set; }
            public bool IsRetweet { get; set; }
            public int RetweetCount { get; set; }
            public string ScreenName { get; set; }
            public string Text { get; set; }
        }
    }
}

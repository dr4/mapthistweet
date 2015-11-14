using MapThisTweet.Common;
using MapThisTweet.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using Tweetinvi;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces.Streaminvi;

namespace MapThisTweet.DataProviders
{
    public static class TweetsRepository
    {
        private static LimitedConcurrentQueue<TweetContainer> queue = new LimitedConcurrentQueue<TweetContainer>();

        private static IFilteredStream stream;

        public static void Start()
        {
            ConfigureTwitterCredentials();

            var random = new Random();
            int maxCityId = CitiesRepository.allCityIds.Length;

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
                        RetweetCount = tweet.RetweetCount,
                        Text = tweet.Text,
                        CityId = CitiesRepository.allCityIds[new Random().Next(0, maxCityId - 1)]
                    };

                    queue.Enqueue(tweetContainer);
                }
            };

            stream.AddTweetLanguageFilter(Language.English);
            stream.AddTrack("china");

            stream.StartStreamMatchingAllConditionsAsync();
        }

        public static void Pause()
        {
            stream.PauseStream();
        }

        public static void Resume()
        {
            stream.ResumeStream();
        }

        public static IEnumerable<TweetContainer> SelectAll()
        {
            return queue.ToArray();
        }

        private static void ConfigureTwitterCredentials()
        {
            NameValueCollection settings = ConfigurationManager.AppSettings;
            string consumerKey = settings["consumerKey"];
            string consumerSecret = settings["consumerSecret"];
            string userAccessToken = settings["userAccessToken"];
            string userAccessSecret = settings["userAccessSecret"];

            Auth.SetUserCredentials(consumerKey, consumerSecret, userAccessToken, userAccessSecret);
        }
    }
}
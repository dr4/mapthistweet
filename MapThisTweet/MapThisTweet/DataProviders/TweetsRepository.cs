using MapThisTweet.Common;
using MapThisTweet.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using Tweetinvi;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Interfaces.Streaminvi;

namespace MapThisTweet.DataProviders
{
    public static class TweetsRepository
    {
        private static LimitedConcurrentQueue<TweetContainer> defaultQueue = new LimitedConcurrentQueue<TweetContainer>();

        private static IFilteredStream defaultStream;

        private static readonly ConcurrentDictionary<string, IFilteredStream> hashTagStreams = new ConcurrentDictionary<string, IFilteredStream>();
        private static readonly ConcurrentDictionary<string, LimitedConcurrentQueue<TweetContainer>> hashTagQueues = new ConcurrentDictionary<string, LimitedConcurrentQueue<TweetContainer>>();

        private static bool pause;

        public static void Start()
        {
            ConfigureTwitterCredentials();
            defaultStream = CreateFilteredStream();
        }

        public static void Pause()
        {
            pause = true;
            defaultStream.PauseStream();
        }

        public static void Resume()
        {
            pause = false;
            defaultStream.ResumeStream();
        }

        public static IEnumerable<TweetContainer> Select(string hashTag)
        {
            if(pause)
            {
                return new TweetContainer[0];
            }

            if (string.IsNullOrWhiteSpace(hashTag))
            {
                return defaultQueue.ToArray();
            }
            else
            {
                bool isNew = false;
                LimitedConcurrentQueue<TweetContainer> hashTagQueue = hashTagQueues.GetOrAdd(hashTag, _ =>
                {
                    isNew = true;
                    return new LimitedConcurrentQueue<TweetContainer>();
                });

                if (isNew)
                {
                    CreateHashTagFilteredStream(hashTag);
                }

                return hashTagQueue.ToArray();
            }
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

        private static void CreateHashTagFilteredStream(string hashTag)
        {
            IFilteredStream oldStream = hashTagStreams.GetOrAdd(hashTag, _ =>
            {
                return CreateFilteredStream(hashTag);
            });
        }

        private static IFilteredStream CreateFilteredStream(string hashTag = null)
        {
            LimitedConcurrentQueue<TweetContainer> queue;
            if(!string.IsNullOrWhiteSpace(hashTag))
            {
                queue = hashTagQueues.GetOrAdd(hashTag, _ =>
                {
                    return new LimitedConcurrentQueue<TweetContainer>();
                });
            }
            else
            {
                queue = defaultQueue;
            }

            var random = new Random();
            int maxCityId = CitiesRepository.allCityIds.Length;

            var stream = Stream.CreateFilteredStream();

            stream.MatchingTweetReceived += (sender, args) =>
            {
                var tweet = args.Tweet;
                if (!tweet.IsRetweet)
                {
                    IUser user = tweet.CreatedBy;
                    var tweetContainer = new TweetContainer
                    {
                        CreatedAt = tweet.CreatedAt,
                        User = user.ScreenName,
                        UserFullName = user.Name,
                        Avatar = user.ProfileImageUrl,
                        Id = tweet.Id,
                        RetweetCount = tweet.RetweetCount,
                        Text = tweet.Text,
                        CityId = CitiesRepository.allCityIds[new Random().Next(0, maxCityId - 1)]
                    };

                    queue.Enqueue(tweetContainer);
                }
            };

            stream.AddTweetLanguageFilter(Language.English);

            string track = !string.IsNullOrWhiteSpace(hashTag)
                ? string.Format("china {0}", hashTag)
                : "china";

            stream.AddTrack(track);

            stream.StartStreamMatchingAllConditionsAsync();

            return stream;
        }
    }
}
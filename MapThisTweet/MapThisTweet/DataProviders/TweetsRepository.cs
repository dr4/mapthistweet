﻿using MapThisTweet.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Tweetinvi;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces.Streaminvi;

namespace MapThisTweet.DataProviders
{
    public static class TweetsRepository
    {
        private static ConcurrentQueue<TweetContainer> queue = new ConcurrentQueue<TweetContainer>();

        private static IFilteredStream stream;

        public static void Start()
        {
            // REPLACE WITH Auth

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
            ConcurrentQueue<TweetContainer> tweets = Interlocked.Exchange(ref queue, new ConcurrentQueue<TweetContainer>());
            return tweets.ToArray();
        }
    }
}
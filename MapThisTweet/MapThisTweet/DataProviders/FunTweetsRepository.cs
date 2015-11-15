using MapThisTweet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapThisTweet.DataProviders
{
    public static class FunTweetsRepository
    {
        private sealed class FunUser
        {
            public string User { get; set; }
            public string UserFullName { get; set; }
            public string Avatar { get; set; }
        }

        private static readonly FunUser[] users =
        {
            new FunUser
            {
                User = "BarackObama",
                UserFullName = "Barack Obama",
                Avatar = "https://pbs.twimg.com/profile_images/451007105391022080/iu1f7brY_bigger.png"
            },
            new FunUser
            {
                User = "justinbieber",
                UserFullName = "Justin Bieber",
                Avatar = "https://pbs.twimg.com/profile_images/652596362073272320/Zv6K-clv_bigger.jpg"
            },
            new FunUser
            {
                User = "katyperry",
                UserFullName = "KATY PERRY",
                Avatar = "https://pbs.twimg.com/profile_images/638354019371479040/OVUABU1D_bigger.jpg"
            },
            new FunUser
            {
                User = "taylorswift13",
                UserFullName = "Taylor Swift",
                Avatar = "https://pbs.twimg.com/profile_images/505200807503867904/osJXmYRl_bigger.jpeg"
            },
            new FunUser
            {
                User = "NatGeo",
                UserFullName = "National Geographic",
                Avatar = "https://pbs.twimg.com/profile_images/648950685074071552/ZREwOKBz_bigger.jpg"
            },
            new FunUser
            {
                User = "MedvedevRussiaE",
                UserFullName = "Dmitry Medvedev",
                Avatar = "https://pbs.twimg.com/profile_images/2348669225/5sfntu2zxyvans8vf9n8_bigger.png"
            },
            new FunUser
            {
                User = "BBCBreaking",
                UserFullName = "BBC Breaking News",
                Avatar = "https://pbs.twimg.com/profile_images/460740982498013184/wIPwMwru_bigger.png"
            },
            new FunUser
            {
                User = "nytimes",
                UserFullName = "The New York Times",
                Avatar = "https://pbs.twimg.com/profile_images/2044921128/finals_bigger.png"
            },
            new FunUser
            {
                User = "realDonaldTrump",
                UserFullName = "Donald J. Trump",
                Avatar = "https://pbs.twimg.com/profile_images/1980294624/DJT_Headshot_V2_bigger.jpg"
            }
        };

        public static readonly string[] texts =
        {
            "DAMN, YOU are AWESOME",
            "PURE AWESOMENESS",
            "SUPERB AS USUAL",
            "JUST BRILLIANT",
            "IF IT WAS A MOVIE I WOULD GIVE FIVE STARS",
            "ABSOLUTELY STUNNING",
            "PLEASE HIRE ME",
            "WHERE CAN I BUY EPAM SHARES?",
            "LOL",
            "SEE YOU ON #EPAM SEC IN #Philadelphia",
            "I WANT TO THANK YOU FOR THIS WONDERFUL EVENT",
            "ANDREY BODOEV and DZMITRY RADCHANKA are THE BEST. GIVE THEM 100 of 100!",
            "WOW, SO MANY SENIOR ENGINEERS HERE I FEEL HOT",
            "THE BEST IT EVENT OF THE YEAR!",
            "CAN YOU BUILD ME A WEBSITE??",
            "HI 2 ALL in #Shenzhen!",
            "HOPE ALL OF YOU HAVE FUN WITH THIS WONDERFUL TEAM",
            "WHY ARE YOU IGNORING ME?!"
        };

        private static readonly string[] speakers =
        {
            "Gino Marckx",
            "Juriy Bura",
            "Yebgeniy Galper",
            "Darren Lee Starsmore",
            "Balazs Fejes2",
            "Dorothy Ho"
        };

        public static IEnumerable<TweetContainer> Select()
        {
            int userCount = users.Length;
            int textCount = texts.Length;
            int cityCount = CitiesRepository.allCityIds.Length;
            int speakersCount = speakers.Length;

            var random = new Random();

            return Enumerable.Range(1, 20)
                             .Select(_ =>
                             {
                                 int userId = random.Next(0, userCount - 1);
                                 FunUser user = users[userId];

                                 int textId = random.Next(0, textCount - 1);
                                 string text = texts[textId];

                                 int cityIdIndex = random.Next(0, cityCount - 1);
                                 long cityId = CitiesRepository.allCityIds[cityIdIndex];

                                 int speakerId = random.Next(0, speakersCount - 1);
                                 string speaker = speakers[speakerId];

                                 text = string.Format("{0}, {1} #epamhackfest", speaker, text);

                                 return new TweetContainer
                                 {
                                     IsImportant = true,
                                     User = user.User,
                                     UserFullName = user.UserFullName,
                                     Avatar = user.Avatar,
                                     Text = text,
                                     CityId = cityId
                                 };
                             })
                             .ToArray();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPluginForAtomSite
{
    public class TwitterStructs
    {
        public const string TwitterConfigFileName = "Twitter.config";
        public const string TwitterConfigRootElement = "TwitterSettings";
        public const string TwitterConfigUserElement = "Username";
        public const string TwitterConfigLimitElement = "Limit";
        public const string TwitterCurrentSettings = "TwitterCurrentSettings";
        public const string TwitterUser = "TwitterUser-";
        public const string TwitterCurrentTweets = "TwitterCurrentTweets";

        public class Tweet
        {
            public string Text { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Source { get; set; }
            public string InReplyToStatusURL { get; set; }
            public string InReplyToScreenName { get; set; }
        }
        public class User
        {
            public string ScreenName { get; set; }
            public string Name { get; set; }
            public string ID { get; set; }
            public string HomeURL { get; set; }
            public string ImageURL { get; set; }
            public string TwitterHomeURL { get; set; }
            public string Followers { get; set; }
            public string FriendsCount { get; set; }
            public string StatusCount { get; set; }
            public string Description { get; set; }
       }
        public class Twitter
        {
            public User User { get; set; }
            public List<Tweet> Tweets { get; set; }
        }

        public class Settings
        {
            public string UserName { get; set; }
            public int? Limit { get; set; }
        }
    }
}

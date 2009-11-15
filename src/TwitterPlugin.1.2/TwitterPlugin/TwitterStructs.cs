﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPluginForAtomSite
{
    public class TwitterStructs
    {
        public static class TwitterConsts
        {
            public const string TwitterConfigFileName = "Twitter.config";
            public const string TwitterConfigRootElement = "TwitterSettings";
            public const string TwitterConfigUserElement = "Username";
            public const string TwitterConfigLimitElement = "Limit";
            public const string TwitterCacheDurationElement = "CacheDuration";
            public const string TwitterCurrentSettings = "TwitterCurrentSettings";
            public const string TwitterCachePrefix = "TwitterPluginCache-";
            public static string TwitterUser { get { return TwitterCachePrefix + "TwitterUser-"; } }
            public static string TwitterCurrentTweets { get { return TwitterCachePrefix + "TwitterCurrentTweets-"; } }
            public static TimeSpan TwitterDefaultCacheDuration { get { return new TimeSpan(0, 10, 0); } }
            public const int TwitterDefaultLimit = 5;
        }
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
            public string FollowersLink { get { return string.Format("http://twitter.com/{0}/followers", ScreenName); } }
            public string FriendsCount { get; set; }
            public string FriendsLink { get { return string.Format("http://twitter.com/{0}/following", ScreenName); } }
            public string StatusCount { get; set; }
            public string Description { get; set; }
            public string ProfileURL { get { return string.Format("http://twitter.com/account/profile_image/{0}", ScreenName); } }
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
            public TimeSpan? CacheDuration { get; set; }
        }

        public class TwitterCacheObject
        {
            public Object Cached { get; set; }
            public DateTime When { get; set; }
        }
    }
}
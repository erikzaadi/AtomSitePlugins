using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPluginForAtomSite
{
    public class TwitterStructs
    {
        public class Tweet
        {
            public string Text { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Source { get; set; }
            public User InReplyTo { get { return !string.IsNullOrEmpty(InReplyToUserID) ? TwitterInteraction.GetTwitterUser(InReplyToUserID) : null; } }
            public string InReplyToUserID { get; set; }
            public string InReplyToStatusID { get; set; }
            public string InReplyToStatusURL { get { return InReplyTo != null && !string.IsNullOrEmpty(InReplyToStatusID) ? string.Format("http://twitter.com/{0}/status/{1}", InReplyTo.ScreenName, InReplyToStatusID) : ""; } }
        }
        public class User
        {
            public string ScreenName { get; set; }
            public string Name { get; set; }
            public string ID { get; set; }
            public string HomeURL { get; set; }
            public string ImageURL { get; set; }
            public string TwitterHomeURL { get; set; }
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

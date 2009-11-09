using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPlugin
{
    public class TwitterBridge
    {

        public static LinqToTwitter.Status[] GetUpdates(string TwitterName)
        {
            return GetUpdates(TwitterName, 5);
        }

        public static LinqToTwitter.Status[] GetUpdates(string TwitterName, int Limit)
        {
            var ctx = new LinqToTwitter.TwitterContext();
            var tweets = ctx.Status.Where(p =>
                p.Type == LinqToTwitter.StatusType.User
                && p.ScreenName == TwitterName
                && p.Count == Limit)
                .ToArray();
            return tweets;
        }
    }
}

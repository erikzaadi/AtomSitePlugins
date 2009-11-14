using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TwitterPluginForAtomSite
{
    public class TwitterInteraction
    {
        public static TwitterStructs.Twitter GetUpdates(
            TimeSpan CacheDuration,
            string TwitterName,
            int Limit)
        {
            var cached = TwitterCacheManager.Get<TwitterStructs.Twitter>(
                TwitterStructs.TwitterConsts.TwitterCurrentTweets + TwitterName,
                CacheDuration);
            if (cached != null)
                return cached;
            var webClient = new System.Net.WebClient();
            string url = string.Format("http://twitter.com/status/user_timeline/{0}.xml?count={1}",
                TwitterName,
                Limit);
            string resultXML = string.Empty;
            try
            {
                resultXML = webClient.DownloadString(url);
            }
            catch
            {
                return null;
            }
            if (string.IsNullOrEmpty(resultXML))
                return null;

            XDocument result = null;
            try
            {
                result = XDocument.Parse(resultXML);
            }
            catch
            {
                return null;
            }
            if (result == null)
                return null;
            var toReturn = new TwitterStructs.Twitter();
            var statusArray = result.Descendants("status");
            var user = from u in statusArray.Take(1).Descendants("user")
                       select new TwitterStructs.User
                       {
                           ID = ElementValueSingleOrDefault(u, "id"),
                           ImageURL = ElementValueSingleOrDefault(u, "profile_image_url"),
                           ScreenName = ElementValueSingleOrDefault(u, "screen_name"),
                           HomeURL = ElementValueSingleOrDefault(u, "url"),
                           Name = ElementValueSingleOrDefault(u, "name"),
                           TwitterHomeURL = string.Format("http://twitter.com/{0}", ElementValueSingleOrDefault(u, "screen_name")),
                           Description = ElementValueSingleOrDefault(u, "description"),
                           Followers = ElementValueSingleOrDefault(u, "followers_count"),
                           FriendsCount = ElementValueSingleOrDefault(u, "friends_count"),
                           StatusCount = ElementValueSingleOrDefault(u, "statuses_count"),
                       };
            toReturn.User = user.SingleOrDefault();
            var tweets = from tw in statusArray
                         let dateParts =
                         ElementValueSingleOrDefault(tw, "created_at").Split(' ')
                         let replyToScreenName =
                         ElementValueSingleOrDefault(tw, "in_reply_to_screen_name")
                         select new TwitterStructs.Tweet
                         {
                             CreatedAt = DateTime.Parse(
                                 string.Format("{0} {1} {2} {3} GMT",
                                 dateParts[1],
                                 dateParts[2],
                                 dateParts[5],
                                 dateParts[3]),
                                 System.Globalization.CultureInfo.InvariantCulture),
                             Source = ElementValueSingleOrDefault(tw, "source"),
                             Text = AddMarkupToTweet(ElementValueSingleOrDefault(tw, "text")),
                             InReplyToScreenName = replyToScreenName,
                             InReplyToStatusURL = !string.IsNullOrEmpty(replyToScreenName) ? string.Format("http://twitter.com/{0}/status/{1}", replyToScreenName, ElementValueSingleOrDefault(tw, "in_reply_to_status_id")) : ""
                         };
            toReturn.Tweets = tweets.ToList();
            TwitterCacheManager.Set(toReturn,
                TwitterStructs.TwitterConsts.TwitterCurrentTweets + TwitterName);
            return toReturn;
        }

        private static string ElementValueSingleOrDefault(XElement element, string name)
        {
            return element.Element(name) != null && !string.IsNullOrEmpty(element.Element(name).Value) ? element.Element(name).Value : "";
        }

        public static TwitterStructs.User GetTwitterUser(TimeSpan CacheDuration, string TwitterName)
        {
            var cached = TwitterCacheManager.Get<TwitterStructs.User>(
                TwitterStructs.TwitterConsts.TwitterUser + TwitterName,
                CacheDuration);
            if (cached != null)
                return cached;

            var webClient = new System.Net.WebClient();
            string url = string.Format("http://twitter.com/users/show/{0}.xml", TwitterName);
            string resultXML = string.Empty;
            try
            {
                resultXML = webClient.DownloadString(url);
            }
            catch
            {
                return null;
            }
            if (string.IsNullOrEmpty(resultXML))
                return null;

            XDocument result = null;
            try
            {
                result = XDocument.Parse(resultXML);
            }
            catch
            {
                return null;
            }
            if (result == null)
                return null;
            var user = from u in result.Elements()
                       select new TwitterStructs.User
                       {
                           ID = u.Element("id").Value,
                           ImageURL = u.Element("profile_image_url").Value,
                           ScreenName = u.Element("screen_name").Value,
                           HomeURL = u.Element("url").Value,
                           Name = u.Element("name").Value,
                           TwitterHomeURL = string.Format("http://twitter.com/{0}", u.Element("screen_name").Value),
                           Description = ElementValueSingleOrDefault(u, "description"),
                           Followers = ElementValueSingleOrDefault(u, "followers_count"),
                           FriendsCount = ElementValueSingleOrDefault(u, "friends_count"),
                           StatusCount = ElementValueSingleOrDefault(u, "statuses_count")
                       };
            var toReturn = user.SingleOrDefault();
            TwitterCacheManager.Set(toReturn, TwitterStructs.TwitterConsts.TwitterUser + TwitterName);
            return toReturn;
        }

        public static string AddMarkupToTweet(string Tweet)
        {
            string tagRegex = @"(\#)\w+[\w]";
            string mentionRegex = @"(\@)\w+[\w]";
            string linkRegex = @"((https|http):\/\/+[\w\d\.\-_\?\&\%\/\=\#]*)";

            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(linkRegex);
            foreach (System.Text.RegularExpressions.Capture caught in reg.Match(Tweet).Captures)
            {
                Tweet = Tweet.Replace(caught.Value, string.Format("<span class=\"TwitterStatusLink\"><a href=\"{0}\">{0}</a></span>", caught.Value));
            }
            reg = new System.Text.RegularExpressions.Regex(tagRegex);
            foreach (System.Text.RegularExpressions.Capture caught in reg.Match(Tweet).Captures)
            {
                Tweet = Tweet.Replace(caught.Value, string.Format("<span class=\"TwitterStatusTag\"><a href=\"http://twitter.com/search?q=%23{0}\">{1}</a></span>", caught.Value.Substring(1, caught.Length - 1), caught.Value));
            }
            reg = new System.Text.RegularExpressions.Regex(mentionRegex);
            foreach (System.Text.RegularExpressions.Capture caught in reg.Match(Tweet).Captures)
            {
                Tweet = Tweet.Replace(caught.Value, string.Format("<span class=\"TwitterStatusMention\">@<a href=\"http://twitter.com/{0}\">{0}</a></span>", caught.Value.Substring(1, caught.Length - 1)));
            }

            return Tweet;
        }

    }
}

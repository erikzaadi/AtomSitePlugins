using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TwitterPluginForAtomSite
{
    public class TwitterInteraction
    {
        public static TwitterStructs.Twitter GetUpdates(string TwitterName)
        {
            return GetUpdates(TwitterName, 5);
        }

        public static TwitterStructs.Twitter GetUpdates(string TwitterName, int Limit)
        {
            var webClient = new System.Net.WebClient();
            string url = string.Format("http://twitter.com/status/user_timeline/{0}.xml?count={1}", TwitterName, Limit);
            string resultXML = webClient.DownloadString(url);
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
                           ID = u.Element("id").Value,
                           ImageURL = u.Element("profile_image_url").Value,
                           ScreenName = u.Element("screen_name").Value,
                           HomeURL = u.Element("url").Value,
                           Name = u.Element("name").Value,
                           TwitterHomeURL = string.Format("http://twitter.com/{0}", u.Element("screen_name").Value)
                       };
            toReturn.User = user.SingleOrDefault();
            var tweets = from tw in statusArray
                         let dateParts =
                         tw.Element("created_at").Value.Split(' ')
                         select new TwitterStructs.Tweet
                         {
                             CreatedAt = DateTime.Parse(
                                 string.Format("{0} {1} {2} {3} GMT",
                                 dateParts[1],
                                 dateParts[2],
                                 dateParts[5],
                                 dateParts[3]),
                                 System.Globalization.CultureInfo.InvariantCulture),
                             Source = tw.Element("source").Value,
                             Text = tw.Element("text").Value,
                             InReplyToStatusID = tw.Element("in_reply_to_status_id").Value,
                             InReplyToUserID = tw.Element("in_reply_to_user_id").Value
                         };
            toReturn.Tweets = tweets.ToList();
            return toReturn;
        }

        public static TwitterStructs.User GetTwitterUser(string TwitterName)
        {
            var webClient = new System.Net.WebClient();
            string url = string.Format("http://twitter.com/users/show/{0}.xml", TwitterName);
            string resultXML = webClient.DownloadString(url);
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
                           TwitterHomeURL = string.Format("http://twitter.com/{0}", u.Element("screen_name").Value)
                       };
            return user.SingleOrDefault();
        }

    }
}

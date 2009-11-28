using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.IO;

namespace TwitterPluginForAtomSite
{
    public class TwitterInteraction
    {
        public static TwitterStructs.Twitter GetUpdates(
            int CacheDuration,
            string TwitterName,
            int Limit,
            int PagingIndex)
        {
            var cached = TwitterCacheManager.Get<TwitterStructs.Twitter>(
                TwitterStructs.TwitterConsts.TwitterCurrentTweets + TwitterName + PagingIndex.ToString(),
                CacheDuration);
            if (cached != null)
                return cached;
            var webClient = new System.Net.WebClient();
            string url = string.Format("http://twitter.com/status/user_timeline/{0}.xml?count={1}&page={2}",
                TwitterName,
                Limit,
                PagingIndex);
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
            var userElement = statusArray.Take(1).Descendants("user").SingleOrDefault();
            toReturn.User = GetTwitterUserFromUserElement(userElement);
            var tweets = from tw in statusArray
                         let replyToScreenName =
                         ElementValueSingleOrDefault(tw, "in_reply_to_screen_name")
                         select new TwitterStructs.Tweet
                         {
                             CreatedAt = GetDateTimeFromTwitterTime(ElementValueSingleOrDefault(tw, "created_at")),
                             Source = ElementValueSingleOrDefault(tw, "source"),
                             Text = AddMarkupToTweet(ElementValueSingleOrDefault(tw, "text")),
                             InReplyToScreenName = replyToScreenName,
                             InReplyToStatusURL = !string.IsNullOrEmpty(replyToScreenName) ? string.Format("http://twitter.com/{0}/status/{1}", replyToScreenName, ElementValueSingleOrDefault(tw, "in_reply_to_status_id")) : ""
                         };
            toReturn.Tweets = tweets.ToList();
            TwitterCacheManager.Set(toReturn,
                TwitterStructs.TwitterConsts.TwitterCurrentTweets + TwitterName + PagingIndex.ToString());
            return toReturn;
        }

        private static DateTime GetDateTimeFromTwitterTime(string Value)
        {
            string[] dateParts = Value.Split(' ');
            return DateTime.Parse(
                                string.Format("{0} {1} {2} {3} GMT",
                                dateParts[1],
                                dateParts[2],
                                dateParts[5],
                                dateParts[3]),
                                System.Globalization.CultureInfo.InvariantCulture);
        }

        private static string ElementValueSingleOrDefault(XElement element, string name)
        {
            return element.Element(name) != null && !string.IsNullOrEmpty(element.Element(name).Value) ? element.Element(name).Value : "";
        }

        private static TwitterStructs.User GetTwitterUserFromUserElement(XElement u)
        {
            return new TwitterStructs.User
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
                                   ProfileBackgroundColor = ElementValueSingleOrDefault(u, "profile_background_color"),
                                   ProfileBackgroundImageURL = ElementValueSingleOrDefault(u, "profile_background_image_url"),
                                   ProfileLinkColor = ElementValueSingleOrDefault(u, "profile_link_color"),
                                   ProfileSideBarBorderColor = ElementValueSingleOrDefault(u, "profile_sidebar_border_color"),
                                   ProfileSideBarFillColor = ElementValueSingleOrDefault(u, "profile_sidebar_fill_color"),
                                   ProfileTextColor = ElementValueSingleOrDefault(u, "profile_text_color"),
                                   HasBeenTweetingSince = GetDateTimeFromTwitterTime(ElementValueSingleOrDefault(u, "created_at"))
                               };
        }

        public static TwitterStructs.User GetTwitterUser(int CacheDuration, string TwitterName, string OptionalPassword)
        {
            var cached = TwitterCacheManager.Get<TwitterStructs.User>(
                TwitterStructs.TwitterConsts.TwitterUser + TwitterName,
                CacheDuration);
            if (cached != null)
                return cached;

            var webClient = new System.Net.WebClient();
            string url;
            if (!string.IsNullOrEmpty(OptionalPassword))
            {
                webClient.Credentials = new System.Net.NetworkCredential(TwitterName, OptionalPassword);
                url = string.Format("http://twitter.com/account/verify_credentials.xml");
            }
            else
            {
                url = string.Format("http://twitter.com/users/show/{0}.xml", TwitterName);
            }
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

            var toReturn = GetTwitterUserFromUserElement(result.Elements().SingleOrDefault());
            TwitterCacheManager.Set(toReturn, TwitterStructs.TwitterConsts.TwitterUser + TwitterName);
            return toReturn;
        }
        /*
        private const string TwitterJsonUrl = "http://twitter.com/statuses/update.json";
        private const string TwitterUser = "your_user";
        private const string TwitterPass = "your_pass";

        private static void SendTwitterMessage(string message)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(TwitterJsonUrl);

                string post = string.Empty;
                using (TextWriter writer = new StringWriter())
                {
                    writer.Write("status={0}", HttpUtility.UrlEncode(message));
                    post = writer.ToString();
                    Console.WriteLine("Post: {0}", post);
                }

                SetRequestParams(request);

                request.Credentials = new NetworkCredential(TwitterUser, TwitterPass);

                using (Stream requestStream = request.GetRequestStream())
                {
                    using (StreamWriter writer = new StreamWriter(requestStream))
                    {
                        writer.Write(post);
                    }
                }

                Console.WriteLine("Length: {0}", request.ContentLength);
                Console.WriteLine("Address: {0}", request.Address);

                WebResponse response = request.GetResponse();
                string content;

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        content = reader.ReadToEnd();
                    }
                }

                Console.WriteLine(content);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void SetRequestParams(HttpWebRequest request)
        {
            request.Timeout = 500000;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.Referer = "http://www.orionsbelt.eu";
            request.UserAgent = "Orion's Belt Notifier Bot";
#if USE_PROXY
    request.Proxy = new WebProxy("http://localhost:8080", false);
#endif
        }
        */

        /*
 * A function to post an update to Twitter programmatically
 * Author: Danny Battison
 * Contact: gabehabe@hotmail.com
 */

        /// <summary>
        /// Post an update to a Twitter acount
        /// </summary>
        /// <param name="username">The username of the account</param>
        /// <param name="password">The password of the account</param>
        /// <param name="tweet">The status to post</param>
        public static string PostTweet(string username, string password, string tweet)
        {
            try
            {
                // encode the username/password
                string user = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ":" + password));
                // determine what we want to upload as a status
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes("status=" + tweet);
                // connect with the update page
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://twitter.com/statuses/update.xml");
                // set the method to POST
                request.Method = "POST";
                request.ServicePoint.Expect100Continue = false; // thanks to argodev for this recent change!
                // set the authorisation levels
                request.Headers.Add("Authorization", "Basic " + user);
                request.ContentType = "application/x-www-form-urlencoded";
                // set the length of the content
                request.ContentLength = bytes.Length;

                // set up the stream
                Stream reqStream = request.GetRequestStream();
                // write to the stream
                reqStream.Write(bytes, 0, bytes.Length);
                // close the stream
                reqStream.Close();

                WebResponse response = request.GetResponse();
                string content;

                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        content = reader.ReadToEnd();
                    }
                }
                return XDocument.Parse(content).Element("status").Descendants("id").SingleOrDefault().Value;
            }
            catch
            {
                return null;
            }       
        }

        public static string Tweet(string ToTweet, string TwitterName, string Password)
        {

            string shortenedTweet = ShortenURLsForTweet(ToTweet);

            return PostTweet(TwitterName, Password, shortenedTweet);

            var webClient = new System.Net.WebClient();
            string url;
            webClient.Credentials = new System.Net.NetworkCredential(TwitterName, Password);
            url = string.Format("http://twitter.com/statuses/update.xml");
            
            var nameValues = new System.Collections.Specialized.NameValueCollection();
            nameValues.Add("status", shortenedTweet);

            string resultXML = null;

            try
            {
                resultXML = webClient.UploadString(url, string.Format("status={0}", shortenedTweet));
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

            string statusID = result.Element("status").Descendants("id").SingleOrDefault().Value;

            return string.Format("http://twitter.com/{0}/status/{1}", TwitterName, statusID);
        }

        private static string ShortenURLsForTweet(string ToTweet)
        {
            string linkRegex = @"((https|http):\/\/+[\w\d\.\-_\?\&\%\/\=\#\:]*)";
            DoForAllMatches(ToTweet, linkRegex, p =>
                {
                    ToTweet = ToTweet.Replace(p, ShortenURL(p));
                });
            return ToTweet;
        }

        private static string ShortenURL(string URL)
        {
            var webClient = new System.Net.WebClient();
            string url = string.Format("http://1url.com/?api=1url&u={0}", URL);
            string toReturn = string.Empty;
            try
            {
                toReturn = webClient.DownloadString(url);
            }
            catch
            {
                return null;
            }
            return toReturn;
        }

        public static string AddMarkupToTweet(string Tweet)
        {
            string tagRegex = @"(\#)\w+[\w]";
            string mentionRegex = @"(\@)\w+[\w]";
            string linkRegex = @"((https|http):\/\/+[\w\d\.\-_\?\&\%\/\=\#\:]*)";

            DoForAllMatches(Tweet, linkRegex, p =>
            {
                Tweet = Tweet.Replace(p, string.Format("<span class=\"TwitterStatusLink\"><a href=\"{0}\">{0}</a></span>", p));
            });
            DoForAllMatches(Tweet, tagRegex, p =>
            {
                Tweet = Tweet.Replace(p, string.Format("<span class=\"TwitterStatusTag\"><a href=\"http://twitter.com/search?q=%23{0}\">{1}</a></span>", p.Substring(1, p.Length - 1), p));
            });
            DoForAllMatches(Tweet, mentionRegex, p =>
            {
                Tweet = Tweet.Replace(p, string.Format("<span class=\"TwitterStatusMention\">@<a href=\"http://twitter.com/{0}\">{0}</a></span>", p.Substring(1, p.Length - 1)));
            });

            return Tweet;
        }

        private static void DoForAllMatches(string Input, string Regex, Action<string> ToDo)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(Regex);
            //There has got to be a better way to do this..
            foreach (System.Text.RegularExpressions.Match caught in reg.Matches(Input))
            {
                foreach (System.Text.RegularExpressions.Capture capture in caught.Captures)
                {
                    ToDo(capture.Value);
                }
            }
        }
    }


}

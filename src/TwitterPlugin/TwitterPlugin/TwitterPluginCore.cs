using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TwitterPluginForAtomSite
{
    public class TwitterPluginCore
    {
        public static TwitterStructs.Settings GetCurrent(System.Web.Caching.Cache cache)
        {
            //Change if..
            if (cache != null && cache[TwitterStructs.TwitterCurrentSettings] != null)
                return (TwitterStructs.Settings)cache[TwitterStructs.TwitterCurrentSettings];
            var xml = GetTwitterConfigXMLDoc();
            if (xml == null ||
                xml.Root.Name == null ||
                xml.Root.Name != TwitterStructs.TwitterConfigRootElement ||
                xml.Descendants(TwitterStructs.TwitterConfigUserElement).SingleOrDefault() == null ||
                string.IsNullOrEmpty(xml.Descendants(TwitterStructs.TwitterConfigUserElement).SingleOrDefault().Value))
                return null;

            int? limit = null;
            int temp = -1;
            if (xml.Descendants(TwitterStructs.TwitterConfigLimitElement).SingleOrDefault() != null &&
                !string.IsNullOrEmpty(xml.Descendants(TwitterStructs.TwitterConfigLimitElement).SingleOrDefault().Value) &&
                int.TryParse(xml.Descendants(TwitterStructs.TwitterConfigLimitElement).SingleOrDefault().Value, out temp) &&
                temp > 0)
                limit = 0;


            var toReturn = new TwitterStructs.Settings
            {
                UserName = xml.Descendants(TwitterStructs.TwitterConfigUserElement).SingleOrDefault().Value,
                Limit = limit
            };
            if (cache != null)
                cache[TwitterStructs.TwitterCurrentSettings] = toReturn;
            return toReturn;
        }

        private static XDocument GetTwitterConfigXMLDoc()
        {
            string path = GetTwitterConfigPath();
            if (!System.IO.File.Exists(path))
                return null;
            else
                return XDocument.Load(path);
        }

        private static string GetTwitterConfigPath()
        {
            return System.IO.Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, TwitterStructs.TwitterConfigFileName);
        }


        public static TwitterStructs.Settings UpdateAndReturnCurrent(System.Web.Caching.Cache cache, string id, int? limit)
        {
            if (TwitterInteraction.GetTwitterUser(cache, id) == null)
                return new TwitterStructs.Settings { Limit = null, UserName = null };
            var doc = new XDocument(new XElement(TwitterStructs.TwitterConfigRootElement,
                new XElement(TwitterStructs.TwitterConfigUserElement, id),
                new XElement(TwitterStructs.TwitterConfigLimitElement, limit)
                ));
            doc.Save(GetTwitterConfigPath());
            var toReturn = new TwitterStructs.Settings { Limit = limit, UserName = id };
            if (cache != null)
                cache[TwitterStructs.TwitterCurrentSettings] = toReturn;

            return toReturn;
        }

        public static TwitterStructs.Twitter GetUpdates(System.Web.Caching.Cache cache)
        {
            var current = GetCurrent(cache);
            if (current == null)
                return null;
            if (current.Limit.HasValue)
                return TwitterInteraction.GetUpdates(cache, current.UserName, current.Limit.Value);
            else
                return TwitterInteraction.GetUpdates(cache, current.UserName);
        }
    }
}

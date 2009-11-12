using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TwitterPluginForAtomSite
{
    public class TwitterPluginCore
    {
        public static TwitterStructs.Settings GetCurrent()
        {
            var xml = GetTwitterConfigXMLDoc();
            if (xml == null ||
                xml.Root.Name == null ||
                xml.Root.Name != TwitterStructs.TwitterConsts.TwitterConfigRootElement ||
                xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigUserElement).SingleOrDefault() == null ||
                string.IsNullOrEmpty(xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigUserElement).SingleOrDefault().Value))
                return null;

            int? limit = null;
            int temp = -1;
            if (xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigLimitElement).SingleOrDefault() != null &&
                !string.IsNullOrEmpty(xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigLimitElement).SingleOrDefault().Value) &&
                int.TryParse(xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigLimitElement).SingleOrDefault().Value, out temp) &&
                temp > 0)
                limit = temp;

            TimeSpan? cacheduration = null;
            TimeSpan tempTimespan = new TimeSpan(-15);
            if (xml.Descendants(TwitterStructs.TwitterConsts.TwitterCacheDurationElement).SingleOrDefault() != null &&
            !string.IsNullOrEmpty(xml.Descendants(TwitterStructs.TwitterConsts.TwitterCacheDurationElement).SingleOrDefault().Value) &&
            TimeSpan.TryParse(xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigLimitElement).SingleOrDefault().Value, out tempTimespan) &&
            tempTimespan.Ticks != -15)
                cacheduration = tempTimespan;

            var toReturn = new TwitterStructs.Settings
            {
                UserName = xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigUserElement).SingleOrDefault().Value,
                Limit = limit,
                CacheDuration = cacheduration
            };

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
            return System.IO.Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, TwitterStructs.TwitterConsts.TwitterConfigFileName);
        }


        public static TwitterStructs.Settings UpdateAndReturnCurrent(
            string id,
            int? limit,
            TimeSpan? CacheDuration,
            System.Web.Caching.Cache Cache)
        {
            TwitterCacheManager.Delete(TwitterStructs.TwitterConsts.TwitterCachePrefix, Cache);
            if (TwitterInteraction.GetTwitterUser(null, new TimeSpan(), id) == null)
                return new TwitterStructs.Settings();
            var doc = new XDocument(new XElement(TwitterStructs.TwitterConsts.TwitterConfigRootElement,
                new XElement(TwitterStructs.TwitterConsts.TwitterConfigUserElement, id),
                new XElement(TwitterStructs.TwitterConsts.TwitterConfigLimitElement, limit),
                new XElement(TwitterStructs.TwitterConsts.TwitterCacheDurationElement, CacheDuration)
                ));
            doc.Save(GetTwitterConfigPath());
            var toReturn = new TwitterStructs.Settings { Limit = limit, UserName = id };

            return toReturn;
        }

        public static TwitterStructs.Twitter GetUpdates(System.Web.Caching.Cache Cache)
        {
            var Settings = GetCurrent();
            if (Settings == null)
                return null;
            return TwitterInteraction.GetUpdates(
                Cache,
                Settings.CacheDuration.HasValue ? Settings.CacheDuration.Value : TwitterStructs.TwitterConsts.TwitterDefaultCacheDuration,
                Settings.UserName,
                Settings.Limit.HasValue ? Settings.Limit.Value : TwitterStructs.TwitterConsts.TwitterDefaultLimit);
        }
    }
}

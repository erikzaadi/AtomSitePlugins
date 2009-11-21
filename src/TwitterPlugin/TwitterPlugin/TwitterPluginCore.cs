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

            int? limit = GetIntFromElement(xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigLimitElement).SingleOrDefault());
            int? cacheduration = GetIntFromElement(xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigCacheDurationElement).SingleOrDefault());
            int? clientrefresh = GetIntFromElement(xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigClientRefreshElement).SingleOrDefault());


            var toReturn = new TwitterStructs.Settings
            {
                UserName = xml.Descendants(TwitterStructs.TwitterConsts.TwitterConfigUserElement).SingleOrDefault().Value,
                Limit = limit,
                CacheDuration = cacheduration,
                ClientRefreshDuration = clientrefresh
            };

            return toReturn;
        }

        private static int? GetIntFromElement(XElement element)
        {
            int? toReturn = null;
            int temp = -1;
            if (element != null &&
                !string.IsNullOrEmpty(element.Value) &&
                int.TryParse(element.Value, out temp) &&
                temp > 0)
                toReturn = temp;
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
            int? CacheDuration,
            int? ClientRefreshDuration)
        {
            TwitterCacheManager.Delete(TwitterStructs.TwitterConsts.TwitterCachePrefix);
            if (TwitterInteraction.GetTwitterUser(0, id) == null)
                return new TwitterStructs.Settings();
            var doc = new XDocument(new XElement(TwitterStructs.TwitterConsts.TwitterConfigRootElement,
                new XElement(TwitterStructs.TwitterConsts.TwitterConfigUserElement, id),
                new XElement(TwitterStructs.TwitterConsts.TwitterConfigLimitElement, limit),
                new XElement(TwitterStructs.TwitterConsts.TwitterConfigCacheDurationElement, CacheDuration),
                new XElement(TwitterStructs.TwitterConsts.TwitterConfigClientRefreshElement, ClientRefreshDuration)
                ));
            doc.Save(GetTwitterConfigPath());
            var toReturn = new TwitterStructs.Settings { Limit = limit, UserName = id };

            return toReturn;
        }

        public static TwitterStructs.Twitter GetUpdates()
        {
            return GetUpdates(1);
        }

        public static TwitterStructs.Twitter GetUpdates(int? PagingIndex)
        {
            return PagingIndex.HasValue ? GetUpdates(PagingIndex.Value) : GetUpdates();
        }
        public static TwitterStructs.Twitter GetUpdates(int PagingIndex)
        {
            var Settings = GetCurrent();
            if (Settings == null)
                return null;
            var toReturn = TwitterInteraction.GetUpdates(
                Settings.CacheDuration.HasValue ? Settings.CacheDuration.Value : TwitterStructs.TwitterConsts.TwitterDefaultCacheDuration,
                Settings.UserName,
                Settings.Limit.HasValue ? Settings.Limit.Value : TwitterStructs.TwitterConsts.TwitterDefaultLimit,
                PagingIndex);
            toReturn.Settings = Settings;
            toReturn.PagingIndex = ++PagingIndex;
            return toReturn;
        }
    }
}

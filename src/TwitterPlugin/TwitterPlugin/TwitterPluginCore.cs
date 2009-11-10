using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TwitterPluginForAtomSite
{
    public class TwitterPluginCore
    {
        public const string TwitterConfigFileName = "Twitter.config";
        public const string TwitterConfigRootElement = "TwitterSettings";
        public const string TwitterConfigUserElement = "Username";
        public const string TwitterConfigLimitElement = "Limit";

        public static TwitterStructs.Settings GetCurrent()
        {
            var xml = GetTwitterConfigXMLDoc();
            if (xml == null ||
                xml.Root.Name == null ||
                xml.Root.Name != TwitterConfigRootElement ||
                xml.Descendants(TwitterConfigUserElement).SingleOrDefault() == null ||
                string.IsNullOrEmpty(xml.Descendants(TwitterConfigUserElement).SingleOrDefault().Value))
                return null;

            int? limit = null;
            int temp = -1;
            if (xml.Descendants(TwitterConfigLimitElement).SingleOrDefault() != null &&
                !string.IsNullOrEmpty(xml.Descendants(TwitterConfigLimitElement).SingleOrDefault().Value) &&
                int.TryParse(xml.Descendants(TwitterConfigLimitElement).SingleOrDefault().Value, out temp) &&
                temp > 0)
                limit = 0;


            return new TwitterStructs.Settings
            {
                UserName = xml.Descendants(TwitterConfigUserElement).SingleOrDefault().Value,
                Limit = limit
            };
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
            return System.IO.Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, TwitterConfigFileName);
        }


        public static TwitterStructs.Settings UpdateAndReturnCurrent(string id, int? limit)
        {
            var doc = new XDocument(new XElement(TwitterConfigRootElement,
                new XElement(TwitterConfigUserElement, id),
                new XElement(TwitterConfigLimitElement, limit)
                ));
            doc.Save(GetTwitterConfigPath());
            return new TwitterStructs.Settings { Limit = limit, UserName = id };
        }

        public static TwitterStructs.Twitter GetUpdates()
        {
            var current = GetCurrent();
            if (current == null)
                return null;
            if (current.Limit.HasValue)
                return TwitterInteraction.GetUpdates(current.UserName, current.Limit.Value);
            else
                return TwitterInteraction.GetUpdates(current.UserName);
        }
    }
}

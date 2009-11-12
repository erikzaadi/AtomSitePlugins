using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GA4AtomSite
{
    public class GA4AtomSiteUtils
    {
        private static System.Web.Caching.Cache Cache
        {
            get
            {
                var current = System.Web.HttpContext.Current;
                if (current == null)
                    return null;
                else
                    return current.Cache;
            }
        }
        private const string GAConfigFileName = "GA4AtomSite.config";
        private const string GAConfigROOT = "GA4AtomSite";
        private const string GAConfigGAElement = "GAID";
        private const string GAConfigCacheName = "GA4AtomSite";
        public static string CurrentGoogleAnalyticsID
        {
            get
            {
                return _getGAID();
            }
            set
            {
                _setGAID(value);
            }
        }

        private static void _setGAID(string value)
        {
            if (Cache != null)
                Cache.Remove(GAConfigCacheName);
            
            var doc = new XDocument(new XElement(GAConfigROOT,
               new XElement(GAConfigGAElement, value)
               ));

            doc.Save(GetGAConfigPath());
        }

        private static string _getGAID()
        {
            var cached = GetGAIDFromCache();
            if (string.IsNullOrEmpty(cached))
                cached = GetGAIDFromXML();
            return cached;
        }

        private static string GetGAIDFromXML()
        {
            var xml = GetGAConfigXMLDoc();
            if (xml == null ||
                xml.Root.Name == null ||
                xml.Root.Name != GAConfigROOT ||
                xml.Descendants(GAConfigGAElement).SingleOrDefault() == null ||
                string.IsNullOrEmpty(xml.Descendants(GAConfigGAElement).SingleOrDefault().Value))
                return null;
            else
                return xml.Descendants(GAConfigGAElement).SingleOrDefault().Value;
        }

        private static string GetGAIDFromCache()
        {
            if (Cache != null && Cache[GAConfigCacheName] != null)
                return Cache[GAConfigCacheName].ToString();
            else
                return null;
        }

        private static XDocument GetGAConfigXMLDoc()
        {
            string path = GetGAConfigPath();
            if (!System.IO.File.Exists(path))
                return null;
            else
                return XDocument.Load(path);
        }

        private static string GetGAConfigPath()
        {
            return System.IO.Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, GAConfigFileName);
        }
    }
}

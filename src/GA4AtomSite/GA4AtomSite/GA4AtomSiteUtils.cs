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

        private static IEnumerable<XElement> GetGAIDsFromXML()
        {
            var xml = GetGAConfigXMLDoc();
            if (xml == null ||
                xml.Root.Name == null ||
                xml.Root.Name != GAConfigROOT ||
                xml.Descendants(GAConfigGAElement).Count() == 0)
                return null;
            else
                return xml.Descendants(GAConfigGAElement);
        }

        private static string GetGAIDFromXML(string CollectionID)
        {
            var GAIDS = GetGAIDsFromXML();
            if (GAIDS == null)
                return null;
            else
                return GAIDS.Where(p => p.Attribute("CollectionID").Value == CollectionID).Select(p => p.Value).SingleOrDefault();
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

        internal static string GetGAIDByCollectionID(string CollectionID)
        {
            return GetGAIDFromXML(CollectionID);
        }

        internal static void SetGAIDForCollectionID(string GAID, string CollectionID)
        {
            var xml = GetGAConfigXMLDoc();
            if (xml == null ||
                xml.Root.Name == null ||
                xml.Root.Name != GAConfigROOT)
                return;

            var gaids = xml.Descendants(GAConfigGAElement);
            if (gaids == null || gaids.Where(p => p.Attribute("CollectionID").Value == CollectionID).Count() == 0)
            {
                var toAdd = new XElement(GAConfigGAElement, GAID);
                toAdd.SetAttributeValue("CollectionID", CollectionID);
                xml.Add(toAdd);
            }
            else
            {
                gaids.Where(p => p.Attribute("CollectionID").Value == CollectionID).Single().Value = GAID;
            }
        }

        internal static System.Collections.Specialized.NameValueCollection GetGAIDsCollection()
        {
            System.Collections.Specialized.NameValueCollection toReturn = new System.Collections.Specialized.NameValueCollection();
            var gaids = GetGAIDsFromXML();
            if (gaids == null || gaids.Count() == 0)
                return toReturn;
            else 
                gaids.ToList().ForEach(p => toReturn.Add(p.Attribute("CollectionID").Value, p.Value));
            return toReturn;
        }

        internal static System.Collections.Specialized.NameValueCollection GetGAIDsCollection(IEnumerable<AtomSite.Domain.AppCollection> Collections)
        {
            System.Collections.Specialized.NameValueCollection toReturn = new System.Collections.Specialized.NameValueCollection();
            foreach (var collection in Collections)
            {
                toReturn.Add(collection.Id.ToFullWebId(), GetGAIDFromXML(collection.Id.ToFullWebId()));
            }
            return toReturn;
        }
    }
}

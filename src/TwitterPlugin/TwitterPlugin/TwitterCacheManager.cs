using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPluginForAtomSite
{
    public class TwitterCacheManager
    {
        private static System.Web.Caching.Cache cache
        {
            get
            {
                return System.Web.HttpContext.Current != null ? System.Web.HttpContext.Current.Cache : null;
            }
        }
        public static ObjType Get<ObjType>(string Name, int CacheDuration) where ObjType : class
        {
            if (cache == null)
                return null;
            var cached = cache[Name] as TwitterStructs.TwitterCacheObject;
            if (cached == null)
                return null;
            var now = DateTime.Now;
            if (now.Subtract(cached.When).Minutes > CacheDuration)
                return null;
            else
                return cached.Cached as ObjType;
        }

        public static void Set<ObjType>(ObjType ToCache, string Name) where ObjType : class
        {
            if (cache == null)
                return;
            var cacheObj = new TwitterStructs.TwitterCacheObject { Cached = ToCache, When = DateTime.Now };
            cache[Name] = cacheObj;
        }

        public static void Delete(string StartingWith)
        {
            if (cache == null)
                return;
            var enumerator = cache.GetEnumerator();
           
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().StartsWith(StartingWith))
                    cache.Remove(enumerator.Key.ToString());
            }
        }
    }
}

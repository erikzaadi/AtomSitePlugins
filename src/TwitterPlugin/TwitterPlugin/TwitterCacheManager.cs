using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPluginForAtomSite
{
    public class TwitterCacheManager
    {
        public static ObjType Get<ObjType>(string Name, System.Web.Caching.Cache cache, TimeSpan CacheDuration) where ObjType : class
        {
            if (cache == null)
                return null;
            var cached = cache[Name] as TwitterStructs.TwitterCacheObject;
            if (cached == null)
                return null;
            var now = DateTime.Now;
            if (now.Subtract(cached.When) > CacheDuration)
                return null;
            else
                return cached.Cached as ObjType;
        }

        public static void Set<ObjType>(ObjType ToCache, string Name, System.Web.Caching.Cache cache) where ObjType : class
        {
            if (cache == null)
                return;
            var cacheObj = new TwitterStructs.TwitterCacheObject { Cached = ToCache, When = DateTime.Now };
            cache[Name] = cacheObj;
        }

        public static void Delete(string StartingWith, System.Web.Caching.Cache cache)
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

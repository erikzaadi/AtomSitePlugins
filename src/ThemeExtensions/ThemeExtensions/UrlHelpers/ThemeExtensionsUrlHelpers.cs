using System.Web.Mvc;

namespace ThemeExtensions.UrlHelpers
{
    public static class ThemeExtensionsUrlHelpersExposer
    {
        public static ThemeExtensions ThemeExtensions(this UrlHelper helper)
        {
            return new ThemeExtensions(helper);
        }
    }

    public class ThemeExtensions : ThemeExtensionsUrlHelperBase
    {
        public ThemeExtensions(UrlHelper helper) : base(helper) { }

        public Social Social { get { return new Social(Helper); } }
        
        public Entries Entries { get { return new Entries(Helper); } }
    }
}

using System.Web.Mvc;

namespace ThemeExtensions.HtmlHelpers
{
    public static class ThemeExtensionsHtmlHelpersExposer
    {
        public static ThemeExtensions ThemeExtensions(this HtmlHelper helper)
        {
            return new ThemeExtensions(helper);
        }
    }

    public class ThemeExtensions : ThemeExtensionsHtmlHelperBase
    {
        public ThemeExtensions(HtmlHelper helper) : base(helper) { }

        public Social Social { get { return new Social(Helper); } }

        public Date Date { get { return new Date(Helper); } }

        public Theme Theme { get { return new Theme(Helper); } }

        public Entries Entries { get { return new Entries(Helper); } }
    }
}

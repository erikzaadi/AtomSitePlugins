using System.Web.Mvc;

namespace ThemeExtensions.HtmlHelpers
{
    public static class ThemeExtensionsHtmlHelpers
    {
        public static Theme Theme(this HtmlHelper helper)
        {
            return new Theme(helper);
        }

        public static Entries Entries(this HtmlHelper helper)
        {
            return new Entries(helper);
        }

        public static Date Date(this HtmlHelper helper)
        {
            return new Date(helper);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using AtomSite.Domain;
using AtomSite.WebCore;

namespace ThemeExtensions
{
    public static class HtmlHelpers
    {
        private static IThemeService GetThemeService(HtmlHelper helper)
        {
            return ThemeService.GetCurrent(helper.ViewContext.RequestContext);
        }

        private static UrlHelper GetUrlHelper(HtmlHelper helper)
        {
            return new UrlHelper(helper.ViewContext.RequestContext);
        }

        private static TType GetThemeProperty<TType>(Theme theme, string propertyName, TType defaultValue) where TType : class
        {
            return theme.GetValue<TType>(XName.Get(propertyName, Atom.ThemeNs.NamespaceName)) ?? defaultValue;
        }

        private static bool GetThemeBooleanProperty(Theme theme, string propertyName, bool defaultValue) 
        {
            return theme.GetBooleanWithDefault(XName.Get(propertyName, Atom.ThemeNs.NamespaceName),defaultValue);
        }

        private static Theme GetTheme(HtmlHelper helper)
        {
            var themeName = ThemeViewEngine.GetCurrentThemeName(helper.ViewContext.RequestContext);
            return GetThemeService(helper).GetTheme(themeName);
        }

        public static void IfThemeProperty<TType>(this HtmlHelper helper, string propertyName, Action<TType> doWithProperty) where TType : class
        {
            IfThemeProperty(helper, propertyName, doWithProperty, null);
        }

        public static void IfThemeProperty<TType>(this HtmlHelper helper, string propertyName, Action<TType> doWithProperty, TType defaultValue) where TType : class
        {
            var theme = GetTheme(helper);

            var result = GetThemeProperty(theme, propertyName, defaultValue);

            doWithProperty(result);
        }

        public static string IfThemeProperty<TType>(this HtmlHelper helper, string propertyName, Func<TType, string> doWithProperty) where TType : class
        {
            return IfThemeProperty(helper, propertyName, doWithProperty, null);
        }

        public static string IfThemeProperty<TType>(this HtmlHelper helper, string propertyName, Func<TType, string> doWithProperty, TType defaultValue) where TType : class
        {
            var toReturn = "";
            IfThemeProperty(helper, propertyName, (result) => toReturn = doWithProperty(result), defaultValue);

            return toReturn;
        }

        public static TType GetThemeProperty<TType>(this HtmlHelper helper, string propertyName) where TType : class
        {
            return GetThemeProperty<TType>(helper, propertyName, null);
        }

        public static TType GetThemeProperty<TType>(this HtmlHelper helper, string propertyName, TType defaultValue) where TType : class
        {
            return GetThemeProperty(GetTheme(helper), propertyName, defaultValue);
        }

        public static bool GetThemeBooleanProperty(this HtmlHelper helper, string propertyName) 
        {
            return GetThemeBooleanProperty(helper, propertyName, false);
        }

        public static bool GetThemeBooleanProperty(this HtmlHelper helper, string propertyName, bool defaultValue) 
        {
            return GetThemeBooleanProperty(GetTheme(helper), propertyName, defaultValue);
        }

        public static IEnumerable<AtomEntry> GetTrackBacks(this HtmlHelper helper, FeedModel feedModel)
        {
            return feedModel.Feed.Entries != null && feedModel.Feed.Entries.Any()
                                 ? feedModel.Feed.Entries.Where(x => (x.AnnotationType ?? string.Empty).EndsWith("back"))
                                 : new List<AtomEntry>();
        }

        public static IEnumerable<AtomEntry> GetCommentsWithoutTrackBacks(this HtmlHelper helper, FeedModel feedModel)
        {
            return feedModel.Feed.Entries != null && feedModel.Feed.Entries.Any()
              ? feedModel.Feed.Entries.Where(x => !(x.AnnotationType ?? string.Empty).EndsWith("back"))
              : new List<AtomEntry>();
        }

        public static string GetAvatarUrl(this HtmlHelper helper, string email)
        {
            return GetAvatarUrl(helper, email, null);
        }

        public static string GetAvatarUrl(this HtmlHelper helper, string email, int? size)
        {
            var url = GetUrlHelper(helper);
            return url.GetGravatarHref(email, size.HasValue ? size.Value : 48)
                   + helper.ViewContext.RequestContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority)
                   + url.ImageSrc("noav.png");
        }

        public static string GetCurrentFeed(this HtmlHelper helper, PageModel pageModel)
        {
            return (pageModel != null && pageModel.Collection != null && pageModel.Collection.Id != null)
                       ? GetUrlHelper(helper).RouteIdUrl("AtomPubFeed", pageModel.Collection.Id)
                       : null;
        }

        public static string GetCurrentCommentsFeed(this HtmlHelper helper, PageModel pageModel)
        {
            return (pageModel != null && pageModel.Collection != null && pageModel.Collection.Id != null)
                       ? GetUrlHelper(helper).RouteIdUrl("AnnotateAnnotationsFeed", pageModel.Collection.Id)
                       : null;
        }
    }
}

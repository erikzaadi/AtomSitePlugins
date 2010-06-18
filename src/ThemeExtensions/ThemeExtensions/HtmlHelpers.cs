using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using AtomSite.Domain;
using AtomSite.Repository;
using AtomSite.WebCore;
using StructureMap;

namespace ThemeExtensions
{
    public static class HtmlHelpers
    {
        private static AppService _GetService(HtmlHelper helper)
        {
            IContainer container = (IContainer)helper.ViewContext.HttpContext.Application["Container"];
            return container.GetInstance<IAppServiceRepository>().GetService();
        }

        private static IThemeService _GetThemeService(HtmlHelper helper)
        {
            return ThemeService.GetCurrent(helper.ViewContext.RequestContext);
        }

        private static UrlHelper _GetUrlHelper(HtmlHelper helper)
        {
            return new UrlHelper(helper.ViewContext.RequestContext);
        }
    
        private static Theme _GetTheme(HtmlHelper helper)
        {
            var themeName = ThemeViewEngine.GetCurrentThemeName(helper.ViewContext.RequestContext);
            return _GetThemeService(helper).GetTheme(themeName);
        }

        public static bool GetThemeBooleanProperty(this HtmlHelper helper, string propertyName, bool defaultValue)
        {
            //traverse scope first
            var service = _GetService(helper);
            IRouteService r = RouteService.GetCurrent(helper.ViewContext.RequestContext);
            var scope = r.GetScope();
            var pages = service.GetServicePages(scope, "", "");

            var propertyValue = pages.Select(p => p.GetBooleanProperty(XName.Get(propertyName, Atom.ThemeNs.NamespaceName))).Where(a => a.HasValue).LastOrDefault();
            if (propertyValue.HasValue)
                return propertyValue.Value;

            var theme = _GetTheme(helper);
            return theme.GetBooleanWithDefault(XName.Get(propertyName, Atom.ThemeNs.NamespaceName), defaultValue);
        }

        public static TType GetThemeProperty<TType>(HtmlHelper helper, string propertyName, TType defaultValue) where TType : class
        {
            //traverse scope first
            var service = _GetService(helper);
            IRouteService r = RouteService.GetCurrent(helper.ViewContext.RequestContext);
            var scope = r.GetScope();
            var pages = service.GetServicePages(scope, "", "");

            var propertyValue = pages.Select(p => p.GetProperty<TType>(XName.Get(propertyName, Atom.ThemeNs.NamespaceName))).Where(a => a != null).LastOrDefault();
            if (propertyValue != null)
                return propertyValue;

            var theme = _GetTheme(helper);
            return theme.GetValue<TType>(XName.Get(propertyName, Atom.ThemeNs.NamespaceName)) ?? defaultValue;
        }

        public static TType GetThemeProperty<TType>(this HtmlHelper helper, string propertyName) where TType : class
        {
            return GetThemeProperty<TType>(helper, propertyName, null);
        }

        public static bool GetThemeBooleanProperty(this HtmlHelper helper, string propertyName)
        {
            return GetThemeBooleanProperty(helper, propertyName, false);
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
            var url = _GetUrlHelper(helper);
            return url.GetGravatarHref(email, size.HasValue ? size.Value : 48)
                   + helper.ViewContext.RequestContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority)
                   + url.ImageSrc("noav.png");
        }

        public static string GetCurrentFeed(this HtmlHelper helper, PageModel pageModel)
        {
            return (pageModel != null && pageModel.Collection != null && pageModel.Collection.Id != null)
                       ? _GetUrlHelper(helper).RouteIdUrl("AtomPubFeed", pageModel.Collection.Id)
                       : null;
        }

        public static string GetCurrentCommentsFeed(this HtmlHelper helper, PageModel pageModel)
        {
            return (pageModel != null && pageModel.Collection != null && pageModel.Collection.Id != null)
                       ? _GetUrlHelper(helper).RouteIdUrl("AnnotateAnnotationsFeed", pageModel.Collection.Id)
                       : null;
        }
    }
}

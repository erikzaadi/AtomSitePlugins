using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using AtomSite.Domain;
using AtomSite.Repository;
using AtomSite.WebCore;
using StructureMap;

namespace ThemeExtensions.HtmlHelpers
{
    public class Theme : ThemeExtensionsHtmlHelperBase
    {
        public Theme(HtmlHelper helper)
            : base(helper)
        {
            var container = (IContainer)Helper.ViewContext.HttpContext.Application["Container"];
            AppService = container.GetInstance<IAppServiceRepository>().GetService();

            ThemeService = AtomSite.WebCore.ThemeService.GetCurrent(Helper.ViewContext.RequestContext);

            Scope = RouteService.GetCurrent(Helper.ViewContext.RequestContext).GetScope();
        }

        private AppService AppService { get; set; }

        private IThemeService ThemeService { get; set; }

        private Scope Scope { get; set; }

        private AtomSite.Domain.Theme _GetTheme()
        {
            var themeName = ThemeViewEngine.GetCurrentThemeName(Helper.ViewContext.RequestContext);
            return ThemeService.GetTheme(themeName);
        }

        public bool GetThemeBooleanProperty(string propertyName, bool defaultValue)
        {
            //traverse scope first
            var pages = AppService.GetServicePages(Scope, "", "");

            var propertyValue = pages.Select(p => p.GetBooleanProperty(XName.Get(propertyName, Atom.ThemeNs.NamespaceName))).Where(a => a.HasValue).LastOrDefault();
            if (propertyValue.HasValue)
                return propertyValue.Value;

            var theme = _GetTheme();
            return theme.GetBooleanWithDefault(XName.Get(propertyName, Atom.ThemeNs.NamespaceName), defaultValue);
        }

        public TType GetThemeProperty<TType>(string propertyName, TType defaultValue) where TType : class
        {
            //traverse scope first
            var pages = AppService.GetServicePages(Scope, "", "");

            var propertyValue = pages.Select(p => p.GetProperty<TType>(XName.Get(propertyName, Atom.ThemeNs.NamespaceName))).Where(a => a != null).LastOrDefault();
            if (propertyValue != null)
                return propertyValue;

            var theme = _GetTheme();
            return theme.GetValue<TType>(XName.Get(propertyName, Atom.ThemeNs.NamespaceName)) ?? defaultValue;
        }

        public TType GetThemeProperty<TType>(string propertyName) where TType : class
        {
            return GetThemeProperty<TType>(propertyName, null);
        }

        public bool GetThemeBooleanProperty(string propertyName)
        {
            return GetThemeBooleanProperty(propertyName, false);
        }

    }
}

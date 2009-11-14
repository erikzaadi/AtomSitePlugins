using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

namespace GA4AtomSite
{
    public class GA4AtomSitePlugin : AtomSite.WebCore.BasePlugin
    {
        public GA4AtomSitePlugin(ILogService logger)
            : base(logger)
        {
        }

        public override void Register(StructureMap.IContainer container, List<AtomSite.WebCore.SiteRoute> routes, System.Web.Mvc.ViewEngineCollection viewEngines, System.Web.Mvc.ModelBinderDictionary modelBinders, ICollection<AtomSite.Domain.Asset> globalAssets)
        {
            RegisterWidget<Widgets.Ga4AtomSiteWidget>(container);
            RegisterWidget<Widgets.Ga4AtomSiteAdminWidget>(container);
            RegisterController<GA4AtomSiteController>(container);
        }

        public override AtomSite.Domain.PluginState Setup(StructureMap.IContainer container, string appPath)
        {
            base.SetupIncludeInPageArea(container, "BlogListing", "sidemid", "Ga4AtomSiteWidget");
            base.SetupIncludeInPageArea(container, "BlogEntry", "content", "Ga4AtomSiteWidget");
            base.SetupIncludeInPageArea(container, "BlogHome", "content", "Ga4AtomSiteWidget");
            base.SetupIncludeInPageArea(container, "AdminSettingsEntireSite", "settingsLeft", "Ga4AtomSiteAdminWidget");

            return base.Setup(container, appPath);
        }
    }
}

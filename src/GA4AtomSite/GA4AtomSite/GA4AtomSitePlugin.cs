using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AtomSite.Domain;
using AtomSite.WebCore;
using StructureMap;

namespace GA4AtomSite
{
    public class GA4AtomSitePlugin : AtomSite.WebCore.BasePlugin
    {
        public GA4AtomSitePlugin(ILogService logger)
            : base(logger)
        {
            DefaultMerit = (int)MeritLevel.Default;
            CanUninstall = true;
        }

        public override void Register(IContainer container, List<SiteRoute> routes, ViewEngineCollection viewEngines, ModelBinderDictionary modelBinders, ICollection<Asset> globalAssets)
        {
            RegisterWidget(container, new CompositeWidget("GA4AtomSiteWidget", "GA4AtomSite", "Widget")
            {
                Description = "This widget adds Google Analytics tracking code to the page, that is capable of tracking javascript disabled browsers as well.",
                SupportedScopes = SupportedScopes.All,
                OnGetConfigInclude = (p) => new ConfigLinkInclude(p, "GA4AtomSite", "Config"),
                OnValidate = (i) =>
                {
                    var gaInclude = new GA4AtomSiteInclude(i);
                    return !string.IsNullOrEmpty(gaInclude.GoogleAccountID);
                },
                AreaHints = new[] { "foot", "tail" }
            });
            RegisterController<GA4AtomSiteController>(container);
        }

        public override PluginState Setup(IContainer container, string appPath)
        {
            LogService.Info("Setting up Google Analytics Plugin");

            LogService.Info("Finished Setting up Google Analytics Plugin");

            return base.Setup(container, appPath);
        }

        public override PluginState Uninstall(IContainer container, string appPath)
        {
            Plugin plugin = GetEmbeddedPluginEntry();
            base.UninstallInclude(container, (i) => i.Name == "GA4AtomSiteWidget");
            UninstallPluginFiles(container, plugin, appPath);
            return base.Uninstall(container, appPath);
        }
    }
}

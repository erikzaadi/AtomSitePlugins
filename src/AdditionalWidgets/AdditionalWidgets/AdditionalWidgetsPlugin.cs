using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using AtomSite.Domain;
using StructureMap;

namespace AdditionalWidgets
{
    public class AdditionalWidgetsPlugin : BasePlugin
    {
        public AdditionalWidgetsPlugin(ILogService logger)
            : base(logger)
        {
            DefaultMerit = (int)MeritLevel.Default;
            CanUninstall = true;
        }
        public override void Register(StructureMap.IContainer container, List<SiteRoute> routes, System.Web.Mvc.ViewEngineCollection viewEngines, System.Web.Mvc.ModelBinderDictionary modelBinders, ICollection<AtomSite.Domain.Asset> globalAssets)
        {
            RegisterWidget(container, new CompositeWidget("RecentPostsForCategoryWidget", "AdditionalWidgets", "RecentPostsForCategoryWidget")
            {
                Description = "This widget shows the recent posts for a category.",
                SupportedScopes = SupportedScopes.All,
                OnGetConfigInclude = (s) =>
                {
                    return new ConfigLinkInclude()
                  {
                      Controller = "AdditionalWidgets",
                      Action = "RecentPostsForCategorySetupWidget",
                      IncludePath = s
                  };
                },
                OnValidate = (i) => new AdditionalWidgetsIncludes.RecentPostsForCategoryInclude(i).HasCategory,
                AreaHints = new[] { "sidetop", "sidemid", "sidebot" }
            });

            RegisterController<AdditionalWidgetsController>(container);
        }


        public override PluginState Setup(IContainer container, string appPath)
        {
            LogService.Info("Setting up Additional Widgets Plugin");

            LogService.Info("Finished Setting up Additional Widgets Plugin");

            return base.Setup(container, appPath);
        }

        public override PluginState Uninstall(IContainer container, string appPath)
        {
            Plugin plugin = GetEmbeddedPluginEntry();
            base.UninstallInclude(container, (i) => i.Name == Atom.SvcNs + "RecentPostsForCategoryInclude");
            UninstallPluginFiles(container, plugin, appPath);
            return base.Uninstall(container, appPath);
        }
    }
}

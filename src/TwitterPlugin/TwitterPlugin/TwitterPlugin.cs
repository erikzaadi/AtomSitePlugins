using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using System.Xml.Linq;

namespace TwitterPluginForAtomSite
{
    public class TwitterPlugin : AtomSite.WebCore.BasePlugin
    {
        public TwitterPlugin(ILogService logger)
            : base(logger)
        {
        }
        public override void Register(StructureMap.IContainer container, List<AtomSite.WebCore.SiteRoute> routes, System.Web.Mvc.ViewEngineCollection viewEngines, System.Web.Mvc.ModelBinderDictionary modelBinders, ICollection<AtomSite.Domain.Asset> globalAssets)
        {
            RegisterWidget<TwitterWidget>(container);
            RegisterWidget<TwitterSetupWidget>(container);
            RegisterController<TwitterController>(container);
        }

        public override AtomSite.Domain.PluginState Setup(StructureMap.IContainer container, string appPath)
        {
            LogService.Info("Setting up Twitter Plugin");

            base.SetupIncludeInPageArea(container, "BlogHome", "sidemid", "TwitterWidget");
            base.SetupIncludeInPageArea(container, "AdminSettingsEntireSite", "settingsLeft", "TwitterSetupWidget");

            LogService.Info("Finished Setting up Twitter Plugin");

            return base.Setup(container, appPath);
        }
    }
}

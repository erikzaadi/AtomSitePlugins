using System.Collections.Generic;
using System.Web.Mvc;
using AtomSite.Domain;
using AtomSite.WebCore;
using StructureMap;

namespace WLWWorkaround
{
    public class WLWWorkaroundPlugin : BasePlugin
    {
        public WLWWorkaroundPlugin(ILogService logger)
            : base(logger)
        {
            DefaultMerit = (int)MeritLevel.Default;
            CanUninstall = true;
        }

        public override void Register(IContainer container, List<SiteRoute> routes, ViewEngineCollection viewEngines, ModelBinderDictionary modelBinders, ICollection<Asset> globalAssets)
        {
            /*
             <svc:include  name="LiteralWidget">
    &lt;div class="widget settings area"&gt;
        &lt;h3&gt;Windows Live Writer Workaround&lt;/h3&gt;
        &lt;a href="/WLWWorkaround/WLWWorkaround"&gt;Activate/Disable&lt;/a&gt;
    &lt;/div&gt;
</svc:include>
             */

            var wlw = new WLWService();
            container.Inject(typeof(IWLWService), wlw);
            container.Inject(typeof(IAuthenticateService), new WLWWorkaroundAuthenticateService(wlw));

            RegisterController<WLWWorkaroundController>(container);
            RegisterViewWidget(container, "AdminWLWWorkaround.aspx");
        }

        public override PluginState Setup(IContainer container, string appPath)
        {
            LogService.Info("Setting up Windows Live Writer Workaround Plugin");

            LogService.Info("Finished Setting up Windows Live Writer WorkaroundPlugin");

            return base.Setup(container, appPath);
        }

        public override PluginState Uninstall(IContainer container, string appPath)
        {
            Plugin plugin = GetEmbeddedPluginEntry();
            UninstallPluginFiles(container, plugin, appPath);
            return base.Uninstall(container, appPath);
        }
    }
}


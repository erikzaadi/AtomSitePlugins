using System.Collections.Generic;
using System.Web.Mvc;
using AtomSite.Domain;
using AtomSite.WebCore;
using StructureMap;
using System;
using System.Web.Routing;

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

            container.Inject(typeof(IAuthenticateService), new WLWWorkaroundAuthenticateService());

            RegisterController<WLWWorkaroundController>(container);
            RegisterViewWidget(container, "AdminWLWWorkaround.aspx");

            routes.Insert(0, new SiteRoute()
             {
                 Name = "WLWWorkaround",
                 Route = new Route("WLWWorkaround/{Action}",
                   new RouteValueDictionary(new { controller = "WLWWorkaround", action = "Index" }), new MvcRouteHandler()),
                 Merit = (int)MeritLevel.Max
             });
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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using System.Web.Routing;

namespace SubDomainToCollectionRedirector
{
    public class SubDomainToCollectionRedirectorPlugin : BasePlugin
    {
        public SubDomainToCollectionRedirectorPlugin(ILogService logger) : base(logger) { }
        public override void Register(StructureMap.IContainer container, List<SiteRoute> routes, System.Web.Mvc.ViewEngineCollection viewEngines, System.Web.Mvc.ModelBinderDictionary modelBinders, ICollection<AtomSite.Domain.Asset> globalAssets)
        {
            base.RegisterController<SubDomainToCollectionRedirector.SubDomainToCollectionRedirectorPluginController>(container);
            routes.AddRoute(new SiteRoute
            {
                Name = "SubDomainRedirectorShort",
                Merit = 0,
                Route = new Route(
                "SubDomainRedirector/{SubDomain}/{*Path}",                           // URL with parameters
               new RouteValueDictionary(new { controller = "SubDomainToCollectionRedirectorPlugin", action = "FindCorrect", SubDomain = "", Path = "" }) // Parameter defaults
                , new System.Web.Mvc.MvcRouteHandler())
            });
        }
    }
}

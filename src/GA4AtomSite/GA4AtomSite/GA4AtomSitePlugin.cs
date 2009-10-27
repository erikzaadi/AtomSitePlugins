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
            RegisterWidget<Ga4AtomSiteWidget>(container);
        }
    }
}

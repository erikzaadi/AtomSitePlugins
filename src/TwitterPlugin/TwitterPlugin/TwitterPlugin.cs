using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

namespace TwitterPlugin
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
        }
    }
}

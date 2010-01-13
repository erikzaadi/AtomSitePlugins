using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

namespace CommentNotifier
{
    public class CommentNotifierPlugin : BasePlugin
    {
       
        public override void Register(StructureMap.IContainer container, List<SiteRoute> routes, System.Web.Mvc.ViewEngineCollection viewEngines, System.Web.Mvc.ModelBinderDictionary modelBinders, ICollection<AtomSite.Domain.Asset> globalAssets)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TwitterPluginForAtomSite.Widgets
{
    public class TwitterSetupWidget : AtomSite.WebCore.ViewWidget
    {
        public TwitterSetupWidget()
            : base("TwitterSetupWidget")
        {
            AddAsset("TwitterPlugin.css", "admin");
            AddAsset("TwitterPlugin.js", "admin");
            TailScript = "TwitterPlugin.InitTwitterSetup();";
        }

        public override void Render(System.Web.Mvc.ViewContext ctx, AtomSite.Domain.Include include)
        {
            HtmlHelper helper = new HtmlHelper(ctx, new ViewDataContainer() { ViewData = ctx.ViewData });
            System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(helper, Name, new Models.SetupModel { TwitterSettings = TwitterPluginCore.GetCurrent() });
        }

              class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}

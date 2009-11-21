using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TwitterPluginForAtomSite.Widgets
{
    public class TwitterWidget : AtomSite.WebCore.ViewWidget
    {
        public TwitterWidget()
            : base("TwitterWidget")
        {
            AddAsset("TwitterPlugin.css", "site");
            AddAsset("TwitterPlugin.js", "site");
            TailScript = "TwitterPlugin.InitTwitterClient();";
        }

        public override void Render(System.Web.Mvc.ViewContext ctx, AtomSite.Domain.Include include)
        { 
            var _Settings = TwitterPluginCore.GetCurrent();
           
            HtmlHelper helper = new HtmlHelper(ctx, new ViewDataContainer() { ViewData = ctx.ViewData });

            var model = new Models.ClientModel { TwitterResponse = TwitterPluginCore.GetUpdates() };
            System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(helper, Name, model);
        }
        class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}

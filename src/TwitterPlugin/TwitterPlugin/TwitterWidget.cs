﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace TwitterPluginForAtomSite
{
    public class TwitterWidget : AtomSite.WebCore.ViewWidget
    {
        public TwitterWidget()
            : base("TwitterWidget")
        {
            AddAsset("TwitterPlugin.css", "site");
            AddAsset("TwitterPlugin.js", "site");
            TailScript = "InitTwitterClient();";
        }

        public override void Render(System.Web.Mvc.ViewContext ctx, AtomSite.Domain.Include include)
        {
            HtmlHelper helper = new HtmlHelper(ctx, new ViewDataContainer() { ViewData = ctx.ViewData });
            System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(helper, Name, new Models.ClientModel { TwitterResponse = TwitterPluginCore.GetUpdates(ctx.HttpContext.Cache) });
        }
        class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using System.Web.Mvc;

namespace GA4AtomSite.Widgets
{
    public class Ga4AtomSiteWidget : AtomSite.WebCore.BaseWidget
    {
        public Ga4AtomSiteWidget()
            : base("Ga4AtomSiteWidget")
        {
        }

        public override void Render(System.Web.Mvc.ViewContext ctx, AtomSite.Domain.Include include)
        {
            HtmlHelper helper = new HtmlHelper(ctx, new ViewDataContainer() { ViewData = ctx.ViewData });
            System.Web.Mvc.Html.RenderPartialExtensions.RenderPartial(helper, Name, new Models.GA4AtomSiteModel(GA4AtomSiteUtils.CurrentGoogleAnalyticsID));
        }
        class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}

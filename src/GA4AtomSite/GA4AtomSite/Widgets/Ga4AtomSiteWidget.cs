using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

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
            string GoogleAnalyticsID = GA4AtomSiteUtils.CurrentGoogleAnalyticsID;
            if (string.IsNullOrEmpty(GoogleAnalyticsID))
            {
                return;
            }
            else
                ctx.HttpContext.Response.Write(GA.NET.Core.Engine.GetGoogleAnalytics(GoogleAnalyticsID,
                    ctx.HttpContext.Request.Url.Host,
                    ctx.HttpContext.Request.UrlReferrer != null ? ctx.HttpContext.Request.UrlReferrer.ToString() : "",
                    ctx.HttpContext.Request.Url.PathAndQuery,
                    ""));

        }
    }
}

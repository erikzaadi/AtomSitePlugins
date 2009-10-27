using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

namespace GA4AtomSite
{
    public class Ga4AtomSiteWidget : AtomSite.WebCore.BaseWidget
    {
        ILogService LogService { get; set; }
        public Ga4AtomSiteWidget()
            : base("Ga4AtomSiteWidget")
        {
        }

        public override bool IsEnabled(BaseModel baseModel, AtomSite.Domain.Include include)
        {
            LogService = baseModel.Logger;
            return base.IsEnabled(baseModel, include);
        }

        public override void UpdatePageModel(PageModel pageModel, AtomSite.Domain.Include include)
        {
        }

        public override void Render(System.Web.Mvc.ViewContext ctx, AtomSite.Domain.Include include)
        {
            string GoogleAnalyticsID = include.Xml.Attributes().Where(a => a.Name == "GoogleAnalyticsID").Select(p => p.Value).SingleOrDefault();
            if (string.IsNullOrEmpty(GoogleAnalyticsID))
            {
                //Log message?
                if (LogService != null)
                    LogService.Error("GoogleAnalyticsID missing (Ga4AtomSiteWidget)");
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

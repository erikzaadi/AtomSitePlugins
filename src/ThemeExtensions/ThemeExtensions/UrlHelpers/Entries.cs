using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AtomSite.WebCore;

namespace ThemeExtensions.UrlHelpers
{
    public class Entries : ThemeExtensionsUrlHelperBase
    {
        public Entries(UrlHelper helper) : base(helper) { }
       
        public string GetCurrentFeed(PageModel pageModel)
        {
            return (pageModel != null && pageModel.Collection != null && pageModel.Collection.Id != null)
                       ? Helper.RouteIdUrl("AtomPubFeed", pageModel.Collection.Id)
                       : null;
        }

        public string GetCurrentCommentsFeed(PageModel pageModel)
        {
            return (pageModel != null && pageModel.Collection != null && pageModel.Collection.Id != null)
                       ? Helper.RouteIdUrl("AnnotateAnnotationsFeed", pageModel.Collection.Id)
                       : null;
        }
    }
}

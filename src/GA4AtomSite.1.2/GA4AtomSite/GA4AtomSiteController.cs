using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AtomSite.WebCore;

namespace GA4AtomSite
{
    public class GA4AtomSiteController : System.Web.Mvc.Controller
    {
        [AcceptVerbs(HttpVerbs.Post)]
        [ScopeAuthorize]
        public ActionResult Set(string GAID)
        {
            GA4AtomSiteUtils.CurrentGoogleAnalyticsID = GAID;
            if (Request.IsAjaxRequest())
                return Json(new { success = !string.IsNullOrEmpty(GAID), GAID = GAID });
            else
                return RedirectToRoute(new { controller = "Admin" });
        }
    }
}

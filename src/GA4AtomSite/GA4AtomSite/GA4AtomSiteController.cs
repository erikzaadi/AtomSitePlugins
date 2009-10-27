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
        [ScopeAuthorize]
        public ActionResult Setup()
        {
            return View("GA4AtomSiteSetup", "Admin", new GA4AtomSiteAdminModel(GA4AtomSiteUtils.CurrentGoogleAnalyticsID));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ScopeAuthorize]
        public ActionResult Set(string GAID)
        {
            if (string.IsNullOrEmpty(GAID))
            {
                return View("GA4AtomSiteSetup", "Admin", new GA4AtomSiteAdminModel(""));
            }
            else
            {
                GA4AtomSiteUtils.CurrentGoogleAnalyticsID = GAID;
                return View("GA4AtomSiteDone", "Admin", new GA4AtomSiteAdminModel(GAID));
            }
        }
    }
}

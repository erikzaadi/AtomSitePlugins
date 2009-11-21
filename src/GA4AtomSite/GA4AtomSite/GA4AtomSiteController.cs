using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AtomSite.WebCore;

namespace GA4AtomSite
{
    public class GA4AtomSiteController : BaseController
    {
        [AcceptVerbs(HttpVerbs.Post)]
        [ScopeAuthorize]
        public ActionResult Set(string GAID, string CollectionID)
        {
            GA4AtomSiteUtils.SetGAIDForCollectionID(GAID, CollectionID);
            if (Request.IsAjaxRequest())
                return Json(new { success = !string.IsNullOrEmpty(GAID), GAID = GAID });
            else
                return RedirectToRoute(new { controller = "Admin" });
        }

        [ScopeAuthorize]
        public ActionResult GetAdmin()
        {
            return PartialView("Ga4AtomSiteAdminWidget", new Models.GA4AtomSiteAdminModel { GAID = GA4AtomSiteUtils.GetGAIDForCollection(Collection), CollectionID = Collection.Id.Workspace + Collection.Id.Collection });
        }

        public ActionResult Get()
        {
            return PartialView("Ga4AtomSiteWidget", new GA4AtomSite.Models.GA4AtomSiteModel(GA4AtomSiteUtils.GetGAIDByCollectionID(base.Collection.Id.Workspace + base.Collection.Id.Collection)));
        }
    }
}

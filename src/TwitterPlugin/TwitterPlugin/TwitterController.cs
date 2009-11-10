using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using System.Web.Mvc;

namespace TwitterPluginForAtomSite
{
    public class TwitterController : AtomSite.WebCore.BaseController
    {
        [ScopeAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Setup(string id, int? limit)
        {
            TwitterStructs.Settings current = TwitterPluginCore.UpdateAndReturnCurrent(id, limit);
            if (Request.IsAjaxRequest())
                return Json(current);
            else
                return RedirectToRoute(new { controller = "Admin" });
        }


        [ScopeAuthorize]
        public ActionResult Setup()
        {
            return PartialView("TwitterSetupWidget", new TwitterPluginForAtomSite.Models.SetupModel(TwitterPluginCore.GetCurrent()));
        }

        public ActionResult List()
        {
            return PartialView("TwitterWidget", new TwitterPluginForAtomSite.Models.ClientModel(TwitterPluginCore.GetUpdates()));
        }
    }
}

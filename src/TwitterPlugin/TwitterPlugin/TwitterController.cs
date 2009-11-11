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
            TwitterStructs.Settings current = TwitterPluginCore.UpdateAndReturnCurrent(HttpContext.Cache, id, limit);
            if (Request.IsAjaxRequest())
                return Json(current);
            else
                return RedirectToRoute(new { controller = "Admin" });
        }
    }
}

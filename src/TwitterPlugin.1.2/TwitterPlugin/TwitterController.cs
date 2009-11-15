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
        public ActionResult Setup(string id, int? limit, int? cachedurationinminutes)
        {
            TimeSpan? cacheduration = null;
            if (cachedurationinminutes.HasValue)
                cacheduration = new TimeSpan(0, cachedurationinminutes.Value, 0);
            TwitterStructs.Settings current = TwitterPluginCore.UpdateAndReturnCurrent(id, limit, cacheduration);
            if (Request.IsAjaxRequest())
                return Json(current);
            else
                return RedirectToRoute(new { controller = "Admin" });
        }
    }
}

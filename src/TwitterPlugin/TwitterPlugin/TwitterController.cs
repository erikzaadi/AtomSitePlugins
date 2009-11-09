using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using System.Web.Mvc;

namespace TwitterPlugin
{
    public class TwitterController : AtomSite.WebCore.BaseController
    {
        [ScopeAuthorize]
        public ActionResult Setup()
        {
            return View();
        }


        [ScopeAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Setup(string id)
        {
            return View(id);
        }


    }
}

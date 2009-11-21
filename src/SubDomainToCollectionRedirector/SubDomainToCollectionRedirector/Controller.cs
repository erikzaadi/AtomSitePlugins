using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using System.Web.Mvc;

namespace SubDomainToCollectionRedirector
{
    public class SubDomainToCollectionRedirectorPluginController : BaseController
    {
        public ActionResult FindCorrect(string SubDomain, string Path)
        {
            bool found = false;
            string basePath = base.AppService.Base.ToString();
            base.Workspace.Collections.ToList().ForEach(p =>
            {
                if (p.Visible && p.Href.AbsolutePath.ToLowerInvariant().Replace(".atom", "").StartsWith("/" + SubDomain.ToLowerInvariant()))
                    found = true;
            });


            return new RedirectResult(found ? string.Format("{0}{1}/{2}", basePath, SubDomain, Path) : basePath);
        }

    }
}

using System.Web.Mvc;
using AtomSite.Domain;
using AtomSite.WebCore;
using AtomSite.Repository;

namespace GA4AtomSite
{
    public class GA4AtomSiteController : BaseController
    {
        protected readonly IAppServiceRepository AppServiceRepository;

        public GA4AtomSiteController(IAppServiceRepository appServiceRepository)
        {
            AppServiceRepository = appServiceRepository;
        }

        [ScopeAuthorize(Action = AuthAction.UpdateServiceDoc, Roles = AuthRoles.AuthorOrAdmin)]
        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Config")]
        public ActionResult PostConfig(GA4AtomSiteConfigModel m)
        {
            if (string.IsNullOrEmpty(m.GoogleAccountID) || m.GoogleAccountID.Trim().Length == 0)
                ModelState.AddModelError("GoogleAccountID", "Please supply a Google Account ID to track.");

            if (ModelState.IsValid)
            {
                var appSvc = AppServiceRepository.GetService();
                var include = appSvc.GetInclude<GA4AtomSiteInclude>(m.IncludePath);
                include.GoogleAccountID = m.GoogleAccountID;
                AppServiceRepository.UpdateService(appSvc);
                return Json(new { success = true, includePath = m.IncludePath });
            }
            return PartialView("GA4AtomSiteConfig", m);
        }

        [ScopeAuthorize(Action = AuthAction.UpdateServiceDoc, Roles = AuthRoles.AuthorOrAdmin)]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Config(GA4AtomSiteConfigModel m)
        {
            if (m.IncludePath != null)
            {
                var include = AppService.GetInclude<GA4AtomSiteInclude>(m.IncludePath);
                m.GoogleAccountID = include.GoogleAccountID;
            }
            return PartialView("GA4AtomSiteConfig", m);
        }

        public ActionResult Widget(Include include)
        {
            var gaInclude = new GA4AtomSiteInclude(include);

            if (string.IsNullOrEmpty(gaInclude.GoogleAccountID))
                return new EmptyResult();

            return PartialView("GA4AtomSiteWidget", new GA4AtomSiteModel { GoogleAccountID = gaInclude.GoogleAccountID });
        }
    }
}

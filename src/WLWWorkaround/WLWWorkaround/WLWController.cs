using System.Web.Mvc;
using AtomSite.WebCore;

namespace WLWWorkaround
{
    public class WLWWorkaroundController : BaseController
    {
        public WLWWorkaroundController(IWLWService wLWService)
        {
            _WLWService = wLWService;
        }

        protected IWLWService _WLWService { get; set; }

        [ScopeAuthorize]
        public ActionResult WLWWorkaround()
        {
            return PartialView("AdminWLWWorkaround", new WLWWorkaroundModel { Active = _WLWService.Active });
        }

        [ScopeAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WLWWorkaround(string Username, string Password, int? ExpiresInMinutes)
        {
            var expires = ExpiresInMinutes.HasValue ? ExpiresInMinutes.Value : 20;
            var model = new WLWWorkaroundModel();

            if (_WLWService.Authenticate(Username, Password, expires))
            {
                model.Success = true;
                model.Message = string.Format("The user '{0}' will be active for {1} minutes.", Username, expires);
                model.Active = true;
            }
            else
            {
                model.Success = false;
                model.Message = "Failed to login..";
            }
            return PartialView("AdminWLWWorkaround", model);
        }

        [ScopeAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult WLWWorkaroundDisable()
        {
            var model = new WLWWorkaroundModel
            {
                Success = true
            };

            if (_WLWService.Disable())
            {
                model.Message = "The workaround has been disabled";
            }
            else
            {
                model.Message = "No workaround was active";
            }
            return PartialView("AdminWLWWorkaround", model);
        }
    }
}
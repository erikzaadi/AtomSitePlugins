using System.Web.Mvc;
using AtomSite.WebCore;
using System;

namespace WLWWorkaround
{
    public class WLWWorkaroundController : BaseController
    {
        [ScopeAuthorize(Roles = AtomSite.Domain.AuthRoles.Administrator)]
        public ActionResult Index()
        {
            return PartialView("AdminWLWWorkaround", new WLWWorkaroundModel { Active = WLWService.IsActive(HttpContext) });
        }

        [ScopeAuthorize(Roles = AtomSite.Domain.AuthRoles.Administrator)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Activate(string Username, string Password, int? ExpiresInMinutes)
        {
            var expires = ExpiresInMinutes.HasValue ? ExpiresInMinutes.Value : 20;
            var model = new WLWWorkaroundModel();

            if (WLWService.Authenticate(Username, Password, expires, HttpContext.Application["Container"] as StructureMap.IContainer, HttpContext))
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

        [ScopeAuthorize(Roles = AtomSite.Domain.AuthRoles.Administrator)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Disable()
        {
            var model = new WLWWorkaroundModel
            {
                Success = true
            };

            if (WLWService.IsActive(HttpContext))
            {
                WLWService.Disable(HttpContext);
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
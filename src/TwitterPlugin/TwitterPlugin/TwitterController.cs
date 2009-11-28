using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using System.Web.Mvc;
using AtomSite.Repository;

namespace TwitterPluginForAtomSite
{
    public class TwitterController : AtomSite.WebCore.BaseController
    {
        protected IAtomPubService AtomPubService { get; private set; }

        public TwitterController(IAtomPubService atompub, IAnnotateService annotate, IBlogService blog)
            : base()
        {
            AtomPubService = atompub;
        }


        [ScopeAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Setup(string id, int? limit, int? cachedurationinminutes, int? clientrefreshinminutes, string password)
        {
            TwitterStructs.Settings current = TwitterPluginCore.UpdateAndReturnCurrent(id, limit, cachedurationinminutes, clientrefreshinminutes, password == "*******" ? "" : password);
            if (Request.IsAjaxRequest())
                return Json(current);
            else
                return RedirectToRoute(new { controller = "Admin" });
        }

        public ActionResult Get(int? PagingIndex)
        {
            var response = TwitterPluginCore.GetUpdates(PagingIndex);
            if (Request.IsAjaxRequest())
                return PartialView("TwitterPartialWidget", response.Tweets);
            else
                return PartialView("TwitterWidget", new Models.ClientModel { TwitterResponse = response });
        }

        [ScopeAuthorize]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Publish(string statustemplate, string entryid)
        {
            var entry = AtomPubService.GetEntry(entryid);
            string error = "";
            if (!statustemplate.Contains(TwitterStructs.TwitterConsts.StatusTemplateTitleTag))
                error = "Title tag is missing in status template";
            if (!statustemplate.Contains(TwitterStructs.TwitterConsts.StatusTemplateURLTag))
                error = "Post URL Tag is missing in status template";

            if (!entry.Approved)
                error = "Please approve the post before tweeting about it";
            if (entry.Draft)
                error = "Can not tweet about a post that is still in draft";
            if (!string.IsNullOrEmpty(error))
                return Json(new { Success = false, Error = error });

            var settings = TwitterPluginCore.GetCurrent();
            var toTweet = statustemplate.Replace(TwitterStructs.TwitterConsts.StatusTemplateTitleTag, entry.Title.Text)
                .Replace(TwitterStructs.TwitterConsts.StatusTemplateURLTag, entry.LocationWeb.AbsoluteUri);
            var tweetUrl = TwitterInteraction.Tweet(toTweet, settings.UserName, settings.Password);
            if (tweetUrl == null)
                return Json(new { Success = false, Error = "Unable to Tweet about post.." });
            return Json(new { Success = true, TweetUrl = tweetUrl });
        }

        [ScopeAuthorize]
        public ActionResult Publish()
        {
            return PartialView("TwitterPublishWidget", new Models.PublishModel { TwitterSettings = TwitterPluginCore.GetCurrent(), BlogEntryID = this.EntryId });
        }
    }
}

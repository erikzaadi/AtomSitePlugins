using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AtomSite.Domain;
using AtomSite.WebCore;

namespace ThemeExtensions.UrlHelpers
{
    public class Social : ThemeExtensionsUrlHelperBase
    {
        public Social(UrlHelper helper) : base(helper) { }

        public string GetAvatarUrl(string email)
        {
            return GetAvatarUrl(email, null);
        }

        public string GetAvatarUrl(string email, int? size)
        {
            return Helper.GetGravatarHref(email, size.HasValue ? size.Value : 48)
                   + Helper.RequestContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority)
                   + Helper.ImageSrc("noav.png");
        }

        public string ShareEntryUrl(AtomEntry entry, SocialNetwork socialNetwork)
        {
            var entryUrl = Helper.RequestContext.HttpContext.Server.UrlEncode(Helper.RequestContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) +
                           Helper.RouteIdUrl("BlogEntry", entry.Id));
            var entryTitle = Helper.RequestContext.HttpContext.Server.UrlEncode(entry.Title.Text);
            var entryPreview = Helper.RequestContext.HttpContext.Server.UrlEncode(entry.Text.ToStringPreview(64));
            var entrySource = Helper.RequestContext.HttpContext.Server.UrlEncode(Helper.Content("~/"));//TODO: Change to home url according to settings?

            switch (socialNetwork)
            {
                case SocialNetwork.Twitter:
                    return string.Format("http://twitter.com/home?status={0}+-+{1}",
                                         entryTitle,
                                         entryUrl);
                case SocialNetwork.Digg:
                    return string.Format("http://digg.com/submit?phase=2&amp;url={0}&amp;title={1}",
                                         entryUrl,
                                         entryTitle);
                case SocialNetwork.Facebook:
                    return string.Format("http://www.facebook.com/share.php?u={0}&amp;t={1}",
                                         entryUrl,
                                         entryTitle);
                case SocialNetwork.Delicious:
                    return string.Format("http://del.icio.us/post?url={0}&amp;title={1}",
                                         entryUrl,
                                         entryTitle);
                case SocialNetwork.StumbleUpon:
                    return string.Format("http://www.stumbleupon.com/submit?url={0}&amp;title={1}",
                                         entryUrl,
                                         entryTitle);
                case SocialNetwork.GoogleBookmarks:
                    return string.Format("http://www.google.com/bookmarks/mark?op=add&amp;bkmk={0}&amp;title={1}",
                                         entryUrl,
                                         entryTitle);
                case SocialNetwork.LinkedIn:
                    return
                        string.Format(
                            "http://www.linkedin.com/shareArticle?mini=true&amp;url={0}&amp;title={1}&amp;summary={2}&amp;source={3}",
                            entryUrl,
                            entryTitle,
                            entryPreview,
                            entrySource);
                case SocialNetwork.YahooBuzz:
                    return string.Format("http://buzz.yahoo.com/buzz?targetUrl={0}&amp;headline={1}&amp;summary={2}",
                            entryUrl,
                            entryTitle,
                            entryPreview);
                case SocialNetwork.Techorati:
                    return string.Format("http://technorati.com/faves?add={0}",
                                         entryUrl);
                default:
                    return null;
            }
        }

        public enum SocialNetwork
        {
            Twitter,
            Digg,
            Facebook,
            Delicious,
            StumbleUpon,
            GoogleBookmarks,
            YahooBuzz,
            Techorati,
            LinkedIn
        }
    }
}

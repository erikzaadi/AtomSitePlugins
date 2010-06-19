using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AtomSite.Domain;
using AtomSite.WebCore;

namespace ThemeExtensions.HtmlHelpers
{
    public class Entries : ThemeExtensionsHtmlHelperBase
    {
        public Entries(HtmlHelper helper) : base(helper) { }

        public bool IsEntryTrackBack(AtomEntry entry)
        {
            return (entry.AnnotationType ?? string.Empty).EndsWith("back");
        }

        public IEnumerable<AtomEntry> GetTrackBacks(FeedModel feedModel)
        {
            return feedModel.Feed.Entries != null && feedModel.Feed.Entries.Any()
                                 ? feedModel.Feed.Entries.Where(IsEntryTrackBack)
                                 : new List<AtomEntry>();
        }

        public IEnumerable<AtomEntry> GetCommentsWithoutTrackBacks(FeedModel feedModel)
        {
            return feedModel.Feed.Entries != null && feedModel.Feed.Entries.Any()
              ? feedModel.Feed.Entries.Where(x => !IsEntryTrackBack(x))
              : new List<AtomEntry>();
        }

        public string GetNumberOfCommentsString(int? Total)
        {
            switch (Total)
            {
                case 0:
                case null:
                    return "No Comments";
                case 1:
                    return "1 Comment";
                default:
                    return string.Format("{0} Comments", Total);
            }

        }
    }
}

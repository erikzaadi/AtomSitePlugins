using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using AtomSite.Domain;
using System.Web.Mvc;
namespace AdditionalWidgets
{
    public class AdditionalWidgetsModels
    {
        public class RecentPostsForCategoryWidgetSetupConfigInputModel
        {
            public string Workspace { get; set; }
            public string Collection { get; set; }
            public string Category { get; set; }
            public int Count { get; set; }
            public string IncludePath { get; set; }
            public bool HasCategory
            {
                get
                {
                    return !string.IsNullOrEmpty(Workspace)
                        && !string.IsNullOrEmpty(Collection)
                        && !string.IsNullOrEmpty(Category);
                }

            }
        }

        public class RecentPostsForCategoryWidgetSetupConfigModel : RecentPostsForCategoryWidgetSetupConfigInputModel
        {
            public IEnumerable<SelectListItem> Collections { get; set; }
            public IEnumerable<CollectionsCategories> CollectionCategories { get; set; }
        }

        public class CollectionsCategories
        {
            public string Collection { get; set; }
            public IEnumerable<SelectListItem> Categories { get; set; }
        }

        public class RecentPostsForCategoryWidgetModel
        {
            public string Category { get; set; }
            public IEnumerable<AtomEntry> Entries { get; set; }
        }
    }
}

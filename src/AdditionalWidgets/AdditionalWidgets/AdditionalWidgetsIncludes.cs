using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.Domain;
using System.Xml.Linq;
using AtomSite.WebCore;

namespace AdditionalWidgets
{
    public class AdditionalWidgetsIncludes
    {
        public class RecentPostsForCategoryInclude : Include
        {
            public RecentPostsForCategoryInclude() : base(new XElement(Include.IncludeXName)) { }

            public RecentPostsForCategoryInclude(Include include)
                : base(include.Xml) { }

            public RecentPostsForCategoryInclude(string Collection, string Category, int Count)
                : base(new XElement(Atom.SvcNs + "RecentPostsForCategoryInclude"))
            {
                this.Collection = Collection;
                this.Category = Category;
                this.Count = Count;
            }

            public string Collection
            {
                get { return GetProperty<string>("collection"); }
                set { SetProperty<string>("collection", value); }
            }

            public string Category
            {
                get { return GetProperty<string>("category"); }
                set { SetProperty<string>("category", value); }
            }

            public int Count
            {
                get { return GetInt32PropertyWithDefault("count", 5); }
                set { SetInt32Property("count", value); }
            }

            public bool HasCategory
            {
                get
                {
                    return !string.IsNullOrEmpty(Collection)
                        && !string.IsNullOrEmpty(Category);
                }

            }
        }


    }
}

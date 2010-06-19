using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using AtomSite.Domain;
using AtomSite.Repository;
using AtomSite.WebCore;
using StructureMap;

namespace ThemeExtensions.UrlHelpers
{
    public static class ThemeExtensionsUrlHelpers
    {
        public static Social Social(this UrlHelper helper)
        {
            return new Social(helper);
        }

        public static Entries Entries(this UrlHelper helper)
        {
            return new Entries(helper);
        }
    }
}

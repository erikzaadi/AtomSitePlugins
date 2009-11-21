using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using System.Web.Mvc;

namespace GA4AtomSite.Widgets
{
    public class Ga4AtomSiteWidget : AtomSite.WebCore.CompositeWidget
    {
        public Ga4AtomSiteWidget()
            : base("Ga4AtomSiteWidget","Ga4AtomSite","Get")
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;
using System.Web.Mvc;

namespace GA4AtomSite.Widgets
{
    public class Ga4AtomSiteAdminWidget : AtomSite.WebCore.CompositeWidget
    {
        public Ga4AtomSiteAdminWidget()
            : base("Ga4AtomSiteAdminWidget", "Ga4AtomSite", "GetAdmin")
        {
            AddAsset("GA4AtomSite.css", "admin");
            AddAsset("GA4AtomSite.js", "admin");
            TailScript = "GA4AtomSite.InitGA4AtomSiteSetup();";
        }
       
    }
}

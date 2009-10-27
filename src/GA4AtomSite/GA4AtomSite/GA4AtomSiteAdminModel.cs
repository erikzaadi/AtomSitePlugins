using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

namespace GA4AtomSite
{
    public class GA4AtomSiteAdminModel : AdminModel
    {
        public GA4AtomSiteAdminModel(string GAID)
        {
            this.GAID = GAID;
        }
        public string GAID { get; set; }
    }
}

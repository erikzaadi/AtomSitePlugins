using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

namespace GA4AtomSite.Models
{
    public class GA4AtomSiteAdminModel : AdminModel
    {
        public string GAID { get; set; }
        public string CollectionID { get; set; }
    }
}

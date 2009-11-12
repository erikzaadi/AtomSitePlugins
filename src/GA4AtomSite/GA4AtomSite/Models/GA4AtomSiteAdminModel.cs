using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

namespace GA4AtomSite.Models
{
    public class GA4AtomSiteAdminModel : BaseModel
    {
        public GA4AtomSiteAdminModel(string GAID)
        {
            this.GAID = GAID;
        }
        public string GAID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

namespace GA4AtomSite.Models
{
    public class GA4AtomSiteModel : BaseModel
    {
        public GA4AtomSiteModel(string GAID)
        {
            this.GAID = GAID;
        }
        public string GAID { get; set; }
    }
}

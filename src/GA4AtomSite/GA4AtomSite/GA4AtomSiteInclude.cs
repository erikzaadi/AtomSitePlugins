using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AtomSite.Domain;

namespace GA4AtomSite
{
    class GA4AtomSiteInclude : Include
    {
        public GA4AtomSiteInclude() : base(new XElement(Include.IncludeXName)) { }

        public GA4AtomSiteInclude(Include include)
            : base(include.Xml) { }

        public GA4AtomSiteInclude(string googleAccountId)
            : base(new XElement(Atom.SvcNs + "twitter"))
        {
            this.GoogleAccountID = googleAccountId;
        }

        public string GoogleAccountID
        {
            get { return GetProperty<string>("GoogleAccountID"); }
            set { SetProperty<string>("GoogleAccountID", value); }
        }
    }
}

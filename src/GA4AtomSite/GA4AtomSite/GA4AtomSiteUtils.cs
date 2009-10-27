using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GA4AtomSite
{
    public class GA4AtomSiteUtils
    {
        public static string CurrentGoogleAnalyticsID
        {
            get
            {
                //Read from GA4AtomSiteSettings.config ..
                return "";
            }
            set
            {
                value.ToString();
            }
        }
    }
}

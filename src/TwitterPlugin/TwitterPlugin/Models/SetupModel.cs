using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPluginForAtomSite.Models
{
    public class SetupModel : AtomSite.WebCore.PageModel
    {
        public TwitterStructs.Settings TwitterSettings { get; set; }

        public SetupModel() { }
        public SetupModel(TwitterStructs.Settings TwitterSettings)
        {
            this.TwitterSettings = TwitterSettings;
        }   

    }
}

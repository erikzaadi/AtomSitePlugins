using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPluginForAtomSite.Models
{
    public class PublishModel : AtomSite.WebCore.AdminEntryModel
    {
        public TwitterStructs.Settings TwitterSettings { get; set; }

        public PublishModel() { }
        public PublishModel(TwitterStructs.Settings TwitterSettings)
        {
            this.TwitterSettings = TwitterSettings;
        }
    }
}

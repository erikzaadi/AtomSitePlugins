using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPluginForAtomSite.Models
{
    public class PublishModel
    {
        public TwitterStructs.Settings TwitterSettings { get; set; }
        public AtomSite.Domain.Id BlogEntryID { get; set; }
        public string BlogIDString { get { return BlogEntryID != null ? BlogEntryID.ToString() : ""; } }
        public bool IsEnabled { get { return TwitterSettings != null && !string.IsNullOrEmpty(TwitterSettings.Password); } }
        public string DefaultStatusTemplate { get { return TwitterStructs.TwitterConsts.DefaultStatusTemplate; } }
        public string TitleTag { get { return TwitterStructs.TwitterConsts.StatusTemplateTitleTag; } }
        public string PostUrlTag { get { return TwitterStructs.TwitterConsts.StatusTemplateURLTag; } }
        public PublishModel() { }
        public PublishModel(TwitterStructs.Settings TwitterSettings)
        {
            this.TwitterSettings = TwitterSettings;
        }
    }
}

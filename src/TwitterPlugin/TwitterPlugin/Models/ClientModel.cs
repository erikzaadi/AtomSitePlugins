using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPluginForAtomSite.Models
{
    public class ClientModel : AtomSite.WebCore.PageModel
    {
        public TwitterStructs.Twitter TwitterResponse { get; set; }

        public ClientModel() { }
        public ClientModel(TwitterStructs.Twitter TwitterResponse)
        {
            this.TwitterResponse = TwitterResponse;
        }   

    }
}

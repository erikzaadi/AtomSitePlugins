using System;
using AtomSite.WebCore;

namespace WLWWorkaround
{
    public class WLWWorkaroundModel : AdminModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public bool HasErrorMessage { get { return !string.IsNullOrEmpty(Message) && !Success; } }
        public bool HasSuccessMessage { get { return !string.IsNullOrEmpty(Message) && Success; } }
        public bool Active { get;set;}
    }

    public class WLWUser
    {
        public DateTime Expiration { get; set; }
        public AtomSite.Domain.User User { get; set; }
        public string UserHost { get; set; }
        public string UserAddress { get; set; }
    }
}

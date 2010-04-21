using AtomSite.WebCore;
using System.Web;
using System;

namespace WLWWorkaround
{
    public class WLWWorkaroundAuthenticateService : IAuthenticateService
    {
        #region IAuthenticateService Members

        public void Authenticate(ServerApp app)
        {
        }

        public void PostAuthenticate(ServerApp app)
        {
            WLWService.SetWLWUserIfAvailable(app.Context);
        }

        #endregion
    }

}

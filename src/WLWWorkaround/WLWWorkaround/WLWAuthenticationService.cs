using AtomSite.WebCore;

namespace WLWWorkaround
{
    public class WLWWorkaroundAuthenticateService : IAuthenticateService
    {
        public WLWWorkaroundAuthenticateService(IWLWService wLWService)
        {
            _WLWService = wLWService;
        }


        protected IWLWService _WLWService { get; set; }
        
        #region IAuthenticateService Members

        public void Authenticate(ServerApp app)
        {
        }

        public void PostAuthenticate(ServerApp app)
        {
            _WLWService.SetWLWUserIfAvailable(app);
        }

        #endregion
    }

}

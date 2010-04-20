using AtomSite.WebCore;

namespace WLWWorkaround
{
    public interface IWLWService 
    {
        bool Authenticate(string Username, string Password, int ExpiresInMinutes);

        void SetWLWUserIfAvailable(ServerApp app);

        bool Disable();

        bool Active{ get;}
    }
}


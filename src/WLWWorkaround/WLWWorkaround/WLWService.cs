using System;
using System.Web;
using AtomSite.WebCore;

namespace WLWWorkaround
{
    public class WLWService : IWLWService
    {
        public static string ApplicationKey { get { return "WLWIdentity"; } }

        protected HttpContext CurrentContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public StructureMap.IContainer Container
        {
            get { return CurrentContext.Application["Container"] as StructureMap.Container; }
        }

        /// <summary>
        /// Authenticates a user and enableds the Windows Live Writer workaround
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="ExpiresInMinutes">Number of minutes to keep the workaround alive</param>
        /// <returns>true if authenticated</returns>
        public bool Authenticate(string Username, string Password, int ExpiresInMinutes)
        {
            var userRepo = Container.GetInstance<AtomSite.Repository.IUserRepository>();

            var user = userRepo.GetUser(Username);
            if (user != null && user.CheckPassword(Password))
            {
                CurrentContext.Application[ApplicationKey] = new WLWUser
                {
                    User = user,
                    UserAddress = CurrentContext.Request.UserHostAddress,
                    UserHost = CurrentContext.Request.UserHostName,
                    Expiration = DateTime.Now.AddMinutes(ExpiresInMinutes)
                };
                return true;
            }
            return false;
        }


        public void SetWLWUserIfAvailable(ServerApp app)
        {
            if (app.Context.Request.UserAgent == "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Windows Live Writer 1.0)")
            {
                var obj = app.Context.Application[ApplicationKey] as WLWUser;
                if (obj != null
                    && obj.UserHost == app.Context.Request.UserHostName
                    && obj.UserAddress == app.Context.Request.UserHostAddress
                    && DateTime.Now < obj.Expiration)
                {
                    app.Context.User = new System.Security.Principal.GenericPrincipal(obj.User, new string[0]);
                    System.Threading.Thread.CurrentPrincipal = CurrentContext.User;
                }
            }
        }


        /// <summary>
        /// Disables the Windows Live Writer workaround
        /// </summary>
        /// <returns>true if the workaround was active</returns>
        public bool Disable()
        {
            var isActive = Active;
            if (isActive)
                CurrentContext.Application.Remove(ApplicationKey);
            return isActive;
        }

        public bool Active
        {
            get
            {
                var obj = CurrentContext.Application[ApplicationKey] as WLWUser;
                return obj != null;
            }
        }
    }
}


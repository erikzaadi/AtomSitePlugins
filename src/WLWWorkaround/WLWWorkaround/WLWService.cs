using System;
using System.Web;
using System.Web.Mvc;
using AtomSite.WebCore;
using StructureMap;

namespace WLWWorkaround
{
    public class WLWService
    {
        public static string ApplicationKey { get { return "WLWIdentity"; } }

        /// <summary>
        /// Authenticates a user and enableds the Windows Live Writer workaround
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="ExpiresInMinutes">Number of minutes to keep the workaround alive</param>
        /// <returns>true if authenticated</returns>
        public static bool Authenticate(
            string Username,
            string Password,
            int ExpiresInMinutes,
            IContainer Container,
            HttpContextBase CurrentContext)
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


        public static void SetWLWUserIfAvailable(
            HttpContext CurrentContext)
        {
            if (CurrentContext.Request.UserAgent == "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Windows Live Writer 1.0)")
            {
                var obj = CurrentContext.Application[ApplicationKey] as WLWUser;
                if (obj != null
                    && obj.UserHost == CurrentContext.Request.UserHostName
                    && obj.UserAddress == CurrentContext.Request.UserHostAddress
                    && DateTime.Now < obj.Expiration)
                {
                    CurrentContext.User = new System.Security.Principal.GenericPrincipal(obj.User, new string[0]);
                    System.Threading.Thread.CurrentPrincipal = CurrentContext.User;
                }
            }
        }


        /// <summary>
        /// Disables the Windows Live Writer workaround
        /// </summary>
        /// <returns>true if the workaround was active</returns>
        public static void Disable(HttpContextBase CurrentContext)
        {
            if (IsActive(CurrentContext))
                CurrentContext.Application.Remove(ApplicationKey);
        }

        public static bool IsActive(HttpContextBase CurrentContext)
        {
            var obj = CurrentContext.Application[ApplicationKey] as WLWUser;
            return obj != null;
        }

    }
}


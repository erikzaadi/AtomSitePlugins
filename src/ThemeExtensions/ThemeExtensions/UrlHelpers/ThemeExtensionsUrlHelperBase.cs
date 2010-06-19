using System;
using System.Web.Mvc;
using ThemeExtensions.Internal;

namespace ThemeExtensions.UrlHelpers
{
    public abstract class ThemeExtensionsUrlHelperBase : HideDefaultMethods
    {
        protected ThemeExtensionsUrlHelperBase(UrlHelper helper)
        {
            Helper = helper;
        }

        protected UrlHelper Helper { get; set; }
    }

}

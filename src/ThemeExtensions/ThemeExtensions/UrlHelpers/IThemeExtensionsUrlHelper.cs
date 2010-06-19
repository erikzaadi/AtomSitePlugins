using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ThemeExtensions.UrlHelpers
{
    public interface IThemeExtensionsUrlHelper
    {
        UrlHelper Helper { get; set; }
    }

    public abstract class ThemeExtensionsUrlHelperBase : IThemeExtensionsUrlHelper
    {
        protected ThemeExtensionsUrlHelperBase(UrlHelper helper)
        {
            this.Helper = helper;
        }
        #region IThemeExtensionsUrlHelper Members

        public UrlHelper Helper { get; set; }

        #endregion
    }

}

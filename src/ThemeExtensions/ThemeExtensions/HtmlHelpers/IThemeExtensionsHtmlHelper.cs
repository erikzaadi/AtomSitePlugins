using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ThemeExtensions.HtmlHelpers
{
    public interface IThemeExtensionsHtmlHelper
    {
        HtmlHelper Helper { get; set; }
    }

    public abstract class ThemeExtensionsHtmlHelperBase : IThemeExtensionsHtmlHelper
    {
        protected ThemeExtensionsHtmlHelperBase(HtmlHelper helper)
        {
            this.Helper = helper;
        }
        #region IThemeExtensionsHtmlHelper Members

        public HtmlHelper Helper { get; set; }

        #endregion
    }

}

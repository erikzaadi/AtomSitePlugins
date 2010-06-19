using System.Web.Mvc;
using ThemeExtensions.Internal;

namespace ThemeExtensions.HtmlHelpers
{
    public abstract class ThemeExtensionsHtmlHelperBase : HideDefaultMethods
    {
        protected ThemeExtensionsHtmlHelperBase(HtmlHelper helper)
        {
            Helper = helper;
        }
        
        protected HtmlHelper Helper { get; set; }
    }

}

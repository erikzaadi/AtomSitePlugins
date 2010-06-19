using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ThemeExtensions.HtmlHelpers
{
    public class Date : ThemeExtensionsHtmlHelperBase
    {
        public Date(HtmlHelper helper) : base(helper) { }

        public string GetDateNthFormat(DateTimeOffset dateTimeOffset)
        {
            var ending = string.Empty;

            if (dateTimeOffset.Day.ToString().EndsWith("1"))
            {
                ending = dateTimeOffset.Day.ToString().StartsWith("1") && dateTimeOffset.Day != 1 ? "th" : "st";
            }
            else if (dateTimeOffset.Day.ToString().EndsWith("2"))
            {
                ending = dateTimeOffset.Day.ToString().StartsWith("1") ? "th" : "nd";
            }
            else if (dateTimeOffset.Day.ToString().EndsWith("3"))
            {
                ending = dateTimeOffset.Day.ToString().StartsWith("1") ? "th" : "rd";
            }
            else
                ending = "th";
            return string.Format("{0} {1}{2}", dateTimeOffset.ToString("MMM", System.Globalization.DateTimeFormatInfo.InvariantInfo), dateTimeOffset.ToString("dd"), ending);
        }

    }
}

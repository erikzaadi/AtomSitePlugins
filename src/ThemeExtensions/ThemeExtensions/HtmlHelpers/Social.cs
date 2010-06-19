using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace ThemeExtensions.HtmlHelpers
{
    public class Social : ThemeExtensionsHtmlHelperBase
    {
        public Social(HtmlHelper helper) : base(helper) { }

        public string MakeTwitterContentClickable(string content)
        {
            return Regex.Replace(Regex.Replace(MakeClickable(content), @"\@(\w+)", "<a rel=\"nofollow\" href=\"http://twitter.com/$1\">@$1</a>"),
                @"\#(\w+)", "<a rel=\"nofollow\" href=\"http://twitter.com/#search?q=%23$1\">#$1</a>");
        }

        public string MakeClickable(string content)
        {
            var regx = new Regex("http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?",
                RegexOptions.IgnoreCase);

            var matches = regx.Matches(content);

            return matches.Cast<Match>().Aggregate(content, (current, match) => current.Replace(match.Value, "<a href='" + match.Value + "' rel=\"nofollow\">" + match.Value + "</a>"));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterPlugin
{
    public class TwitterWidget : AtomSite.WebCore.BaseWidget
    {
        public TwitterWidget()
            : base("TwitterWidget")
        {
        }

        public override void Render(System.Web.Mvc.ViewContext ctx, AtomSite.Domain.Include include)
        {
            string TwitterName = include.Xml.Attributes().Where(a => a.Name == "TwitterName").Select(p => p.Value).SingleOrDefault();
            string TwitterLimit = include.Xml.Attributes().Where(a => a.Name == "TwitterLimit").Select(p => p.Value).SingleOrDefault();
            if (string.IsNullOrEmpty(TwitterName))
                return;
            int limit = 5;
            if (!string.IsNullOrEmpty(TwitterLimit))
            {
                int result = -1;
                if (int.TryParse(TwitterLimit, out result) &&
                    result > 1)
                    limit = result;

            }
            var tweets = TwitterBridge.GetUpdates(TwitterName, limit);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id=\"TwitterWidget\">");
            tweets.ToList().ForEach(p => sb.AppendFormat("<div class=\"TwitterStatus\"><h4>User : '{0}'</h4><div>{1}</div></div>", p.User.Name, p.Text));
            sb.Append("</div>");
            ctx.HttpContext.Response.Write(sb.ToString());
        }
    }
}

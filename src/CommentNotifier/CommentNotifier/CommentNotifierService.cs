using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

namespace CommentNotifier
{
    public class CommentNotifierService : ICommentNotifierService
    {
        #region ICommentNotifierService Members

        public void Notify(AtomSite.Domain.Id entryId, AtomSite.Domain.AtomEntry entry, string slug)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

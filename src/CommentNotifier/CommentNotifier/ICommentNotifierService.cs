using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomSite.WebCore;

namespace CommentNotifier
{
    public interface ICommentNotifierService
    {
        void Notify(AtomSite.Domain.Id entryId, AtomSite.Domain.AtomEntry entry, string slug);
    }
    
}

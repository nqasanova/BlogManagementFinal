using System;
using BlogManagementFinal.Database.Models;
using BlogManagementFinal.Database.Repository.Common;

namespace BlogManagementFinal.Database.Repository
{
    class InboxRepository : Repository<Inbox, int>
    {
        public static Inbox Add(User receiver, string notification)
        {
            Inbox inbox = new Inbox(receiver, notification);
            DbContext.Add(inbox);
            return inbox;
        }
    }
}

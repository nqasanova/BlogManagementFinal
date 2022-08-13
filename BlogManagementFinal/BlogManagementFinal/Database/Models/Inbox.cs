using System;
using BlogManagementFinal.Database.Models.Common;

namespace BlogManagementFinal.Database.Models
{
    class Inbox : Entity<int>
    {
        public User Receiver { get; set; }
        public string Notification { get; set; }

        public Inbox(User receiver, string notification)
        {
            Receiver = receiver;
            Notification = notification;
        }
    }
}

using System;
using BlogManagementFinal.Database.Models.Common;
using BlogManagementFinal.Database.Repository;

namespace BlogManagementFinal.Database.Models
{
    public class Comment : Entity<int>
    {
        public Blog Blog { get; set; }
        public User Sender { get; set; }
        public string Content { get; set; }
        public DateTime CreatedTime { get; set; }

        public Comment(Blog blog, User sender, string content, int? id = null)
        {
            Blog = blog;
            Sender = sender;
            Content = content;
            CreatedTime = DateTime.Now;

            if (id != null)
            {
                Id = id.Value;
            }

            else
            {
                Id = UserRepository.IdCounter;
            }
        }

        public string GetCommentInfo()
        {
            return CreatedTime.ToString("mm.dd.yyyy") + " " + Sender.FirstName + " " + Sender.LastName + " " + Content;
        }
    }
}

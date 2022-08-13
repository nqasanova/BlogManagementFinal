using System;
using BlogManagementFinal.Database.Enums;
using BlogManagementFinal.Database.Models.Common;
using BlogManagementFinal.Database.Repository;

namespace BlogManagementFinal.Database.Models
{
    public class Blog : Entity<string>
    {
        public User Sender { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ID { get; set; }
        public BlogStatus Status { get; set; }
        public DateTime CreatedTime { get; set; }

        public Blog(User sender, string title, string content, BlogStatus status, string id = null)
        {
            Sender = sender;
            Title = title;
            Content = content;
            Status = status;
            CreatedTime = DateTime.Now;

            if (id != null)
            {
                Id = id;
            }

            else
            {
                Id = BlogRepository.RandomCode;
            }
        }

        public string GetBlogInfo()
        {
            return "Blog's Id : " + Id + " " + "Blog's Owner's First Name : " + Sender.FirstName + "Blog's Owner's Last Name : " + Sender.LastName + " " + "Blog's Title :" + Title + " " + "Blog's Content :" + Content + " " + "Blog Created At :" + CreatedAt + " " + "Blog's status : " + Status;
        }
    }
}

using System;
using BlogManagementFinal.Database.Models;

namespace BlogManagementFinal.Database.Repository
{
    public class CommentRepository : Common.Repository<Comment, int>
    {
        static CommentRepository()
        {
            BlogRepository blogRepository = new BlogRepository();

            DbContext.Add(new Comment(blogRepository.Get(b => b.Id == "BL64677"),
                UserRepository.GetUserByEmail("natavan@gmail.com"),
                "I hosnestly recomment this tutorial to everyone."));
            DbContext.Add(new Comment(blogRepository.Get(b => b.Id == "BL75692"),
                UserRepository.GetUserByEmail("natavan@gmail.com"),
                "Teacher, can you help me pls?"));
            DbContext.Add(new Comment(blogRepository.Get(b => b.Id == "BL46534"),
                UserRepository.GetUserByEmail("natavan@gmail.com"),
                "I want start java programming language."));
        }

        public static Comment Add(Blog blog, User sender, string text)
        {
            Comment comment = new Comment(blog, sender, text);
            DbContext.Add(comment);
            return comment;
        }
    }
}

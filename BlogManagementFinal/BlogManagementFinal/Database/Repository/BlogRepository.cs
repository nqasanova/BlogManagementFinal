using System;
using System.Collections.Generic;
using BlogManagementFinal.Database.Enums;
using BlogManagementFinal.Database.Models;

namespace BlogManagementFinal.Database.Repository
{
    public class BlogRepository : Common.Repository<Blog, string>
    {
        static Random randomID = new Random();

        private static string _code;

        public static string RandomCode
        {
            get
            {
                _code = "BL" + randomID.Next(0, 10000);

                while (GetByCode(_code) != null)
                {
                    _code = "Bl" + randomID.Next(0, 10000);
                }

                return _code;
            }
        }

        static BlogRepository()
        {
            DbContext.Add(new Blog(UserRepository.GetUserByEmail("natavan@gmail.com"), "How to learn programming",
                "Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the " +
                "industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and " +
                "scrambled it to make a type specimen book. It has survived not only five centuries",
                Enums.BlogStatus.Created, "BL21783"));
        }

        public static Blog UpdateBlog(string id, Blog blog)
        {
            BlogRepository blogRepository = new BlogRepository();
            Blog foundblog = blogRepository.GetById(id);

            foundblog.Title = blog.Title;
            foundblog.Content = blog.Content;

            return foundblog;
        }

        public static Blog Add(User sender, string title, string text)
        {
            Blog blog = new Blog(sender, title, text, BlogStatus.Created);
            DbContext.Add(blog);
            return blog;
        }

        public static Blog GetByCode(string code)
        {
            List<Blog> blogs = new List<Blog>();

            foreach (Blog blog in DbContext)
            {
                if (blog.ID == code)
                {
                    return blog;
                }
            }

            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using BlogManagementFinal.ApplicationLogic.Validations;
using BlogManagementFinal.Database.Enums;
using BlogManagementFinal.Database.Models;
using BlogManagementFinal.Database.Repository;

namespace BlogManagementFinal.ApplicationLogic.Services
{
    partial class BlogServices
    {
        private static BlogRepository blogRepository = new BlogRepository();
        private static CommentRepository commentRepository = new CommentRepository();
        private static InboxRepository inboxRepository = new InboxRepository();

        public static void ShowBlogs()
        {
            List<Blog> blogs = blogRepository.GetAll();
            int counter = 1;

            foreach (Blog blog in blogs)
            {
                if (blog.Status == BlogStatus.Approved)
                {
                    Console.WriteLine(counter + "." + blog.GetBlogInfo());
                    List<Comment> commentss = commentRepository.GetAll(c => c.Blog == blog);

                    for (int i = 0; i < commentss.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} {commentss[i].GetCommentInfo()}");
                    }
                }

                else
                {
                    Console.WriteLine("Aprroved blogs could not be found!");
                }

                counter++;
            }
        }

        public static string GetBlogTitle()
        {
            bool isExceptionValid;
            string title = null;

            do
            {
                try
                {
                    Console.Write("Enter title : ");
                    title = Console.ReadLine();

                    if (title == "null")
                    {
                        throw new Exception();
                    }

                    isExceptionValid = false;
                }

                catch (Exception)
                {

                    isExceptionValid = true;
                    Console.WriteLine("Something went wrong...");
                }

            } while (isExceptionValid || !Validation.IsLengthBetween(title, 10, 35));

            return title;
        }

        public static string GetBlogContent()
        {
            bool isExceptionValid;
            string content = null;

            do
            {
                try
                {
                    Console.Write("Enter content : ");
                    content = Console.ReadLine();

                    if (content == "null")
                    {
                        throw new Exception();
                    }

                    isExceptionValid = false;
                }

                catch (Exception)
                {

                    isExceptionValid = true;
                    Console.WriteLine("Something went wrong...");
                }

            } while (isExceptionValid || !Validation.IsLengthBetween(content, 20, 45));

            return content;
        }

        public static void FindBlogByCode()
        {
            Console.WriteLine("Enter code : ");
            string blogCode = Console.ReadLine();

            Blog foundBlog = blogRepository.GetById(blogCode);
            int counter = 1;

            if (foundBlog != null)
            {
                if (foundBlog.Status == BlogStatus.Approved)
                {
                    Console.WriteLine(foundBlog.GetBlogInfo());
                    foreach (Comment comment in commentRepository.GetAll(c => c.Blog == foundBlog))
                    {
                        Console.WriteLine(counter + "." + comment.GetCommentInfo());
                        counter++;
                    }
                }

                else
                {
                    Console.WriteLine($"{foundBlog.Title} is not approved or {foundBlog.Title} is rejected.");
                }
            }

            else
            {
                Console.WriteLine("Blog could not be found by entered code!");
            }
        }

        public static void ShowFilteredBlogsWithComments()
        {
            Console.WriteLine("Available Commands : ");
            Console.WriteLine("/title");
            Console.WriteLine("/firstname");
            Console.WriteLine();
            Console.WriteLine("Enter Command : ");

            string command = Console.ReadLine();

            if (command == "/title")
            {
                Console.WriteLine("Enter Title : ");
                string title = Console.ReadLine();

                foreach (Blog blog in blogRepository.GetAll(x => x.Title.Contains(title) && x.Status == BlogStatus.Approved))
                {
                    GetBlogInfo(blog);
                }
            }

            else if (command == "/firstname")
            {
                Console.WriteLine("Enter firstname : ");
                string firstname = Console.ReadLine();

                foreach (Blog blog in blogRepository.GetAll(x => x.Sender.FirstName == firstname && x.Status == BlogStatus.Approved))
                {
                    GetBlogInfo(blog);
                }
            }
        }

        private static void GetBlogInfo(Blog blog)
        {
            Console.WriteLine($"[{blog.CreatedTime.ToString("dd.MM.yyyy")}] [{blog.ID}] [{blog.Sender.FirstName}] [{blog.Sender.LastName}]");
            Console.WriteLine($"{blog.Title}");
            Console.WriteLine(blog.Content);
            Console.WriteLine();

            int rowNumber = 1;
            foreach (Comment comment in commentRepository.GetAll(x => x.Blog.ID == blog.ID))
            {
                Console.WriteLine($"{rowNumber}. {comment.GetCommentInfo()}");
                rowNumber++;
            }
        }
    }

    partial class BlogServices
    {
        public static void Inbox()
        {
            List<Inbox> inboxes = inboxRepository.GetAll();


            if (inboxes != null)
            {
                foreach (Inbox inbox in inboxes)
                {
                    if (inbox.Receiver == Dashboard.CurrentUser)
                    {
                        Console.WriteLine(inbox.Notification);
                    }
                }
            }

            else
            {
                Console.WriteLine("~~Nothing to see here~~");
            }
        }

        public static void AddBlog()
        {
            Console.WriteLine("Enter title of the blog :");
            string title = Console.ReadLine();

            while (!Validation.IsLengthBetween(title, 10, 35))
            {
                Console.WriteLine("Title's length should be more than 10 and less than 35!");
                title = Console.ReadLine();
            }

            Console.WriteLine("Enter Blog's content : ");
            string content = Console.ReadLine();

            while (!Validation.IsLengthBetween(content, 20, 45))
            {
                Console.WriteLine("Content's length should be more than 20 and less than 45!");
                content = Console.ReadLine();
            }

            BlogRepository.Add(Dashboard.CurrentUser, title, content);
        }

        public static void MyBlogs()
        {
            int rowNumber = 1;
            foreach (Blog blog in blogRepository.GetAll(x => x.Sender.Id == Dashboard.CurrentUser.Id))
            {
                Console.WriteLine($"{rowNumber} {blog.GetBlogInfo()}");
                rowNumber++;
            }
        }

        public static void DeleteBlog()
        {
            Console.WriteLine("Enter Blog's code :");
            string blogCode = Console.ReadLine();

            Blog blog = BlogRepository.GetByCode(blogCode);

            if (Dashboard.CurrentUser.Id == blog.Sender.Id)
            {
                blogRepository.Delete(blog);
                Console.WriteLine("Blog is deleted successfully!");
            }

            else
            {
                Console.WriteLine("You can only remove your own blogs!");
            }
        }

        public static void AddComment()
        {
            Console.WriteLine("Enter Blog's code that you want to add a comment to : ");
            string blogCode = Console.ReadLine();

            Blog blog = BlogRepository.GetByCode(blogCode);

            if (blog != null)
            {
                Console.WriteLine("Enter your comment : ");
                string comment = Console.ReadLine();

                while (!Validation.IsLengthBetween(comment, 10, 35))
                {
                    Console.WriteLine("The comments length should be more than 10 and less than 35!");
                    comment = Console.ReadLine();
                }

                Inbox inbox = new Inbox(blog.Sender, $"{blog.ID} blog Commented by {blog.Sender.FirstName} {blog.Sender.LastName}");
                inboxRepository.Add(inbox);

                CommentRepository.Add(blog, Dashboard.CurrentUser, comment);

                Console.WriteLine("Comment succesfully added to the blog!");
                Console.WriteLine();
            }

            else
            {
                Console.WriteLine("Blog could not be found!");
                Console.WriteLine();
            }
        }
    }

    partial class BlogServices
    {
        public static void BlogManagement()
        {

            foreach (Blog blog in blogRepository.GetAll(x => x.Status == BlogStatus.Created))
            {
                GetBlogInfo(blog);
            }

            Console.WriteLine("Availabe Commands :");
            Console.WriteLine("/approve-blog");
            Console.WriteLine("/reject-blog");
            string command = Console.ReadLine();

            Console.WriteLine("Enter blog's code :");
            string code = Console.ReadLine();

            Blog foundBlog = BlogRepository.GetByCode(code);
            string text = null;

            if (foundBlog != null)
            {
                if (command == "/approve-blog")
                {
                    foundBlog.Status = BlogStatus.Approved;
                    text = $"{foundBlog.ID} Blog is approved!";

                    Inbox inbox = new Inbox(foundBlog.Sender, text);
                    inboxRepository.Add(inbox);
                    Console.WriteLine(text);
                }

                else if (command == "/reject-blog")
                {
                    foundBlog.Status = BlogStatus.Rejected;
                    text = $"{foundBlog.ID} Blog is rejected!";

                    Inbox inbox = new Inbox(foundBlog.Sender, text);
                    inboxRepository.Add(inbox);
                    Console.WriteLine(text);
                }

                else
                {
                    Console.WriteLine("Command not found!");
                }
            }

            else
            {
                Console.WriteLine("Blog not found!");
            }
        }
    }
}

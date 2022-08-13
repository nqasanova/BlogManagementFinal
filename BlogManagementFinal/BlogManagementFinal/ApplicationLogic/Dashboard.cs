using System;
using System.Collections.Generic;
using BlogManagementFinal.ApplicationLogic.Services;
using BlogManagementFinal.Database.Enums;
using BlogManagementFinal.Database.Models;
using BlogManagementFinal.Database.Repository;
using BlogManagementFinal.Database.Repository.Common;

namespace BlogManagementFinal.ApplicationLogic
{
    public partial class Dashboard
    {
        public static User CurrentUser { get; set; }
        public static void AdminPanel(string email)
        {
            Repository<User, int> userRepository = new Repository<User, int>();
            Repository<Blog, string> blogRepository = new Repository<Blog, string>();

            User user = UserRepository.GetUserByEmail(email);
            Console.WriteLine($"Welcome admin : {user.GetUserInfo()}");

            while (true)
            {
                Console.WriteLine("Avaliable Commands : ");
                Console.WriteLine("/add-user");
                Console.WriteLine("/show-users");
                Console.WriteLine("/show-admins");
                Console.WriteLine("/show-auditing-blogs");
                Console.WriteLine("/logout");

                Console.WriteLine();
                Console.WriteLine("Enter Command : ");
                string command = Console.ReadLine();

                if (command == "/add-user")
                {
                    Authentication.Register();
                }

                else if (command == "/show-users")
                {
                    List<User> foundUser = userRepository.GetAll();
                    foreach (User users in foundUser)
                    {
                        if (users == null)
                        {
                            Console.WriteLine("User is not found!");
                        }

                        else if (users is not Admin)
                        {
                            Console.WriteLine(users.GetUserInfo());
                        }
                    }
                }

                else if (command == "/show-admins")
                {
                    List<User> admins = userRepository.GetAll();


                    foreach (User admin in admins)
                    {
                        if (admin is Admin)
                        {
                            Console.WriteLine(admin.GetUserInfo());
                        }
                    }
                }

                else if (command == "/show-auditing-blogs")
                {

                    List<Blog> blogs = blogRepository.GetAll();
                    CommentRepository commentRepository = new CommentRepository();

                    foreach (Blog blog in blogs)
                    {
                        if (blog.Status == BlogStatus.Created)
                        {
                            if (blog.Status == BlogStatus.Created)
                            {
                                Console.WriteLine(blog.GetBlogInfo());
                            }
                        }
                    }
                }

                else if (command == "/logout")
                {
                    Program.Main(new string[] { });
                    break;
                }
            }
        }
    }

    public partial class Dashboard
    {
        public static void UserPanel(string email)
        {
            User user = UserRepository.GetUserByEmail(email);
            Console.WriteLine($"User successfully joined : {user.GetUserInfo()}");

            while (true)
            {
                Console.WriteLine("Available User Commands : ");
                Console.WriteLine("/inbox");
                Console.WriteLine("/add-comment");
                Console.WriteLine("/blogs");
                Console.WriteLine("/add-blog");
                Console.WriteLine("/delete-blog");
                Console.WriteLine("/logout");

                string command = Console.ReadLine();

                if (command == "/inbox")
                {
                    BlogServices.Inbox();
                }

                else if (command == "/add-comment")
                {
                    BlogServices.AddComment();
                }

                else if (command == "blogs")
                {
                    BlogServices.MyBlogs();
                }

                else if (command == "/add-blog")
                {
                    BlogServices.AddBlog();
                }

                else if (command == "/delete-blog")
                {
                    BlogServices.DeleteBlog();
                }

                else if (command == "/logout")
                {
                    Program.Main(new string[] { });
                    break;
                }

                else
                {
                    Console.WriteLine("Command not found!");
                }
            }
        }
    }
}
using System;
using BlogManagementFinal.ApplicationLogic;
using BlogManagementFinal.ApplicationLogic.Services;

namespace BlogManagementFinal
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our application!");
            Console.WriteLine();
            Console.WriteLine("Available Commands : ");
            Console.WriteLine();
            Console.WriteLine("/register");
            Console.WriteLine("/login");
            Console.WriteLine("/show-blogs-with-comments");
            Console.WriteLine("/show-filtered-blogs-with-comments");
            Console.WriteLine("/find-blog-by-code");
            Console.WriteLine("/exit");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Enter Command : ");
                string command = Console.ReadLine();

                if (command == "/register")
                {
                    Authentication.Register();
                }

                else if (command == "/login")
                {
                    Authentication.Login();
                }

                else if (command == "/show-blogs-with-comments")
                {
                    BlogServices.ShowBlogs();
                }

                else if (command == "/show-filtered-blogs-with-comments")
                {
                    BlogServices.ShowFilteredBlogsWithComments();
                }

                else if (command == "/find-blog-by-code")
                {
                    BlogServices.FindBlogByCode();
                }

                else if (command == "/exit")
                {
                    Console.WriteLine("Thank you for using our application!");
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
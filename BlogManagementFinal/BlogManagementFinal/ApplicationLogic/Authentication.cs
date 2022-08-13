using System;
using BlogManagementFinal.ApplicationLogic.Validations;
using BlogManagementFinal.Database.Models;
using BlogManagementFinal.Database.Repository;

namespace BlogManagementFinal.ApplicationLogic
{
    public class Authentication
    {
        public static void Register()
        {
            string firstName = GetFirstName();
            string lastName = GetLastName();
            string email = GetEmail();
            string password = GetPassword();

            //User user = UserRepository.AddUser(firstName, lastName, email, password);
            //Console.WriteLine($"User added to the system {user.GetUserInfo()}");
            Program.Main(new string[] { });
        }

        public static string GetFirstName()
        {
            string firstName = string.Empty;
            bool isExceptionExists = false;

            do
            {
                try
                {
                    Console.Write("Please enter user's name : ");
                    firstName = Console.ReadLine();
                    isExceptionExists = false;
                }
                catch
                {
                    Console.Write("Something went wrong...");
                    isExceptionExists = true;
                }
            } while (!isExceptionExists && !UserValidation.IsValidFirstName(firstName));

            return firstName;
        }

        private static string GetLastName()
        {
            string lastName = string.Empty;
            bool isExceptionExists = false;

            do
            {
                try
                {
                    Console.Write("Please enter user's last name : ");
                    lastName = Console.ReadLine();
                    isExceptionExists = false;
                }
                catch
                {
                    Console.Write("Something went wrong...");
                    isExceptionExists = true;
                }
            } while (!isExceptionExists && !UserValidation.IsValidLastName(lastName));

            return lastName;
        }

        private static string GetEmail()
        {
            string email = string.Empty;
            bool isExceptionExists = false;

            do
            {
                try
                {
                    Console.Write("Please enter user's email : ");
                    email = Console.ReadLine();
                    isExceptionExists = false;
                }
                catch
                {
                    Console.Write("Something went wrong...");
                    isExceptionExists = true;
                }
            } while (!isExceptionExists && !UserValidation.IsValidEmail(email) && !UserValidation.IsUserExistsByEmail(email));

            return email;
        }

        private static string GetPassword()
        {
            string password = string.Empty;
            string confirmPassword = string.Empty;
            bool isExceptionExists = false;

            do
            {
                try
                {
                    Console.Write("Please enter user's password : ");
                    password = Console.ReadLine();

                    Console.Write("Please enter confirm password : ");
                    confirmPassword = Console.ReadLine();

                    isExceptionExists = false;
                }
                catch
                {
                    Console.Write("Something went wrong...");
                    isExceptionExists = true;
                }
            } while (!isExceptionExists && !UserValidation.IsValidPassword(password) && !UserValidation.IsPasswordsMatch(password, confirmPassword));

            return password;
        }

        public static void Login()
        {
            while (true)
            {
                Console.Write("Please enter user's email : ");
                string email = Console.ReadLine();

                Console.Write("Please enter user's password : ");
                string password = Console.ReadLine();

                if (UserRepository.IsUserExistByEmailAndPassword(email, password))
                {
                    User user = UserRepository.GetUserByEmail(email);

                    if (user != null)
                    {
                        Dashboard.CurrentUser = user;

                        if (user is Admin)
                        {
                            Dashboard.AdminPanel(email);
                        }

                        else if (user is User)
                        {
                            Dashboard.UserPanel(email);
                        }
                    }

                    else
                    {
                        Console.WriteLine("User is not found, password or email is inccorrect");
                    }

                    if (user is Admin)
                    {
                        Dashboard.AdminPanel(email);
                    }

                    else if (user is User)
                    {
                        Dashboard.UserPanel(email);
                    }

                    else
                    {
                        Console.WriteLine("User");
                    }
                }

                else
                {
                    Console.WriteLine("The email or password you have entered is not correct!");
                }
            }
        }
    }
}
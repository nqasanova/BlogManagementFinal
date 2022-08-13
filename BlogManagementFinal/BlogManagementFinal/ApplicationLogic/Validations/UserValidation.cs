using System;
using System.Text.RegularExpressions;
using BlogManagementFinal.Database.Repository;

namespace BlogManagementFinal.ApplicationLogic.Validations
{
    public class UserValidation
    {
        public static bool IsValidFirstName(string firstName)
        {
            Regex regex = new Regex(@"^(?=[A-Z]{1})([A-Za-z]{3,30})$");

            if (regex.IsMatch(firstName))
            {
                return true;
            }

            Console.WriteLine("The first name you have entered does not match the requirements. Your first name should only contain letters, start with an uppercase letter, and length should be more than 3 and less than 30!");

            return false;
        }

        public static bool IsValidLastName(string lastName)
        {
            Regex regex = new Regex(@"^(?=[A-Z]{1})([A-Za-z]{4,29})$");

            if (regex.IsMatch(lastName))
            {
                return true;
            }

            Console.WriteLine("The last name you have entered does not match the requirements. Your last name should only contain letters, start with an uppercase letter, and length should be more than 4 and less than 29!");

            return false;
        }

        public static bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^[A-Za-z0-9]{10,20}@code\.edu\.az$");

            if (regex.IsMatch(email))
            {
                return true;
            }

            Console.WriteLine("The email you have entered does not match the requirements. Your email should contain '@', email's domain must end with 'code.edu.az', should be unique, and length should be more than 10 and less than 20!");

            return false;
        }

        public static bool IsUserExistsByEmail(string email)
        {
            if (UserRepository.IsUserExistsByEmail(email))
            {
                Console.WriteLine("User already exists!");

                return true;
            }

            return false;
        }

        public static bool IsValidPassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*[0-9])(?=.*[A-Z])(?=[a-zA-Z0-9]{8,}).*[a-z]$");

            if (regex.IsMatch(password))
            {
                return true;
            }

            Console.WriteLine("The password you have entered does not match the requirements. Your password should contain at least 1 uppercase, 1 lowercase letter, and numbers!");

            return false;
        }

        public static bool IsPasswordsMatch(string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                return true;
            }

            Console.WriteLine("The passwords you have entered do not match!");

            return false;
        }
    }
}

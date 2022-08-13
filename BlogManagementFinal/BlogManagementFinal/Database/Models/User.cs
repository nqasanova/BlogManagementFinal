using System;
using BlogManagementFinal.Database.Models.Common;
using BlogManagementFinal.Database.Repository;

namespace BlogManagementFinal.Database.Models
{
    public class User : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public User(string firstName, string lastName, string email, string password, int? id = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;

            if (id != null)
            {
                base.Id = id.Value;
            }

            else
            {
                Id = UserRepository.IdCounter;
            }
        }

        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public virtual string GetUserInfo()
        {
            return $"ID : {Id}, First Name : {FirstName}, Last Name : {LastName}, Email : {Email}";
        }
    }
}

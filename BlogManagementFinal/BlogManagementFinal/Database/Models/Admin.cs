using System;
namespace BlogManagementFinal.Database.Models
{
    public class Admin : User
    {
        public Admin(string firstName, string lastName, string email, string password, int id)
            : base(firstName, lastName, email, password, id) //adding user after deleting
        {


        }

        public Admin(string firstName, string lastName, string email, string password)
            : base(firstName, lastName, email, password) //adding user
        {


        }

        public Admin(string firstName, string lastName)
            : base(firstName, lastName) //for updating
        {


        }

        public override string GetUserInfo()
        {
            return $"First Name : {FirstName}, Last Name : {LastName}, Email : {Email}, Registration Date : {RegistrationDate}";
        }
    }
}

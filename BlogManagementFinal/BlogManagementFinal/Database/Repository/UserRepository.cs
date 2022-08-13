using System;
using BlogManagementFinal.Database.Models;

namespace BlogManagementFinal.Database.Repository
{
    public class UserRepository : Common.Repository<User, int>
    {
        private static int _idCounter;
        public static int IdCounter
        {
            get
            {
                _idCounter++;
                return _idCounter;
            }
        }

        static UserRepository()
        {
            SeedUsers();
        }

        private static void SeedUsers()
        {
            DbContext.Add(new Admin("Mahmood", "Garibov", "qaribovmahmud@gmail.com", "123321"));
            DbContext.Add(new User("Natavan", "Hasanova", "natavan@gmail.com", "123321"));
        }

        public User AddUser(string firstName, string lastName, string email, string password)
        {
            User user = new User(firstName, lastName, email, password, IdCounter);
            DbContext.Add(user);
            return user;
        }

        public User AddUser(string firstName, string lastName, string email, string password, int id)
        {
            User user = new User(firstName, lastName, email, password, id);
            DbContext.Add(user);
            return user;
        }

        public static bool IsUserExistsByEmail(string email)
        {
            foreach (User user in DbContext)
            {
                if (user.Email == email)
                {
                    return true;
                }
            }

            return false;
        }

        public static User GetUserByEmailAndPassword(string email, string password)
        {
            foreach (User user in DbContext)
            {
                if (user.Email == email && user.Password == password)
                {
                    return user;
                }
            }

            return null;
        }

        public static bool IsUserExistByEmailAndPassword(string email, string password)
        {
            foreach (User user in DbContext)
            {
                if (user.Email == email && user.Password == password)
                {
                    return true;
                }
            }

            return false; ;
        }

        public static User GetUserByEmail(string email)
        {
            foreach (User user in DbContext)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }

            return null;
        }

        public static Admin GetAdminByEmail(string email)
        {
            foreach (Admin admin in DbContext)
            {
                if (admin.Email == email)
                {
                    return admin;
                }
            }

            return null;
        }

        public static User UpdateUser(string email, User user)
        {
            User foundUser = UserRepository.GetUserByEmail(email);

            foundUser.FirstName = user.FirstName;
            foundUser.LastName = user.LastName;
            return foundUser;
        }

        public static User UpdaateAdmin(string email, Admin admin)
        {
            User foundAdmin = UserRepository.GetAdminByEmail(email);

            foundAdmin.FirstName = admin.FirstName;
            foundAdmin.LastName = admin.LastName;
            return foundAdmin;
        }
    }
}

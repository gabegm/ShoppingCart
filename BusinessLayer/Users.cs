using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Users : BLBase
    {
        public Users() : base() { }

        /// <summary>
        /// Returns all users/
        /// </summary>
        /// <returns>All users</returns>
        public IQueryable<CommonLayer.User> GetUsers()
        {
            DataLayer.DAUsers dauser = new DataLayer.DAUsers(this.Entities);
            return dauser.GetUsers();

            //2nd method which does the same thing
            //return new DataLayer.DAUsers().GetUsers();
        }

        /// <summary>
        /// Returns all roles 
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Category> GetUserRoles()
        {
            return new DataLayer.DAUsers(this.Entities).GetUserRoles();
        }

        /// <summary>
        /// Get user with a specific email.
        /// </summary>
        /// <param name="Email">Email for which user will be returned.</param>
        /// <returns>One single user matching email.</returns>
        public CommonLayer.User GetUser(string Email)
        {
            return new DataLayer.DAUsers(this.Entities).GetUser(Email);

        }
        /// <summary>
        /// Gets a user for a specific id passed.
        /// </summary>
        /// <param name="UserId">Id for which user will be returned.</param>
        /// <returns>One single user matching the id passed.</returns>
        public CommonLayer.User GetUser(Guid UserId)
        {
            return new DataLayer.DAUsers(this.Entities).GetUser(UserId);

        }

        /// <summary>
        /// Adds a new to the database.
        /// </summary>
        /// <param name="User">user instance to be added.</param>
        private void AddUserToDatabase(CommonLayer.User User)
        {
            new DataLayer.DAUsers(this.Entities).AddUser(User);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="User">User changes to be updated to db.</param>
        public void UpdateUser(CommonLayer.User User)
        {
            new DataLayer.DAUsers(this.Entities).UpdateUser(User);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="User">User to be added.</param>
        public void RegisterUser(CommonLayer.User User, string ConfirmPassword)
        {
            CommonLayer.User Existing = this.GetUser(User.Email);
            if (Existing == null)
            {
                if (User.Password.Equals(ConfirmPassword))
                {
                    User.ID = Guid.NewGuid();
                    User.Password = HashSHA512String(User.Password, User.ID.ToString());
                    this.AddUserToDatabase(User);
                }
            }
        }

        /// <summary>
        /// Logins a user and returns true if account is valid.
        /// </summary>
        /// <param name="Email">User email.</param>
        /// <param name="Password"></param>
        /// <returns>True if valid, false if not.</returns>
        public bool Login(string Email, string Password)
        {
            try
            {
                CommonLayer.User User = this.GetUser(Email);
                if (User != null)
                {
                    string EncPassword = HashSHA512String(Password, User.ID.ToString());
                    if (EncPassword.Equals(User.Password))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



        /// <summary>
        /// Hash SHA 512 string.
        /// </summary>
        /// <param name="Salt">Salt which is used to salt final output.</param>
        /// <param name="Password">Password for user.</param>
        /// <returns>Hashed string.</returns>
        public static string HashSHA512String(string Password, string Salt)
        {
            string AllString = Password + Salt;
            if (string.IsNullOrEmpty(AllString)) throw new ArgumentNullException();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(AllString);
            buffer = System.Security.Cryptography.SHA512Managed.Create().ComputeHash(buffer);
            return System.Convert.ToBase64String(buffer).Substring(0, 86); // strip padding
        }

        /// <summary>
        /// Deletes a user from database.
        /// </summary>
        /// <param name="User">User to delete.</param>
        public void DeleteUser(Guid Id)
        {
            CommonLayer.User user = this.GetUser(Id);
            if (user != null)
            {
                new DataLayer.DAUsers(this.Entities).DeleteUser(user);
            }


        }

    }

}
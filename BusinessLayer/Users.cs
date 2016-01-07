using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Users : BLBase
    {
        public Users() : base() { }
        public Users(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        /// <summary>
        /// Returns all users and user accounts
        /// </summary>
        /// <returns>All users</returns>
        public IQueryable<CommonLayer.Models.UsersModel> GetUsers()
        {
            return new DataLayer.DAUsers(this.Entities).GetUsers();
        }

        /// <summary>
        /// Returns all roles 
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Role> GetUserRoles()
        {
            return new DataLayer.DAUsers(this.Entities).GetUserRoles();
        }

        /// <summary>
        /// Returns list of user types
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.UserType> GetUserTypes()
        {
            return new DataLayer.DAUserTypes(this.Entities).GetUserTypes();
        }

        /// <summary>
        /// Returns all countries 
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Town> GetUserTowns()
        {
            return new DataLayer.DAUsers(this.Entities).GetUserTowns();
        }

        /// <summary>
        /// Returns all countries 
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Country> GetUserCountries()
        {
            return new DataLayer.DAUsers(this.Entities).GetUserCountries();
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
        /// Gets a user for a specific id passed.
        /// </summary>
        /// <param name="UserId">Id for which user will be returned.</param>
        /// <returns>One single user matching the id passed.</returns>
        public CommonLayer.UserAccount GetUserAccount(Guid UserAccountID)
        {
            return new DataLayer.DAUsers(this.Entities).GetUserAccount(UserAccountID);
        }

        /// <summary>
        /// Gets a user for a specific id passed.
        /// </summary>
        /// <param name="UserId">Id for which user will be returned.</param>
        /// <returns>One single user matching the id passed.</returns>
        public CommonLayer.UserAccount GetUserAccount(string Username)
        {
            return new DataLayer.DAUsers(this.Entities).GetUserAccount(Username);
        }

        /// <summary>
        /// Adds a new to the database.
        /// </summary>
        /// <param name="User">user instance to be added.</param>
        private void AddUserToDatabase(CommonLayer.User User, CommonLayer.UserAccount UserAccount)
        {
            new DataLayer.DAUsers(this.Entities).AddUser(User, UserAccount);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="User">User changes to be updated to db.</param>
        public void UpdateUser(CommonLayer.User User, CommonLayer.UserAccount UserAccount)
        {
            new DataLayer.DAUsers(this.Entities).UpdateUser(User, UserAccount);
        }

        /// <summary>
        /// Deletes a user from database.
        /// </summary>
        /// <param name="User">User to delete.</param>
        public void DeleteUser(Guid UserID)
        {
            CommonLayer.User User = this.GetUser(UserID);
            CommonLayer.UserAccount UserAccount = this.GetUserAccount(User.UserAccountID);

            if (User != null && UserAccount != null)
            {
                foreach (CommonLayer.Role Roles in UserAccount.Roles)
                {
                    CommonLayer.Role Role = new Roles(this.Entities).GetRole(Roles.ID);
                    new DataLayer.DARoles(this.Entities).DeallocateUserRole(UserAccount, Role);
                }

                new DataLayer.DAUsers(this.Entities).DeleteUser(User, UserAccount);
            }
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="User"></param>
        /// <param name="UserAccount"></param>
        /// <param name="ConfirmPassword"></param>
        /// <param name="RoleID"></param>
        public void RegisterUser(CommonLayer.User User, CommonLayer.UserAccount UserAccount, string ConfirmPassword, Guid[] RoleID)
        {
            CommonLayer.User ExistingUser = this.GetUser(User.Email);
            CommonLayer.UserAccount ExistingUserAccount = this.GetUserAccount(UserAccount.Username);
            Roles Role = new Roles(this.Entities);

            if (ExistingUser == null && ExistingUserAccount == null)
            {
                if (UserAccount.Password.Equals(ConfirmPassword))
                {
                    User.ID = Guid.NewGuid();
                    UserAccount.ID = Guid.NewGuid();
                    User.UserAccountID = UserAccount.ID;
                    UserAccount.Password = HashSHA512String(UserAccount.Password, UserAccount.ID.ToString());
                    this.AddUserToDatabase(User, UserAccount);
                    if (RoleID != null)
                    {
                        foreach (Guid ID in RoleID)
                        {
                            Role.AddUserRole(ID, UserAccount.ID);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Logins a user and returns true if account is valid.
        /// </summary>
        /// <param name="Email">User email.</param>
        /// <param name="Password"></param>
        /// <returns>True if valid, false if not.</returns>
        public bool Login(string Username, string Password)
        {
            try
            {
                CommonLayer.UserAccount UserAccount = this.GetUserAccount(Username);
                if (UserAccount != null)
                {
                    string EncPassword = HashSHA512String(Password, UserAccount.ID.ToString());
                    if (EncPassword.Equals(UserAccount.Password))
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
            buffer = System.Security.Cryptography.SHA512.Create().ComputeHash(buffer);
            return Convert.ToBase64String(buffer).Substring(0, 86); // strip padding
        }
    }
}
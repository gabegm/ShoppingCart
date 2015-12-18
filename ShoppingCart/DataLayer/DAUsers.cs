using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DAUsers : ConnectionClass
    {
        public DAUsers() : base() { }
        public DAUsers(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        /// <summary>
        /// Returns all users/
        /// </summary>
        /// <returns>All users</returns>
        public IQueryable<CommonLayer.User> GetUsers()
        {
            return this.Entities.Users;
        }

        /// <summary>
        /// Returns roles from database
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Role> GetUserRoles()
        {
            return this.Entities.Roles;
        }

        /// <summary>
        /// Returns countries from database
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Country> GetUserCountries()
        {
            return this.Entities.Countries;
        }

        /// <summary>
        /// Get user with a specific email.
        /// </summary>
        /// <param name="Email">Email for which user will be returned.</param>
        /// <returns>One single user matching email.</returns>
        public CommonLayer.User GetUser(string Email)
        {
            return this.Entities.Users.SingleOrDefault(p => p.Email.Equals(Email));

        }
        /// <summary>
        /// Gets a user for a specific id passed.
        /// </summary>
        /// <param name="UserId">Id for which user will be returned.</param>
        /// <returns>One single user matching the id passed.</returns>
        public CommonLayer.User GetUser(Guid UserId)
        {
            return this.Entities.Users.SingleOrDefault(p => p.ID.Equals(UserId));
        }

        /// <summary>
        /// Adds a new to the database.
        /// </summary>
        /// <param name="User">user instance to be added.</param>
        public void AddUser(CommonLayer.User User)
        {
            this.Entities.Users.Add(User);
            this.Entities.SaveChanges();
        }

        /// <summary>
        /// Deletes a user from database.
        /// </summary>
        /// <param name="User">User to delete.</param>
        public void DeleteUser(CommonLayer.User User)
        {
            this.Entities.Users.Remove(User);
            this.Entities.SaveChanges();

        }

        public void UpdateUser(CommonLayer.User User)
        {
            CommonLayer.User ExistingUser = this.GetUser(User.ID);
            User.Password = ExistingUser.Password;
            this.Entities.Entry(ExistingUser).CurrentValues.SetValues(User);
            this.Entities.SaveChanges();
        }




    }
}
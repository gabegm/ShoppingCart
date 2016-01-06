using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class UserTypes : BLBase
    {
        public UserTypes() : base() { }
        public UserTypes(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        /// <summary>
        /// Returns all users and user accounts
        /// </summary>
        /// <returns>All users</returns>
        public IQueryable<CommonLayer.UserType> GetUserTypes()
        {
            return new DataLayer.DAUserTypes(this.Entities).GetUserTypes();
        }

        /// <summary>
        /// Get user with a specific email.
        /// </summary>
        /// <param name="Email">Email for which user will be returned.</param>
        /// <returns>One single user matching email.</returns>
        public CommonLayer.UserType GetUserType(Guid ID)
        {
            return new DataLayer.DAUserTypes(this.Entities).GetUserType(ID);
        }

        /// <summary>
        /// Adds a new to the database.
        /// </summary>
        /// <param name="User">user instance to be added.</param>
        public void AddUserTypeToDatabase(CommonLayer.UserType UserType)
        {
            UserType.ID = Guid.NewGuid();
            new DataLayer.DAUserTypes(this.Entities).AddUserType(UserType);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="User">User changes to be updated to db.</param>
        public void UpdateUserType(CommonLayer.UserType UserType)
        {
            new DataLayer.DAUserTypes(this.Entities).UpdateUserType(UserType);
        }

        /// <summary>
        /// Deletes a user from database.
        /// </summary>
        /// <param name="User">User to delete.</param>
        public void DeleteUserType(Guid ID)
        {
            CommonLayer.UserType UserType = this.GetUserType(ID);

            if (UserType != null)
            {
                new DataLayer.DAUserTypes(this.Entities).DeleteUserType(UserType);
            }
        }
    }
}
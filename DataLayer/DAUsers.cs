﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DAUsers : ConnectionClass
    {
        public DAUsers() : base() { }
        public DAUsers(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Models.UsersModel> GetUsers()
        {
            return (from users in this.Entities.Users
                    join userAccounts in this.Entities.UserAccounts on users.UserAccountID equals userAccounts.ID
                    join towns in this.Entities.Towns on users.TownID equals towns.ID
                    join countries in this.Entities.Countries on users.TownID equals countries.ID
                    select new CommonLayer.Models.UsersModel()
                    {
                        UserAccountID = userAccounts.ID,
                        UserAccountUsername = userAccounts.Username,
                        TownID = towns.ID,
                        TownName = towns.Name,
                        CountryID = countries.ID,
                        CountryName = countries.Name
                    });
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
        public IQueryable<CommonLayer.Town> GetUserTowns()
        {
            return this.Entities.Towns;
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
        /// Get user with a specific email.
        /// </summary>
        /// <param name="Email">Email for which user will be returned.</param>
        /// <returns>One single user matching email.</returns>
        public CommonLayer.UserAccount GetUserAccount(Guid ID)
        {
            return this.Entities.UserAccounts.SingleOrDefault(p => p.ID.Equals(ID));
        }

        /// <summary>
        /// Get user with a specific email.
        /// </summary>
        /// <param name="Email">Email for which user will be returned.</param>
        /// <returns>One single user matching email.</returns>
        public CommonLayer.UserAccount GetUserAccount(string username)
        {
            return this.Entities.UserAccounts.SingleOrDefault(p => p.Username.Equals(username));
        }

        /// <summary>
        /// Gets a user for a specific id passed.
        /// </summary>
        /// <param name="UserId">Id for which user will be returned.</param>
        /// <returns>One single user matching the id passed.</returns>
        public CommonLayer.User GetUser(Guid ID)
        {
            return this.Entities.Users.SingleOrDefault(p => p.ID.Equals(ID));
        }

        /// <summary>
        /// Adds a new to the database.
        /// </summary>
        /// <param name="User">user instance to be added.</param>
        public void AddUser(CommonLayer.User User, CommonLayer.UserAccount UserAccount)
        {
            this.Entities.Users.Add(User);
            this.Entities.UserAccounts.Add(UserAccount);
            this.Entities.SaveChanges();
        }

        /// <summary>
        /// Deletes a user from database.
        /// </summary>
        /// <param name="User">User to delete.</param>
        public void DeleteUser(CommonLayer.User User, CommonLayer.UserAccount UserAccount)
        {
            this.Entities.Users.Remove(User);
            this.Entities.UserAccounts.Remove(UserAccount);
            this.Entities.SaveChanges();
        }

        public void UpdateUser(CommonLayer.User User, CommonLayer.UserAccount UserAccount)
        {
            CommonLayer.User ExistingUser = this.GetUser(User.ID);
            this.Entities.Entry(ExistingUser).CurrentValues.SetValues(User);

            CommonLayer.UserAccount ExistingUserAccount = this.GetUserAccount(UserAccount.ID);
            this.Entities.Entry(ExistingUserAccount).CurrentValues.SetValues(UserAccount);

            this.Entities.SaveChanges();
        }
    }
}
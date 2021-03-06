﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DAUsers : ConnectionClass
    {
        public DAUsers() : base() { }
        public DAUsers(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        /// <summary>
        /// Returns list of all users
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Models.UsersModel> GetUsers()
        {
            return (from users in this.Entities.Users
                    join userAccounts in this.Entities.UserAccounts on users.UserAccountID equals userAccounts.ID
                    join towns in this.Entities.Towns on users.TownID equals towns.ID
                    join countries in this.Entities.Countries on towns.CountryID equals countries.ID
                    join userTypes in this.Entities.UserTypes on users.UserTypeID equals userTypes.ID
                    select new CommonLayer.Models.UsersModel()
                    {
                        ID = users.ID,
                        Email = users.Email,
                        FirstName = users.FirstName,
                        LastName = users.LastName,
                        Contact = users.Contact,
                        DOB = users.DOB,
                        Gender = users.Gender,
                        House = users.House,
                        Street = users.Street,
                        TownID = towns.ID,
                        TownName = towns.Name,
                        CountryID = countries.ID,
                        CountryName = countries.Name,
                        UserAccountID = userAccounts.ID,
                        Username = userAccounts.Username,
                        UserTypeID = userTypes.ID,
                        UserTypeName = userTypes.Name
                    });
        }

        /// <summary>
        /// Returns list of users according to role specified
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public IQueryable<CommonLayer.User> GetUserRoles(string Code)
        {
            return (from UserAccount in this.Entities.UserAccounts
                    join User in this.Entities.Users on UserAccount.ID equals User.UserAccountID
                    join Role in this.Entities.Roles on UserAccount.Roles.FirstOrDefault().ID equals Role.UserAccounts.FirstOrDefault().ID
                    where Role.Code == Code
                    select User
                    );
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
            try
            {
                this.Entities.Users.Add(User);
                this.Entities.UserAccounts.Add(UserAccount);
                this.Entities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
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

        /// <summary>
        /// Updates the user in the database
        /// </summary>
        /// <param name="User"></param>
        /// <param name="UserAccount"></param>
        public void UpdateUser(CommonLayer.User User, CommonLayer.UserAccount UserAccount)
        {
            try
            {
                CommonLayer.User ExistingUser = this.GetUser(User.ID);
                this.Entities.Entry(ExistingUser).CurrentValues.SetValues(User);

                CommonLayer.UserAccount ExistingUserAccount = this.GetUserAccount(UserAccount.ID);
                this.Entities.Entry(ExistingUserAccount).CurrentValues.SetValues(UserAccount);

                this.Entities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
    }
}
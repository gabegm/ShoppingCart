using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DARoles : ConnectionClass
    {
        public DARoles() : base() { }
        public DARoles(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        /// <summary>
        /// Returns all the roles from the database
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Role> GetRoles()
        {
            return this.Entities.Roles;
        }

        /// <summary>
        /// Returns a specific role
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public CommonLayer.Role GetRole(Guid ID)
        {
            return this.Entities.Roles.SingleOrDefault(p => p.ID == ID);
        }

        /// <summary>
        /// Returns a specific role
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public CommonLayer.Role GetRole(string Code)
        {
            return this.Entities.Roles.SingleOrDefault(c => c.Code == Code);
        }

        /// <summary>
        /// Adds a new role to the database
        /// </summary>
        /// <param name="role"></param>
        public void AddRole(CommonLayer.Role role)
        {
            this.Entities.Roles.Add(role);
            this.Entities.SaveChanges();
        }

        /// <summary>
        /// Updates an existing role in the database
        /// </summary>
        /// <param name="role"></param>
        public void UpdateRole(CommonLayer.Role role)
        {
            CommonLayer.Role ExistingRole = this.GetRole(role.ID);
            this.Entities.Entry(ExistingRole).CurrentValues.SetValues(role);
            this.Entities.SaveChanges();
        }

        /// <summary>
        /// Gets all roles for a specific user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IQueryable<CommonLayer.Role> GetUserRoles(Guid UserAccountID)
        {
            return (from userAccounts in this.Entities.UserAccounts
                    from roles in userAccounts.Roles
                    where userAccounts.ID == UserAccountID
                    select roles);
        }

        /// <summary>
        /// Gets all roles for a specific user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IQueryable<CommonLayer.Role> GetUserRoles(string Username)
        {
            return (from userAccounts in this.Entities.UserAccounts
                    from roles in userAccounts.Roles
                    where userAccounts.Username == Username
                    select roles);
        }

        /// <summary>
        /// Allocates a role to a user in the database
        /// </summary>
        /// <param name="UserAccount"></param>
        /// <param name="Role"></param>
        public void AllocateUserRole(CommonLayer.UserAccount UserAccount, CommonLayer.Role Role)
        {
            UserAccount.Roles.Add(Role);
            this.Entities.SaveChanges();
        }

        /// <summary>
        /// Deallocated a use role from the database
        /// </summary>
        /// <param name="UserAccount"></param>
        /// <param name="Role"></param>
        public void DeallocateUserRole(CommonLayer.UserAccount UserAccount, CommonLayer.Role Role)
        {
            UserAccount.Roles.Remove(Role);
            this.Entities.SaveChanges();
        }

        /// <summary>
        /// Gets users in a role.
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public IQueryable<CommonLayer.UserAccount> GetUsersinRole(Guid RoleID)
        {
            return (from userAccounts in this.Entities.UserAccounts
                    from roles in userAccounts.Roles
                    where roles.ID == RoleID
                    select userAccounts);
        }

        /// <summary>
        /// Deletes a role from the database
        /// </summary>
        /// <param name="Role"></param>
        public void DeleteRole(CommonLayer.Role Role)
        {
            this.Entities.Roles.Remove(Role);
            this.Entities.SaveChanges();
        }
    }
}
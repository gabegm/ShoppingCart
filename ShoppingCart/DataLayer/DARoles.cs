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

        public IQueryable<CommonLayer.Role> GetRoles()
        {
            return this.Entities.Roles;
        }

        public CommonLayer.Role GetRole(string RoleCode)
        {
            return this.Entities.Roles.SingleOrDefault(p => p.ID == RoleCode);
        }


        public void AddRole(CommonLayer.Role role)
        {
            this.Entities.Roles.Add(role);
            this.Entities.SaveChanges();
        }

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
        public IQueryable<CommonLayer.Role> GetUserRoles(Guid UserId)
        {
            return (from users in
                        this.Entities.Users
                    from roles in users.Roles
                    where users.ID == UserId
                    select roles);

        }

        /// <summary>
        /// Gets all roles for a specific user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IQueryable<CommonLayer.Role> GetUserRoles(string UserEmail)
        {
            return (from users in
                        this.Entities.Users
                    from roles in users.Roles
                    where users.Email == UserEmail
                    select roles);

        }

        public void AllocateUserRole(CommonLayer.User User, CommonLayer.Role Role)
        {
            User.Roles.Add(Role);
            this.Entities.SaveChanges();
        }

        /// <summary>
        /// Gets users in a role.
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public IQueryable<CommonLayer.User> GetUsersinRole(String RoleId)
        {
            return (from users in
                        this.Entities.Users
                    from roles in users.Roles
                    where roles.ID == RoleId
                    select users);
        }

        public void DeleteRole(CommonLayer.Role Role)
        {
            this.Entities.Roles.Remove(Role);
            this.Entities.SaveChanges();
        }
    }
}
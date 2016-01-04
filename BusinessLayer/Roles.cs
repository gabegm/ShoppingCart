using System;
using System.Linq;

namespace BusinessLayer
{
    public class Roles : BLBase
    {
        public Roles() : base() { }

        public IQueryable<CommonLayer.Role> GetRoles()
        {
            return new DataLayer.DARoles(this.Entities).GetRoles();
        }

        public void AddRoleToDatabase(CommonLayer.Role Role)
        {
            Role.ID = Guid.NewGuid();
            new DataLayer.DARoles(this.Entities).AddRole(Role);
        }

        public void UpdateRole(CommonLayer.Role role)
        {
            if (!string.IsNullOrEmpty(role.Name))
            {
                new DataLayer.DARoles(this.Entities).UpdateRole(role);
            }

        }

        public void AddUserRole(Guid RoleID, Guid UserID)
        {
            CommonLayer.UserAccount UserAccount = new Users().GetUserAccount(UserID);
            CommonLayer.Role Role = new Roles().GetRole(RoleID);

            new DataLayer.DARoles(this.Entities).AllocateUserRole(UserAccount, Role);
        }

        public void RemoveUserRole(Guid RoleID, Guid UserID)
        {
            CommonLayer.UserAccount UserAccount = new Users().GetUserAccount(UserID);
            CommonLayer.Role Role = new Roles().GetRole(RoleID);

            new DataLayer.DARoles(this.Entities).DeallocateUserRole(UserAccount, Role);
        }

        /// <summary>
        /// Gets user roles.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IQueryable<CommonLayer.Role> GetUserRoles(Guid UserId)
        {
            return new DataLayer.DARoles(this.Entities).GetUserRoles(UserId);
        }

        /// <summary>
        /// Gets all roles for a specific user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IQueryable<CommonLayer.Role> GetUserRoles(string UserEmail)
        {
            return new DataLayer.DARoles(this.Entities).GetUserRoles(UserEmail);

        }

        public CommonLayer.Role GetRole(Guid ID)
        {
            return new DataLayer.DARoles(this.Entities).GetRole(ID);
        }

        public CommonLayer.Role GetRole(string Code)
        {
            return new DataLayer.DARoles(this.Entities).GetRole(Code);
        }

        public void DeleteRole(Guid ID)
        {
            CommonLayer.Role role = this.GetRole(ID);
            if (role != null)
            {
                new DataLayer.DARoles(this.Entities).DeleteRole(role);
            }
        }
    }
}

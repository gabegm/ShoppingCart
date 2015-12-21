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
            DataLayer.DARoles dar = new DataLayer.DARoles(this.Entities);
            DataLayer.DAUsers dau = new DataLayer.DAUsers(this.Entities);

            CommonLayer.UserAccount UserAccount = dau.GetUserAccount(UserID);
            CommonLayer.Role Role = dar.GetRole(RoleID);

            dar.AllocateUserRole(UserAccount, Role);
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

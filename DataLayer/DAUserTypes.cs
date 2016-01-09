using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DAUserTypes : ConnectionClass
    {
        public DAUserTypes() : base() { }
        public DAUserTypes(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.UserType> GetUserTypes()
        {
            return this.Entities.UserTypes;
        }

        public CommonLayer.UserType GetUserType(Guid ID)
        {
            return this.Entities.UserTypes.SingleOrDefault(ut => ut.ID.Equals(ID));
        }

        public CommonLayer.UserType GetUserType(string Name)
        {
            return this.Entities.UserTypes.SingleOrDefault(ut => ut.Name.Equals(Name));
        }

        public CommonLayer.UserType GetUserType(CommonLayer.User User)
        {
            return this.Entities.UserTypes.SingleOrDefault(ut => ut.Users.Equals(User));
        }

        public void AddUserType(CommonLayer.UserType UserType)
        {
            this.Entities.UserTypes.Add(UserType);
            this.Entities.SaveChanges();
        }

        public void UpdateUserType(CommonLayer.UserType UserType)
        {
            CommonLayer.UserType ExistingUserType = this.GetUserType(UserType.ID);
            this.Entities.Entry(ExistingUserType).CurrentValues.SetValues(UserType);
            this.Entities.SaveChanges();
        }

        public void DeleteUserType(CommonLayer.UserType UserType)
        {
            this.Entities.UserTypes.Remove(UserType);
            this.Entities.SaveChanges();
        }
    }
}
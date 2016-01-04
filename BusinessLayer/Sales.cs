using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Sales : BLBase
    {
        public Sales() : base() { }

        /// <summary>
        /// Returns all users and user accounts
        /// </summary>
        /// <returns>All users</returns>
        public IQueryable<CommonLayer.Sale> GetSales()
        {
            return new DataLayer.DASales(this.Entities).GetSales();
        }

        /// <summary>
        /// Get user with a specific email.
        /// </summary>
        /// <param name="Email">Email for which user will be returned.</param>
        /// <returns>One single user matching email.</returns>
        public CommonLayer.Sale GetSale(Guid ID)
        {
            return new DataLayer.DASales(this.Entities).GetSale(ID);
        }

        /// <summary>
        /// Adds a new to the database.
        /// </summary>
        /// <param name="User">user instance to be added.</param>
        public void AddSaleToDatabase(CommonLayer.Sale Sale)
        {
            new DataLayer.DASales(this.Entities).AddSale(Sale);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="User">User changes to be updated to db.</param>
        public void UpdateSale(CommonLayer.Sale Sale)
        {
            new DataLayer.DASales(this.Entities).UpdateSale(Sale);
        }

        /// <summary>
        /// Deletes a user from database.
        /// </summary>
        /// <param name="User">User to delete.</param>
        public void DeleteSale(Guid ID)
        {
            CommonLayer.Sale Sale = this.GetSale(ID);

            if (Sale != null)
            {
                new DataLayer.DASales(this.Entities).DeleteSale(Sale);
            }
        }
    }
}
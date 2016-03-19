using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DAOrderDetails : ConnectionClass
    {
        public DAOrderDetails() : base() { }
        public DAOrderDetails(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.OrderDetail> GetOrderDetails(Guid OrderID)
        {
            return this.Entities.OrderDetails.Where(od => od.OrderID.Equals(OrderID));
        }

        public CommonLayer.OrderDetail GetOrderDetail(Guid ID)
        {
            return this.Entities.OrderDetails.SingleOrDefault(OrderDetail => OrderDetail.ID.Equals(ID));
        }

        public void AddNewOrderDetails(CommonLayer.OrderDetail OrderDetail)
        {
            try
            {
                this.Entities.OrderDetails.Add(OrderDetail);
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

        public void UpdateOrderDetail(CommonLayer.OrderDetail OrderDetail)
        {
            CommonLayer.OrderDetail ExistingOrderDetail = this.GetOrderDetail(OrderDetail.ID);

            this.Entities.Entry(ExistingOrderDetail).CurrentValues.SetValues(OrderDetail);
            this.Entities.SaveChanges();
        }

        public void DeleteOrderDetail(CommonLayer.OrderDetail OrderDetail)
        {
            this.Entities.OrderDetails.Remove(OrderDetail);
            this.Entities.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DAOrders : ConnectionClass
    {
        public DAOrders() : base() { }
        public DAOrders(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Models.OrdersModel> GetOrdersAsModel()
        {
            return (from Order in this.Entities.Orders
                    join OrderDetail in this.Entities.OrderDetails on Order.ID equals OrderDetail.OrderID
                    join User in this.Entities.Users on Order.UserID equals User.ID
                    select new CommonLayer.Models.OrdersModel()
                    {
                        ID = Order.ID,
                        Date = Order.Date,
                        Status = Order.Status,
                        Number = Order.Number,
                        UserID = Order.UserID,
                        UserEmail = User.Email,
                        OrderDetailsID = OrderDetail.ID,
                        ProductID = OrderDetail.ProductID,
                        ProductPrice = (float)OrderDetail.ProductPrice,
                        ProductQuantity = OrderDetail.ProductQuantity,
                        ProductVATRate = (float)OrderDetail.ProductVATRate
                    });
        }

        public IQueryable<CommonLayer.Models.OrdersModel> GetOrdersAsModel(Guid UserID)
        {
            return (from Order in this.Entities.Orders
                    join OrderDetail in this.Entities.OrderDetails on Order.ID equals OrderDetail.OrderID
                    join User in this.Entities.Users on Order.UserID equals User.ID
                    where User.ID == UserID
                    select new CommonLayer.Models.OrdersModel()
                    {
                        ID = Order.ID,
                        Date = Order.Date,
                        Status = Order.Status,
                        Number = Order.Number,
                        UserID = Order.UserID,
                        UserEmail = User.Email,
                        OrderDetailsID = OrderDetail.ID,
                        ProductID = OrderDetail.ProductID,
                        ProductPrice = (float)OrderDetail.ProductPrice,
                        ProductQuantity = OrderDetail.ProductQuantity,
                        ProductVATRate = (float)OrderDetail.ProductVATRate
                    });
        }

        public void AddNewOrder(CommonLayer.Order Order, CommonLayer.OrderDetail OrderDetail)
        {
            try
            {
                this.Entities.Orders.Add(Order);
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

        public CommonLayer.Order GetOrder(Guid ID)
        {
            return this.Entities.Orders.SingleOrDefault(order => order.ID.Equals(ID));
        }

        public CommonLayer.OrderDetail GetOrderDetail(Guid ID)
        {
            return this.Entities.OrderDetails.SingleOrDefault(OrderDetail => OrderDetail.ID.Equals(ID));
        }

        public void UpdateOrder(CommonLayer.Order Order, CommonLayer.OrderDetail OrderDetail)
        {
            CommonLayer.Order ExistingOrder = this.GetOrder(Order.ID);
            CommonLayer.OrderDetail ExistingOrderDetail = this.GetOrderDetail(OrderDetail.ID);

            this.Entities.Entry(ExistingOrder).CurrentValues.SetValues(Order);
            this.Entities.Entry(ExistingOrderDetail).CurrentValues.SetValues(OrderDetail);
            this.Entities.SaveChanges();
        }

        public void DeleteOrder(CommonLayer.Order Order, CommonLayer.OrderDetail OrderDetail)
        {
            this.Entities.Orders.Remove(Order);
            this.Entities.OrderDetails.Remove(OrderDetail);
            this.Entities.SaveChanges();
        }
    }
}
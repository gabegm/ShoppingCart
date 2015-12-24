using System;
using System.Collections.Generic;
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
            return (from orders in this.Entities.Orders
                    join orderDetails in this.Entities.OrderDetails on orders.OrderDetailsID equals orderDetails.ID
                    select new CommonLayer.Models.OrdersModel()
                    {
                        ID = orders.ID,
                        Date = orders.Date,
                        Status = orders.Status,
                        Number = orders.Number,
                        UserID = orders.UserID,
                        OrderDetailsID = orders.OrderDetailsID,
                        ProductPrice = (float)orderDetails.ProductPrice,
                        ProductQuantity = orderDetails.ProductQuantity,
                        ProductVATRate = (float)orderDetails.ProductVATRate
                    });
        }

        public void AddNewOrder(CommonLayer.Order order)
        {
            this.Entities.Orders.Add(order);
            this.Entities.SaveChanges();
        }

        public CommonLayer.Order GetOrder(Guid ID)
        {
            return this.Entities.Orders.SingleOrDefault(order => order.ID.Equals(ID));
        }
    }
}
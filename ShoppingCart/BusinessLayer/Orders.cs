using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class Orders : BLBase
    {
        public Orders() : base() { }
        public Orders(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Order> GetOrders()
        {
            return new DataLayer.DAOrders(this.Entities).GetOrders();
        }

        public IQueryable<CommonLayer.Models.OrdersModel> GetOrdersAsModel()
        {
            return new DataLayer.DAOrders(this.Entities).GetOrdersAsModel();
        }

        public IQueryable<CommonLayer.Models.OrdersModel> GetOrdersAsModel(Guid UserID)
        {
            return new DataLayer.DAOrders(this.Entities).GetOrdersAsModel(UserID);
        }

        public void AddOrder(Guid OrderID, Guid UserID)
        {
            CommonLayer.Order Order = new CommonLayer.Order();

            Order.ID = OrderID;
            Order.Date = DateTime.Today;
            Order.Status = "Pending";
            Order.Number = new Random().Next(1, 100000001);
            Order.UserID = UserID;

            new DataLayer.DAOrders(this.Entities).AddNewOrder(Order);
        }

        public CommonLayer.Order GetOrder(Guid ID)
        {
            return new DataLayer.DAOrders(this.Entities).GetOrder(ID);
        }

        public CommonLayer.OrderDetail GetOrderDetail(Guid ID)
        {
            return new DataLayer.DAOrders(this.Entities).GetOrderDetail(ID);
        }

        public void UpdateOrder(CommonLayer.Order Order)
        {
            if (!string.IsNullOrEmpty(Order.ID.ToString()))
            {
                CommonLayer.User User = new Users(this.Entities).GetUser(Order.UserID);
                new DataLayer.DAOrders(this.Entities).UpdateOrder(Order);
                new Email(this.Entities).SendEmailToCustomer(User.Email, "Order Status", "Order status now: " + Order.Status);
                new Email(this.Entities).SendEmailToAdmin("Order Updated", "Order status now: " + Order.Status);
            }
        }

        public void DeleteOrder(Guid OrderID)
        {
            CommonLayer.Order Order = this.GetOrder(OrderID);

            if (Order != null)
            {    
                foreach (CommonLayer.OrderDetail OrderDetail in new OrderDetails(this.Entities).GetOrderDetails(OrderID).ToList())
                {
                    new DataLayer.DAOrderDetails(this.Entities).DeleteOrderDetail(OrderDetail);
                }

                new DataLayer.DAOrders(this.Entities).DeleteOrder(Order);
            }
        }
    }
}
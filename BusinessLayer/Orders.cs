using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class Orders : BLBase
    {
        public Orders() : base() { }
        public Orders(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Models.OrdersModel> GetOrdersAsModel()
        {
            return new DataLayer.DAOrders(this.Entities).GetOrdersAsModel();
        }

        public void AddOrder(CommonLayer.CartItem Cart)
        {
            CommonLayer.Order Order = new CommonLayer.Order();
            CommonLayer.OrderDetail OrderDetail = new CommonLayer.OrderDetail();

            Products ProductsBL = new Products(this.Entities);
            CommonLayer.Product Product = ProductsBL.GetProduct(Cart.ProductID);

            Order.ID = Guid.NewGuid();
            Order.Date = DateTime.Today;
            Order.Status = "Pending";
            Order.Number = new Random().Next(1, 100000001);
            Order.UserID = Cart.UserID;

            OrderDetail.ID = Guid.NewGuid();
            OrderDetail.ProductQuantity = Cart.Quantity;
            OrderDetail.ProductPrice = Product.Price;
            OrderDetail.ProductVATRate = Product.VATRate;
            OrderDetail.OrderID = Order.ID;
            OrderDetail.ProductID = Cart.ProductID;

            new DataLayer.DAOrders(this.Entities).AddNewOrder(Order, OrderDetail);

            Product.Quantity--;
            ProductsBL.UpdateProduct(Product);
        }

        public CommonLayer.Order GetOrder(Guid ID)
        {
            return new DataLayer.DAOrders(this.Entities).GetOrder(ID);
        }

        public CommonLayer.OrderDetail GetOrderDetail(Guid ID)
        {
            return new DataLayer.DAOrders(this.Entities).GetOrderDetail(ID);
        }

        public void UpdateOrder(CommonLayer.Order Order, CommonLayer.OrderDetail OrderDetail)
        {
            if (!string.IsNullOrEmpty(Order.ID.ToString()) && !string.IsNullOrEmpty(OrderDetail.ID.ToString()))
            {
                new DataLayer.DAOrders(this.Entities).UpdateOrder(Order, OrderDetail);
            }
        }

        public void DeleteOrder(Guid ID)
        {
            CommonLayer.OrderDetail OrderDetail = this.GetOrderDetail(ID);
            CommonLayer.Order Order = this.GetOrder(OrderDetail.OrderID);

            if (Order != null && OrderDetail != null)
            {
                new DataLayer.DAOrders(this.Entities).DeleteOrder(Order, OrderDetail);
            }
        }
    }
}
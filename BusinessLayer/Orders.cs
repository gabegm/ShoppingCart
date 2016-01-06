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
            CommonLayer.Product Product = new Products().GetProduct(Cart.ProductID);

            Order.ID = Guid.NewGuid();
            OrderDetail.ID = Guid.NewGuid();

            Order.UserID = Cart.UserID;
            OrderDetail.ProductID = Cart.ProductID;
            OrderDetail.ProductQuantity = Cart.Quantity;
            OrderDetail.ProductPrice = Product.Price;
            OrderDetail.ProductVATRate = Product.VATRate;

            this.AddOrderToDatabase(Order, OrderDetail);
        }

        public void AddOrderToDatabase(CommonLayer.Order Order, CommonLayer.OrderDetail OrderDetail)
        {
            Order.ID = new Guid();
            OrderDetail.ID = new Guid();
            new DataLayer.DAOrders(this.Entities).AddNewOrder(Order, OrderDetail);
        }
    }
}
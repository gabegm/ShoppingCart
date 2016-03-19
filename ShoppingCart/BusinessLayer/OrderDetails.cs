using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class OrderDetails : BLBase
    {

        public OrderDetails() : base() { }
        public OrderDetails(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.OrderDetail> GetOrderDetails(Guid OrderID)
        {
            return new DataLayer.DAOrderDetails(this.Entities).GetOrderDetails(OrderID);
        }

        public CommonLayer.OrderDetail GetOrderDetail(Guid ID)
        {
            return new DataLayer.DAOrders(this.Entities).GetOrderDetail(ID);
        }

        public void AddOrderDetails(CommonLayer.CartItem Cart, Guid OrderID)
        {
            CommonLayer.OrderDetail OrderDetail = new CommonLayer.OrderDetail();
            Products ProductsBL = new Products(this.Entities);
            CommonLayer.Product Product = ProductsBL.GetProduct(Cart.ProductID);
            CommonLayer.User User = new Users(this.Entities).GetUser(Cart.UserID);
            CommonLayer.UserType UserType = new UserTypes(this.Entities).GetUserType(User.UserTypeID);
            CommonLayer.ProductPrice ProductPrice = new ProductPrices(this.Entities).GetProductPrice(Product.ID, UserType.ID);

            OrderDetail.ID = Guid.NewGuid();
            OrderDetail.ProductQuantity = Cart.Quantity;
            if (ProductsBL.isProductOnSale(Product.ID))
            {
                OrderDetail.ProductPrice = new Sales(this.Entities).GetSalePrice(Product.SaleID.Value, (float)ProductPrice.Price);
            }
            else
            {
                OrderDetail.ProductPrice = ProductPrice.Price;
            }
            OrderDetail.ProductVATRate = Product.VATRate;
            OrderDetail.OrderID = OrderID;
            OrderDetail.ProductID = Cart.ProductID;

            new DataLayer.DAOrderDetails(this.Entities).AddNewOrderDetails(OrderDetail);

            Product.Quantity -= OrderDetail.ProductQuantity;
            ProductsBL.DecreaseQuantity(Product.ID, OrderDetail.ProductQuantity);

            new Email(this.Entities).SendEmailToCustomer(User.Email, "Order Confirmed", "Order status is pending");
            new Email(this.Entities).SendEmailToAdmin("Product Stock Changed", "Product stock now: " + Product.Quantity);
        }

        public void UpdateOrderDetails(CommonLayer.OrderDetail OrderDetails)
        {
            int Quantity = 0;

            CommonLayer.OrderDetail OldOrderDetails = this.GetOrderDetail(OrderDetails.ID);

            Products ProductsBL = new Products(this.Entities);

            CommonLayer.Product Product = ProductsBL.GetProduct(OrderDetails.ProductID);

            if (OrderDetails.ProductQuantity > OldOrderDetails.ProductQuantity)
            {
                Quantity = OrderDetails.ProductQuantity - OldOrderDetails.ProductQuantity;

                if (Product.Quantity > Quantity)
                {
                    new DataLayer.DAOrderDetails(this.Entities).UpdateOrderDetail(OrderDetails);

                    ProductsBL.DecreaseQuantity(OrderDetails.ProductID, Quantity);
                }
            }

            if (OrderDetails.ProductQuantity < OldOrderDetails.ProductQuantity)
            {
                Quantity = OldOrderDetails.ProductQuantity - OrderDetails.ProductQuantity;

                if (Product.Quantity > Quantity)
                {
                    new DataLayer.DAOrderDetails(this.Entities).UpdateOrderDetail(OrderDetails);

                    ProductsBL.IncreaseQuantity(OrderDetails.ProductID, Quantity);
                }
            }
        }

        public void DeleteOrderDetails(Guid ID)
        {
            new DataLayer.DAOrderDetails(this.Entities).DeleteOrderDetail(this.GetOrderDetail(ID));
        }
    }
}
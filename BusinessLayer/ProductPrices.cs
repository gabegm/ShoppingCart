using System;
using System.Linq;


namespace BusinessLayer
{
    public class ProductPrices : BLBase
    {
        public ProductPrices() : base() { }
        public ProductPrices(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public void AllocateProductPrice(Guid ProductID, Guid UserTypeID, int Price)
        {
            CommonLayer.Product Product = new Products(this.Entities).GetProduct(ProductID);
            CommonLayer.UserType UserType = new UserTypes(this.Entities).GetUserType(UserTypeID);
            CommonLayer.ProductPrice ProductPrice = new CommonLayer.ProductPrice();

            ProductPrice.UserTypeID = UserTypeID;
            ProductPrice.ProductID = ProductID;
            ProductPrice.Price = Price;

            new DataLayer.DAProductPrices(this.Entities).AllocateProductPrice(ProductPrice);
        }

        public void DeallocateProductPrice(CommonLayer.ProductPrice ProductPrice)
        {
            new DataLayer.DAProductPrices(this.Entities).DeallocateProductPrice(ProductPrice);
        }

        public IQueryable<CommonLayer.ProductPrice> GetProductPrices()
        {
            return new DataLayer.DAProductPrices(this.Entities).GetProductPrices();
        }

        public CommonLayer.ProductPrice GetProductPrice(Guid ProductID, Guid UserTypeID)
        {
            return new DataLayer.DAProductPrices(this.Entities).GetProductPrice(ProductID, UserTypeID);
        }
    }
}
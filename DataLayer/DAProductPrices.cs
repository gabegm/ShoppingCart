using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DAProductPrices : ConnectionClass
    {
        public DAProductPrices() : base() { }
        public DAProductPrices(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public void AllocateProductPrice(CommonLayer.ProductPrice ProductPrice)
        {
            this.Entities.ProductPrices.Add(ProductPrice);
            this.Entities.SaveChanges();
        }

        public void DeallocateProductPrice(CommonLayer.ProductPrice ProductPrice)
        {
            this.Entities.ProductPrices.Remove(ProductPrice);
            this.Entities.SaveChanges();
        }

        public IQueryable<CommonLayer.ProductPrice> GetProductPrices()
        {
            return (from ProductPrice in this.Entities.ProductPrices
                    select ProductPrice
                    );
        }

        public CommonLayer.ProductPrice GetProductPrice(Guid ProductID, Guid UserTypeID)
        {
            return this.Entities.ProductPrices.SingleOrDefault(pp => pp.ProductID.Equals(ProductID) && pp.UserTypeID.Equals(UserTypeID));
        }
    }
}
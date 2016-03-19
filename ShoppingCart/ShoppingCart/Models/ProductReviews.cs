using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class ProductReviews
    {
        public CommonLayer.Product Product;
        public IQueryable<CommonLayer.Review> Reviews;
        public CommonLayer.UserType UserType;
        public IQueryable<CommonLayer.ProductPrice> ProductPrices;
    }
}
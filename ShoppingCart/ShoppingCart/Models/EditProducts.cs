using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class EditProducts
    {
        public CommonLayer.Product Product;
        public IQueryable<CommonLayer.ProductPrice> ProductPrices;
        public IQueryable<CommonLayer.UserType> UserTypes;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class UserTypesProductPrices
    {
        public CommonLayer.UserType UserType;
        public IQueryable<CommonLayer.ProductPrice> ProductPrices;
        public IQueryable<CommonLayer.Models.ProductsModel> Products;
    }
}
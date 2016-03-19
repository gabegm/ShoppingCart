using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class ProductsList
    {
        public IQueryable<CommonLayer.Models.ProductsModel> Products;
        public IQueryable<CommonLayer.UserType> UserTypes;
        public IQueryable<CommonLayer.ProductPrice> ProductPrices;
    }
}
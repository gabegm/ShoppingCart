using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class TempBusiness
    {
        public List<string> GetProducts()
        {
            List<string> Products = new List<string>();
            Products.Add("Product1");
            Products.Add("Product2");

            return Products;
        }

    }
}
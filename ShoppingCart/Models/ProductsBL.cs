using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class ProductsBL
    {


        public List<Models.UiModels.Product> GetProducts()
        {
            List<Models.UiModels.Product> listofProducts = new List<UiModels.Product>();
            Models.UiModels.Product p1 = new UiModels.Product();
            p1.Name = "Product1";
            p1.Quantity = 5;
            p1.Id = 2;
            p1.Description = "New";

            listofProducts.Add(p1);

            p1 = new UiModels.Product();
            p1.Name = "Product2";
            p1.Quantity = 6;
            p1.Id = 3;
            p1.Description = "Old";

            listofProducts.Add(p1);

            return listofProducts;
        }

        public Models.UiModels.Product GetProduct(int Id)
        {
            List<Models.UiModels.Product> listofProducts = new List<UiModels.Product>();
            Models.UiModels.Product p1 = new UiModels.Product();
            p1.Name = "Product1";
            p1.Quantity = 5;
            p1.Id = 2;
            p1.Description = "New";

            listofProducts.Add(p1);

            p1 = new UiModels.Product();
            p1.Name = "Product2";
            p1.Quantity = 6;
            p1.Id = 3;
            p1.Description = "Old";

            listofProducts.Add(p1);

            Models.UiModels.Product ProductFound = listofProducts.SingleOrDefault(p => p.Id == Id);

            return ProductFound;
        }
    }
}
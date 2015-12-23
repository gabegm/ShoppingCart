using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DACarts : ConnectionClass
    {
        public DACarts() : base() { }
        public DACarts(CommonLayer.DBModelEntities Entities) : base(Entities) { }
        
        public IQueryable<CommonLayer.Models.CartsModel> GetCartItemsAsModel()
        {
            return (from carts in this.Entities.CartItems
                    join products in this.Entities.Products on carts.ProductID equals products.ID
                    join users in this.Entities.Users on carts.UserID equals users.ID
                    select new CommonLayer.Models.CartsModel()
                    {
                        ProductID = products.ID,
                        ProductName = products.Name,
                        ProductPrice = (float)products.Price,
                        ProductVATRate = (float)products.VATRate,
                        UserID = users.ID
                    });
        }
        
        public void AddCartItem(CommonLayer.CartItem CartItem)
        {
            this.Entities.CartItems.Add(CartItem);
            this.Entities.SaveChanges();
        } 
    }
}
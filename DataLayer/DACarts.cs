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
        
        public IQueryable<CommonLayer.Models.CartsModel> GetCartItemsAsModel(Guid ID)
        {
            return (from carts in this.Entities.CartItems
                    join products in this.Entities.Products on carts.ProductID equals products.ID
                    select new CommonLayer.Models.CartsModel()
                    {
                        ProductID = products.ID,
                        ProductName = products.Name
                    });
        } 
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class CartItems : BLBase
    {
        public CartItems() : base() { }
        public CartItems(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Models.CartItemsModel> GetCartProductsAsModel()
        {
            return new DataLayer.DACartItems(this.Entities).GetCartItemsAsModel();
        }

        public CommonLayer.CartItem GetCartItem(Guid ID)
        {
            return new DataLayer.DACartItems(this.Entities).GetCartItem(ID);
        }

        private void AddCartItemToDatabase(CommonLayer.CartItem CartItem)
        {
            new DataLayer.DACartItems(this.Entities).AddCartItem(CartItem);
        }

        public void UpdateCartItem(CommonLayer.CartItem CartItem)
        {
            if (!string.IsNullOrEmpty(CartItem.ID.ToString()))
            {
                new DataLayer.DACartItems(this.Entities).UpdateCartItem(CartItem);
            }
        }

        public void DeleteCartItem(Guid ID)
        {
            CommonLayer.CartItem CartItem = this.GetCartItem(ID);
            if (CartItem != null)
            {
                new DataLayer.DACartItems(this.Entities).DeleteCartItem(CartItem);
            }
        }
    }
}
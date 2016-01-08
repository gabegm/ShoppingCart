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

        public IQueryable<CommonLayer.CartItem> GetUserCartItems(Guid ID)
        {
            return new DataLayer.DACartItems(this.Entities).GetUserCartItems(ID);
        }

        public IQueryable<CommonLayer.Models.CartItemsModel> GetUserCartItemsAsModel(Guid ID)
        {
            return new DataLayer.DACartItems(this.Entities).GetUserCartItemsAsModel(ID);
        }

        public CommonLayer.CartItem GetCartItem(Guid ID)
        {
            return new DataLayer.DACartItems(this.Entities).GetCartItem(ID);
        }

        public CommonLayer.CartItem GetCartItem(Guid UserID, Guid ProductID)
        {
            return new DataLayer.DACartItems(this.Entities).GetCartItem(UserID, ProductID);
        }

        private void AddCartItemToDatabase(CommonLayer.CartItem CartItem)
        {
            new DataLayer.DACartItems(this.Entities).AddCartItem(CartItem);
        }

        public void UpdateCartItem(Guid ID, int Quantity)
        {
            if (!string.IsNullOrEmpty(ID.ToString()))
            {
                new DataLayer.DACartItems(this.Entities).UpdateCartItem(ID, Quantity);
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
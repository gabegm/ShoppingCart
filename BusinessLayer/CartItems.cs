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

        public IQueryable<CommonLayer.Models.CartItemsModel> GetCartItemsAsModel()
        {
            return new DataLayer.DACartItems(this.Entities).GetCartItemsAsModel();
        }

        public IQueryable<CommonLayer.CartItem> GetUserCartItems(CommonLayer.User User)
        {
            return new DataLayer.DACartItems(this.Entities).GetUserCartItems(User.ID);
        }

        public IQueryable<CommonLayer.Models.CartItemsModel> GetUserCartItemsAsModel(CommonLayer.User User)
        {
            CommonLayer.UserType UserType = new UserTypes(this.Entities).GetUserType(User.UserTypeID);

            return new DataLayer.DACartItems(this.Entities).GetUserCartItemsAsModel(User.ID, UserType.ID);
        }

        public CommonLayer.CartItem GetCartItem(Guid ID)
        {
            return new DataLayer.DACartItems(this.Entities).GetCartItem(ID);
        }

        public CommonLayer.CartItem GetCartItem(Guid UserID, Guid ProductID)
        {
            return new DataLayer.DACartItems(this.Entities).GetCartItem(UserID, ProductID);
        }

        /// <summary>
        /// Returns price including vat
        /// </summary>
        /// <param name="ProductPrice"></param>
        /// <returns></returns>
        public float GetPriceVAT(CommonLayer.ProductPrice ProductPrice)
        {
            return (float)ProductPrice.Price * 1.18f;
        }

        /// <summary>
        /// Returns VAT value
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public float GetTotalVAT(CommonLayer.User User)
        {
            float Total = 0;

            foreach (CommonLayer.Models.CartItemsModel CartItems in this.GetUserCartItemsAsModel(User))
            {
                Total += (CartItems.ProductPrice * 0.18f);
            }

            return Total;
        }

        /// <summary>
        /// Returns total price
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public float GetTotalPrice(CommonLayer.User User)
        {
            float Total = 0;

            foreach(CommonLayer.Models.CartItemsModel CartItems in this.GetUserCartItemsAsModel(User))
            {
                Total += (CartItems.ProductPrice * CartItems.Quantity); 
            }

            return Total;
        }

        /// <summary>
        /// Returns total price including vat
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public float GetTotalVATPrice(CommonLayer.User User)
        {
            float Total = 0;

            foreach (CommonLayer.Models.CartItemsModel CartItems in this.GetUserCartItemsAsModel(User))
            {
                Total += ((CartItems.ProductPrice * CartItems.Quantity) * 1.18f);
            }

            return Total;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DACartItems : ConnectionClass
    {
        public DACartItems() : base() { }
        public DACartItems(CommonLayer.DBModelEntities Entities) : base(Entities) { }
        
        public IQueryable<CommonLayer.Models.CartItemsModel> GetCartItemsAsModel()
        {
            return (from carts in this.Entities.CartItems
                    join products in this.Entities.Products on carts.ProductID equals products.ID
                    join users in this.Entities.Users on carts.UserID equals users.ID
                    select new CommonLayer.Models.CartItemsModel()
                    {
                        ID = carts.ID,
                        ProductID = products.ID,
                        ProductName = products.Name,
                        ProductPrice = (float)products.Price,
                        ProductVATRate = (float)products.VATRate,
                        Quantity = carts.Quantity,
                        UserID = users.ID
                    });
        }

        public IQueryable<CommonLayer.CartItem> GetUserCartItems(Guid UserID)
        {
            return (from CartItems in this.Entities.CartItems
                    join User in this.Entities.Users on CartItems.UserID equals UserID
                    select CartItems
                    );
        }

        public IQueryable<CommonLayer.Models.CartItemsModel> GetUserCartItemsAsModel(Guid UserID)
        {
            return (from CartItem in this.Entities.CartItems
                    join Product in this.Entities.Products on CartItem.ProductID equals Product.ID
                    join User in this.Entities.Users on CartItem.UserID equals User.ID
                    where CartItem.UserID == UserID
                    select new CommonLayer.Models.CartItemsModel()

                    {
                        ID = CartItem.ID,
                        ProductID = Product.ID,
                        ProductName = Product.Name,
                        ProductPrice = (float)Product.Price,
                        ProductVATRate = (float)Product.VATRate,
                        Quantity = CartItem.Quantity,
                        UserID = User.ID
                    });
        }

        public void AddCartItem(CommonLayer.CartItem CartItem)
        {
            this.Entities.CartItems.Add(CartItem);
            this.Entities.SaveChanges();
        }

        public CommonLayer.CartItem GetCartItem(Guid ID)
        {
            return this.Entities.CartItems.SingleOrDefault(ci => ci.ID == ID);
        }

        public CommonLayer.CartItem GetCartItem(Guid UserID, Guid ProductID)
        {
            return this.Entities.CartItems.SingleOrDefault(ci => ci.UserID == UserID && ci.ProductID == ProductID);
        }

        public void UpdateCartItem(Guid ID, int Quantity)
        {
            CommonLayer.CartItem ExistingCartItem = this.GetCartItem(ID);
            CommonLayer.CartItem CartItem = ExistingCartItem;

            CartItem.Quantity = Quantity;

            this.Entities.Entry(ExistingCartItem).CurrentValues.SetValues(CartItem);
            this.Entities.SaveChanges();
        }

        public void UpdateCartItem(CommonLayer.CartItem CartItem)
        {
            CommonLayer.CartItem ExistingCartItem = this.GetCartItem(CartItem.ID);
            this.Entities.Entry(ExistingCartItem).CurrentValues.SetValues(CartItem);
            this.Entities.SaveChanges();
        }

        public void DeleteCartItem(CommonLayer.CartItem CartItem)
        {
            this.Entities.CartItems.Remove(CartItem);
            this.Entities.SaveChanges();
        }
    }
}
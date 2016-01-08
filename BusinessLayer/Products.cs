using System;
using System.Linq;

namespace BusinessLayer
{
    public class Products : BLBase
    {
        public Products() : base() { }
        public Products(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        /// <summary>
        /// Returns a list of products
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Product> GetProducts()
        {
            return new DataLayer.DAProducts(this.Entities).GetProducts();
        }

        /// <summary>
        /// Returns ProductsModel as a model
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Models.ProductsModel> GetProductsAsModel()
        {
            return new DataLayer.DAProducts(this.Entities).GetProductsAsModel();
        }

        /// <summary>
        /// Returns the product categories
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Category> GetProductTypes()
        {
            return new DataLayer.DAProducts(this.Entities).GetProductTypes();
        }

        public IQueryable<CommonLayer.Review> GetProductReviews(Guid ID)
        {
            return new DataLayer.DAProducts(this.Entities).GetProductReviews(ID);
        }

        public IQueryable<CommonLayer.Sale> GetProductSales()
        {
            return new DataLayer.DAProducts(this.Entities).GetProductSales();
        }

        /// <summary>
        /// Adds the product to the database
        /// </summary>
        /// <param name="Product"></param>
        public void AddProductToDatabase(CommonLayer.Product Product)
        {
            Product.ID = Guid.NewGuid();
            new DataLayer.DAProducts(this.Entities).AddNewProduct(Product);
        }

        /// <summary>
        /// Updates the product in the database
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(CommonLayer.Product product)
        {
            if (!string.IsNullOrEmpty(product.Name))
            {
                new DataLayer.DAProducts(this.Entities).UpdateProduct(product);
            }
        }

        /// <summary>
        /// Returns a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommonLayer.Product GetProduct(Guid ID)
        {
            return new DataLayer.DAProducts(this.Entities).GetProduct(ID);
        }

        public bool isProductAvailable(Guid ID)
        {
            CommonLayer.Product Product = this.GetProduct(ID);

            if(Product.Quantity < 1)
            {
                return false;
            }

            return true;
        }

        public void DeleteProduct(Guid ID)
        {
            CommonLayer.Product product = this.GetProduct(ID);
            if (product != null)
            {
                new DataLayer.DAProducts(this.Entities).DeleteProduct(product);
            }
        }

        public void AddProductToCart(Guid ProductID, Guid UserID)
        {
            CommonLayer.CartItem ExistingCartItem = new CartItems(this.Entities).GetCartItem(UserID, ProductID);

            if(ExistingCartItem == null)
            {
                CommonLayer.CartItem CartItem = new CommonLayer.CartItem();

                CartItem.ID = Guid.NewGuid();
                CartItem.Quantity = 1;
                CartItem.ProductID = ProductID;
                CartItem.UserID = UserID;

                new DataLayer.DACartItems(this.Entities).AddCartItem(CartItem);
            }
            else
            {
                ExistingCartItem.Quantity++;
                new DataLayer.DACartItems(this.Entities).UpdateCartItem(ExistingCartItem);
            }
        }

        public IQueryable<CommonLayer.Product> Search(string Search)
        {
            return new DataLayer.DAProducts(this.Entities).Search(Search);
        }
    }
}

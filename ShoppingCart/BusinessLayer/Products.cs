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

        public IQueryable<CommonLayer.Product> GetEnabledProducts()
        {
            return new DataLayer.DAProducts(this.Entities).GetEnabledProducts();
        }

        /// <summary>
        /// Returns ProductsModel as a model
        /// </summary>
        /// <returns></returns>
        public IQueryable<CommonLayer.Models.ProductsModel> GetProductsAsModel()
        {
            return new DataLayer.DAProducts(this.Entities).GetProductsAsModel();
        }

        public IQueryable<CommonLayer.Models.ProductsModel> GetEnabledProductsAsModel()
        {
            return new DataLayer.DAProducts(this.Entities).GetEnabledProductsAsModel();
        }

        public IQueryable<CommonLayer.Models.ProductsModel> GetProductsInCategory(string ID)
        {
            return new DataLayer.DAProducts(this.Entities).GetProductsInCategory(ID);
        }

        /// <summary>
        /// Adds the product to the database
        /// </summary>
        /// <param name="Product"></param>
        public void AddProduct(CommonLayer.Product Product, Guid[] UserTypeID, float[] Price)
        {
            Product.ID = Guid.NewGuid();
            new DataLayer.DAProducts(this.Entities).AddNewProduct(Product);

            int num = 0;

            foreach (Guid ID in UserTypeID)
            {
                new ProductPrices(this.Entities).AllocateProductPrice(Product.ID, ID, Price[num++]);
            }
        }

        /// <summary>
        /// Updates the product in the database
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(CommonLayer.Product Product, Guid[] UserTypeID, float[] Price)
        {
            if (!string.IsNullOrEmpty(Product.Name))
            {
                new DataLayer.DAProducts(this.Entities).UpdateProduct(Product);

                int num = 0;
                if(UserTypeID != null && Price != null)
                {
                    ProductPrices ProductPricesBL = new ProductPrices(this.Entities);
                    foreach (Guid ID in UserTypeID)
                    {
                        ProductPricesBL.DeallocateProductPrice(ProductPricesBL.GetProductPrice(Product.ID, ID));
                        ProductPricesBL.AllocateProductPrice(Product.ID, ID, Price[num++]);
                        new Email(this.Entities).SendEmailToAdmin("Product Stock Changed", "Product ID" + Product.ID+ ", stock now: " + Product.Quantity);
                    }
                }
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

        public bool isProductOnSale(Guid ID)
        {
            CommonLayer.Product Product = this.GetProduct(ID);

            if(Product.SaleID != null)
            {
                return true;
            }

            return false;
        }

        public void DeleteProduct(Guid ID)
        {
            CommonLayer.Product Product = this.GetProduct(ID);
            if (Product != null)
            {
                foreach(CommonLayer.ProductPrice ProductPrice in Product.ProductPrices.ToList())
                {
                    new ProductPrices(this.Entities).DeallocateProductPrice(ProductPrice);
                }
                new DataLayer.DAProducts(this.Entities).DeleteProduct(Product);
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

        public void IncreaseQuantity(Guid ID, int Quantity)
        {
            CommonLayer.Product Product = this.GetProduct(ID);
            Product.Quantity += Quantity;

            new DataLayer.DAProducts().UpdateProduct(Product);
        }

        public void DecreaseQuantity(Guid ID, int Quantity)
        {
            CommonLayer.Product Product = this.GetProduct(ID);
            Product.Quantity -= Quantity;

            new DataLayer.DAProducts().UpdateProduct(Product);
        }
    }
}

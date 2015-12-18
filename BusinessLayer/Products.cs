using System;
using System.Linq;

namespace BusinessLayer
{
    public class Products : BLBase
    {
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

        /// <summary>
        /// Adds the product to the database
        /// </summary>
        /// <param name="Product"></param>
        public void AddProductToDatabase(CommonLayer.Product Product)
        {
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
        public CommonLayer.Product GetProduct(Guid id)
        {
            return new DataLayer.DAProducts(this.Entities).GetProduct(id);
        }
    }
}

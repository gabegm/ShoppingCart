using System;
using System.Linq;

namespace DataLayer
{
    public class DAProducts : ConnectionClass
    {
        public DAProducts() : base() { }
        public DAProducts(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Models.ProductsModel> GetProductsAsModel()
        {
            return (from p
                    in this.Entities.Products
                    join types in this.Entities.Categories on p.CategoryID equals types.ID
                    select new CommonLayer.Models.ProductsModel()
                    {
                        CategoryID = types.ID,
                        CategoryName = types.Name,
                        Name = p.Name,
                        ID = p.ID
                    });
        }

        public void AddNewProduct(CommonLayer.Product product)
        {
            this.Entities.Products.Add(product);
            this.Entities.SaveChanges();
        }

        public IQueryable<CommonLayer.Category> GetProductTypes()
        {
            return this.Entities.Categories;
        }

        public CommonLayer.Product GetProduct(Guid id)
        {
            return this.Entities.Products.SingleOrDefault(p => p.ID.Equals(id));
        }

        public void UpdateProduct(CommonLayer.Product product)
        {
            CommonLayer.Product ExistingProduct = this.GetProduct(product.ID);
            this.Entities.Entry(ExistingProduct).CurrentValues.SetValues(product);
            this.Entities.SaveChanges();
        }

        public void DeleteProduct(CommonLayer.Product Product)
        {
            this.Entities.Products.Remove(Product);
            this.Entities.SaveChanges();
        }
    }
}
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace DataLayer
{
    public class DAProducts : ConnectionClass
    {
        public DAProducts() : base() { }
        public DAProducts(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Product> GetProducts()
        {
            return this.Entities.Products;
        }

        public IQueryable<CommonLayer.Models.ProductsModel> GetProductsAsModel()
        {
            return (from product in this.Entities.Products
                    join category in this.Entities.Categories on product.CategoryID equals category.ID
                    join sale in this.Entities.Sales on product.SaleID equals sale.ID
                    join review in this.Entities.Reviews on product.ID equals review.ProductID into pr
                    from subreview in pr.DefaultIfEmpty()
                    select new CommonLayer.Models.ProductsModel()
                    {
                        ID = product.ID,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        Price = (float)product.Price,
                        VATRate = (float)product.VATRate,
                        Quantity = product.Quantity,
                        Active = product.Active,
                        CategoryID = category.ID,
                        CategoryName = category.Name,
                        SaleID = sale.ID,
                        SaleValue = (float)sale.Value,
                        SaleStart = sale.Start,
                        SaleStop = sale.Stop,
                        ReviewID = (subreview == null ? Guid.Empty : subreview.ID),
                        ReviewDescription = (subreview == null ? String.Empty : subreview.Description),
                        ReviewRating = (subreview == null ? 0 : subreview.Rating),
                        ReviewDate = (subreview == null ? default(DateTime) : subreview.Date)
                    });
        }

        public void AddNewProduct(CommonLayer.Product product)
        {
            try
            {
                this.Entities.Products.Add(product);
                this.Entities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public IQueryable<CommonLayer.Category> GetProductTypes()
        {
            return this.Entities.Categories;
        }

        public IQueryable<CommonLayer.Sale> GetProductSales()
        {
            return this.Entities.Sales;
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
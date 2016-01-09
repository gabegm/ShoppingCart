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

        public IQueryable<CommonLayer.Product> GetEnabledProducts()
        {
            return (from Product in this.Entities.Products
                    where Product.Active == true
                    select Product
                    );
        }

        public IQueryable<CommonLayer.Models.ProductsModel> GetProductsAsModel()
        {
            return (from product in this.Entities.Products
                    join category in this.Entities.Categories on product.CategoryID equals category.ID
                    join sale in this.Entities.Sales on product.SaleID equals sale.ID into ps
                    from subsale in ps.DefaultIfEmpty()
                    join review in this.Entities.Reviews on product.ID equals review.ProductID into pr
                    from subreview in pr.DefaultIfEmpty()
                    select new CommonLayer.Models.ProductsModel()
                    {
                        ID = product.ID,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        VATRate = (float)product.VATRate,
                        Quantity = product.Quantity,
                        Active = product.Active,
                        CategoryID = category.ID,
                        CategoryName = category.Name,
                        SaleID = (subsale == null ? Guid.Empty : subsale.ID),
                        SaleValue = (float)(subsale == null ? 0 : subsale.Value),
                        SaleStart = (subsale == null ? default(DateTime) : subsale.Start),
                        SaleStop = (subsale == null ? default(DateTime) : subsale.Stop),
                        ReviewID = (subreview == null ? Guid.Empty : subreview.ID),
                        ReviewDescription = (subreview == null ? String.Empty : subreview.Description),
                        ReviewRating = (subreview == null ? 0 : subreview.Rating),
                        ReviewDate = (subreview == null ? default(DateTime) : subreview.Date)
                    });
        }

        public IQueryable<CommonLayer.Models.ProductsModel> GetEnabledProductsAsModel()
        {
            return (from Product in this.Entities.Products
                    join Category in this.Entities.Categories on Product.CategoryID equals Category.ID
                    join Sale in this.Entities.Sales on Product.SaleID equals Sale.ID into ps
                    from subsale in ps.DefaultIfEmpty()
                    join Review in this.Entities.Reviews on Product.ID equals Review.ProductID into pr
                    from subreview in pr.DefaultIfEmpty()
                    where Product.Active == true
                    select new CommonLayer.Models.ProductsModel()
                    {
                        ID = Product.ID,
                        Name = Product.Name,
                        Description = Product.Description,
                        ImageURL = Product.ImageURL,
                        VATRate = (float)Product.VATRate,
                        Quantity = Product.Quantity,
                        Active = Product.Active,
                        CategoryID = Category.ID,
                        CategoryName = Category.Name,
                        SaleID = (subsale == null ? Guid.Empty : subsale.ID),
                        SaleValue = (float)(subsale == null ? 0 : subsale.Value),
                        SaleStart = (subsale == null ? default(DateTime) : subsale.Start),
                        SaleStop = (subsale == null ? default(DateTime) : subsale.Stop),
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

        public IQueryable<CommonLayer.Product> Search(string Search)
        {
            return (from Product in this.Entities.Products
                    where Product.Name.Contains(Search) || Product.Description.Contains(Search)
                    select Product
                    );
        }

        public IQueryable<CommonLayer.Category> GetProductTypes()
        {
            return this.Entities.Categories;
        }

        public IQueryable<CommonLayer.Review> GetProductReviews(Guid ID)
        {
            return (from review in this.Entities.Reviews
                    where review.ProductID == ID
                    select review
                    );
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class DACategories : ConnectionClass
    {
        public DACategories(): base() { }
        public DACategories(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public IQueryable<CommonLayer.Category> GetCategories()
        {
            return this.Entities.Categories;
        }

        public IQueryable<CommonLayer.Models.CategoriesModel> GetCategoriesAsModel()
        {
            return (from Category in this.Entities.Categories
                    join ParentCategory in this.Entities.Categories on Category.ParentID equals ParentCategory.ID into cs
                    from Subcategory in cs.DefaultIfEmpty()
                    select new CommonLayer.Models.CategoriesModel()
                    {
                        ID = Category.ID,
                        Name = Category.Name,
                        ParentID = Category.ParentID,
                        ParentName = (Subcategory == null ? String.Empty : Subcategory.Name)
                    });
        }

        public IQueryable<CommonLayer.Models.CategoriesModel> GetParentCategoriesAsModel()
        {
            return (from Category in this.Entities.Categories
                    join ParentCategory in this.Entities.Categories on Category.ParentID equals ParentCategory.ID into cs
                    where Category.ParentID == null
                    from Subcategory in cs.DefaultIfEmpty()
                    select new CommonLayer.Models.CategoriesModel()
                    {
                        ID = Category.ID,
                        Name = Category.Name,
                        ParentID = Category.ParentID,
                        ParentName = (Subcategory == null ? String.Empty : Subcategory.Name)
                    });
        }

        public IQueryable<CommonLayer.Models.CategoriesModel> GetSubCategoriesAsModel()
        {
            return (from Category in this.Entities.Categories
                    join ParentCategory in this.Entities.Categories on Category.ParentID equals ParentCategory.ID into cs
                    where Category.ParentID != null
                    from Subcategory in cs.DefaultIfEmpty()
                    select new CommonLayer.Models.CategoriesModel()
                    {
                        ID = Category.ID,
                        Name = Category.Name,
                        ParentID = Category.ParentID,
                        ParentName = (Subcategory == null ? String.Empty : Subcategory.Name)
                    });
        }

        public CommonLayer.Category GetCategory(string ID)
        {
            return this.Entities.Categories.SingleOrDefault(c => c.ID == ID);
        }

        public void AddCategory(CommonLayer.Category category)
        {
            this.Entities.Categories.Add(category);
            this.Entities.SaveChanges();
        }

        public void UpdateCategory(CommonLayer.Category category)
        {
            CommonLayer.Category ExistingCategory = this.GetCategory(category.ID);
            this.Entities.Entry(ExistingCategory).CurrentValues.SetValues(category);
            this.Entities.SaveChanges();
        }

        public void DeleteCategory(CommonLayer.Category Category)
        {
            this.Entities.Categories.Remove(Category);
            this.Entities.SaveChanges();
        }
    }
}
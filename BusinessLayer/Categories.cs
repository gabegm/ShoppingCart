using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Categories : BLBase
    {
        public Categories() : base() { }

        public IQueryable<CommonLayer.Category> GetCategories()
        {
            return new DataLayer.DACategories(this.Entities).GetCategories();
        }

        public IQueryable<CommonLayer.Models.CategoriesModel> GetParentCategoriesAsModel()
        {
            return new DataLayer.DACategories(this.Entities).GetParentCategoriesAsModel();
        }

        public IQueryable<CommonLayer.Models.CategoriesModel> GetCategoriesAsModel()
        {
            return new DataLayer.DACategories(this.Entities).GetCategoriesAsModel();
        }

        public void AddCategoryToDatabase(CommonLayer.Category Category)
        {
            Category.ID = Guid.NewGuid().ToString();
            new DataLayer.DACategories(this.Entities).AddCategory(Category);
        }

        public void UpdateCategory(CommonLayer.Category category)
        {
            if (!string.IsNullOrEmpty(category.Name))
            {
                new DataLayer.DACategories(this.Entities).UpdateCategory(category);
            }
        }

        public CommonLayer.Category GetCategory(string ID)
        {
            return new DataLayer.DACategories(this.Entities).GetCategory(ID);
        }

        public void DeleteCategory(string ID)
        {
            CommonLayer.Category category = this.GetCategory(ID);
            if (category != null)
            {
                new DataLayer.DACategories(this.Entities).DeleteCategory(category);
            }
        }
    }
}
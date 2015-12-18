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

        public CommonLayer.Category GetCategory(Guid ID)
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
    }
}
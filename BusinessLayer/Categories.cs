﻿using System;
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

        public void AddCategoryToDatabase(CommonLayer.Category Category)
        {
            new DataLayer.DACategories(this.Entities).AddCategory(Category);

        }

        public void UpdateCategory(CommonLayer.Category category)
        {
            if (!string.IsNullOrEmpty(category.Name))
            {
                new DataLayer.DACategories(this.Entities).UpdateCategory(category);
            }
        }
    }
}
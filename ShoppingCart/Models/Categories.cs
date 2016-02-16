using System.Linq;

namespace ShoppingCart.Models
{
    public class Categories
    {
        public IQueryable<CommonLayer.Models.CategoriesModel> SubCategories;
        public IQueryable<CommonLayer.Models.CategoriesModel> ParentCategories;
    }
}
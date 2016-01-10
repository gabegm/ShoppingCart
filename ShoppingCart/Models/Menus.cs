using System.Linq;

namespace ShoppingCart.Models
{
    public class Menus
    {
        public IQueryable<CommonLayer.Models.MenusModel> SubMenus;
        public IQueryable<CommonLayer.Models.MenusModel> ParentMenus;
        public IQueryable<CommonLayer.Models.CartItemsModel> CartItems;
    }
}
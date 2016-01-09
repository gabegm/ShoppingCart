using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            new BusinessLayer.Audits().AddAudit(Guid.Empty, "Successful Guest Visit", "Visit");

            Models.UserTypesProductPrices UserTypesProductPrices = new Models.UserTypesProductPrices();

            if(HttpContext.User.Identity.IsAuthenticated == true)
            {
                CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);
                UserTypesProductPrices.UserType = new BusinessLayer.UserTypes().GetUserType(User.UserTypeID);
            }
            else
            {
                UserTypesProductPrices.UserType = new BusinessLayer.UserTypes().GetUserType("Client");
            }

            UserTypesProductPrices.Products = new BusinessLayer.Products().GetEnabledProductsAsModel();
            UserTypesProductPrices.ProductPrices = new BusinessLayer.ProductPrices().GetProductPrices();

            return View(UserTypesProductPrices);
        }

        [ChildActionOnly]
        public ActionResult RenderMenus()
        {
            Models.Menus Menus = new Models.Menus();

            Menus.SubMenus = new BusinessLayer.Menus().GetSubMenusAsModel();
            Menus.ParentMenus = new BusinessLayer.Menus().GetParentMenusAsModel();

            return PartialView("~/Views/_Shared/_MenuNav.cshtml", Menus);
        }

        [ChildActionOnly]
        public ActionResult RenderCategories()
        {
            Models.Categories Categories = new Models.Categories();

            Categories.SubCategories = new BusinessLayer.Categories().GetSubCategoriesAsModel();
            Categories.ParentCategories = new BusinessLayer.Categories().GetParentCategoriesAsModel();

            return PartialView("~/Views/_Shared/_CategoryNav.cshtml", Categories);
        }
    }
}
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

            return View(new BusinessLayer.Products().GetEnabledProductsAsModel());
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
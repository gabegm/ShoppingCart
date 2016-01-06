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
            ViewBag.Menus = new BusinessLayer.Menus().GetMenus();
            ViewBag.ParentMenus = new BusinessLayer.Menus().GetParentMenusAsModel();

            ViewBag.Categories = new BusinessLayer.Categories().GetCategories();
            ViewBag.ParentCategories = new BusinessLayer.Categories().GetParentCategoriesAsModel();

            return View(new BusinessLayer.Products().GetProductsAsModel());
        }
    }
}
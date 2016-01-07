using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(string ID)
        {
            return View(new BusinessLayer.Categories().GetProductsInCategory(ID));
        }
    }
}
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
            BusinessLayer.Products pr = new BusinessLayer.Products();
            return View(pr.GetProductsAsModel());
        }
    }
}
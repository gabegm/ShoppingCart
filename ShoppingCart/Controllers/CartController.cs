using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            BusinessLayer.Carts c = new BusinessLayer.Carts();
            return View(c/*.GetCartProducts()*/);
        }
    }
}
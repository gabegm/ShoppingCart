using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);

            return View(new BusinessLayer.Orders().GetOrdersAsModel(User.ID));
        }
    }
}
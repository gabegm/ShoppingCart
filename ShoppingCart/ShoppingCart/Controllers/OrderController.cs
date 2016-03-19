using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = "USR")]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);

            return View(new BusinessLayer.Orders().GetOrdersAsModel(User.ID));
        }

        public ActionResult Detail(Guid ID)
        {
            CommonLayer.OrderDetail OrderDetail = new BusinessLayer.Orders().GetOrderDetail(ID);
            CommonLayer.Order Order = new BusinessLayer.Orders().GetOrder(OrderDetail.OrderID);

            Models.ViewOrder ViewOrder = new Models.ViewOrder();
            ViewOrder.Order = Order;
            ViewOrder.OrderDetail = OrderDetail;

            return View(ViewOrder);
        }
    }
}
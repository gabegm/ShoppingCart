using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProcessOrder()
        {
            CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);

            foreach (CommonLayer.CartItem CartItem in new BusinessLayer.CartItems().GetUserCartItems(User))
            {
                new BusinessLayer.Orders().AddOrder(CartItem);
                new BusinessLayer.CartItems().DeleteCartItem(CartItem.ID);

                BusinessLayer.Email EmailBL = new BusinessLayer.Email();
                EmailBL.SendEmailToCustomer(User.Email, "Order has been placed", "Congratulations, your order has been placed. You shall be notified when said order is dispatched.");
                EmailBL.SendEmailToAdmin("Order has been placed", "");
            }

            return RedirectToAction("Index", "Order");
        }
    }
}
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

        public ActionResult Checkout()
        {
            CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);

            foreach (CommonLayer.CartItem CartItem in new BusinessLayer.CartItems().GetUserCartItems(User.ID))
            {
                new BusinessLayer.Orders().AddOrder(CartItem);
                new BusinessLayer.CartItems().DeleteCartItem(CartItem.ID);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
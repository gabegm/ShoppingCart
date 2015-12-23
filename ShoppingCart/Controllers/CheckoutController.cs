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
    }
}
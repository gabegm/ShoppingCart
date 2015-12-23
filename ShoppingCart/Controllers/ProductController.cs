using System;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = "ADM")]
    public class ProductsController : Controller
    {
        //
        // GET: /Products/
        public ActionResult Index()
        {
            BusinessLayer.Products pr = new BusinessLayer.Products();
            return View(pr.GetProductsAsModel());
        }

        [HttpGet]
        public ActionResult Detail(Guid ID)
        {
            //BusinessLayer.Products p = new BusinessLayer.Products();
            return View(new BusinessLayer.Products().GetProduct(ID));
        }

        public ActionResult AddProductToCart(Guid ProductID, Guid UserID)
        {
            CommonLayer.User User = new BusinessLayer.Users().GetUser(UserID);
            new BusinessLayer.Products().AddProductToCart(ProductID, UserID);
            return RedirectToAction("Index");
        }

    }
}
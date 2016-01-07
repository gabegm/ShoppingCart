using System;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Products/
        [HttpGet]
        public ActionResult Index(Guid ID)
        {
            Models.ProductReviews ProductReviews = new Models.ProductReviews();
            BusinessLayer.Products pr = new BusinessLayer.Products();

            ProductReviews.Product = pr.GetProduct(ID);
            ProductReviews.Reviews = pr.GetProductReviews(ID);

            return View(ProductReviews);
        }

        public ActionResult AddProductToCart(Guid ID)
        {
            CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);
            new BusinessLayer.Products().AddProductToCart(ID, User.ID);
            return RedirectToAction("Index", "Cart");
        }
        
    }
}
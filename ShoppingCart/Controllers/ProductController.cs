using System;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        //
        // GET: /Products/
        [AllowAnonymous]
        public ActionResult Index(Guid ID)
        {
            Models.ProductReviews ProductReviews = new Models.ProductReviews();
            BusinessLayer.Products ProductsBL = new BusinessLayer.Products();

            ProductReviews.Product = ProductsBL.GetProduct(ID);
            ProductReviews.Reviews = new BusinessLayer.Reviews().GetReviews(ID);

            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);
                ProductReviews.UserType = new BusinessLayer.UserTypes().GetUserType(User.UserTypeID);
            }
            else
            {
                ProductReviews.UserType = new BusinessLayer.UserTypes().GetUserType("Client");
            }

            ProductReviews.ProductPrices = new BusinessLayer.ProductPrices().GetProductPrices();

            return View(ProductReviews);
        }

        [AllowAnonymous]
        public ActionResult AddProductToCart(Guid ID)
        {
            BusinessLayer.Products ProductsBL = new BusinessLayer.Products();
            if (ProductsBL.isProductAvailable(ID))
            {
                CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);
                new BusinessLayer.Products().AddProductToCart(ID, User.ID);
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                return null;
            }
        }
        
    }
}
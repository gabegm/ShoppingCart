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
            //BusinessLayer.Products pr = new BusinessLayer.Products();
            return View(new BusinessLayer.Products().GetProductsAsModel());
        }

        [HttpGet]
        public ActionResult Detail(Guid ID)
        {
            //BusinessLayer.Products p = new BusinessLayer.Products();
            return View(new BusinessLayer.Products().GetProduct(ID));
        }

        public ActionResult AddProductToCart(Guid ID)
        {
            CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);
            new BusinessLayer.Products().AddProductToCart(ID, User.ID);
            return RedirectToAction("Index");
        }
        
    }
}
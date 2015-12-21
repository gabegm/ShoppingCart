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
        public ActionResult Detail(int id)
        {
            Models.ProductsBL prDet = new Models.ProductsBL();
            Models.UiModels.Product product = prDet.GetProduct(id);
            return View(product);
        }

        /*[HttpPost]
        public ActionResult AddProductToCart(Guid ProductID, Guid UserID)
        {
            BusinessLayer.Products p = new BusinessLayer.Products();
            p.AddProductToCart(ProductID, UserID);
            return RedirectToAction("Index")
        }*/

    }
}
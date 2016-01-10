using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);
            BusinessLayer.CartItems CartItemsBL = new BusinessLayer.CartItems();

            ViewBag.TotalPrice = CartItemsBL.GetTotalPrice(User);
            ViewBag.GetTotalVAT = CartItemsBL.GetTotalVAT(User);
            ViewBag.TotalVATPrice = CartItemsBL.GetTotalVATPrice(User);

            return View(CartItemsBL.GetUserCartItemsAsModel(User));
        }

        /// <summary>
        /// Adds cart items to checkout
        /// </summary>
        /// <param name="Cart"></param>
        /// <returns></returns>
        public ActionResult Checkout()
        {
            return RedirectToAction("Index", "Checkout");
        }

        /// <summary>
        /// Edit cart item quantity
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateCartItem(Guid ID, int Quantity)
        {
            new BusinessLayer.CartItems().UpdateCartItem(ID, Quantity);
            return RedirectToAction("Index", "Cart");
        }

        /// <summary>
        /// Deletes a specigic category from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteCartItem(Guid ID)
        {
            new BusinessLayer.CartItems().DeleteCartItem(ID);
            return RedirectToAction("Index");
        }
    }
}
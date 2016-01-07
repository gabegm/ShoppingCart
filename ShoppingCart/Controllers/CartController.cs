﻿using System;
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
            return View(new BusinessLayer.CartItems().GetCartProductsAsModel());
        }

        /// <summary>
        /// Adds cart items to checkout
        /// </summary>
        /// <param name="Cart"></param>
        /// <returns></returns>
        public ActionResult Checkout(CommonLayer.CartItem Cart)
        {
            new BusinessLayer.Orders().AddOrder(Cart);
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
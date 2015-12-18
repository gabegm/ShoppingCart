using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/
        public ActionResult Index()
        {
            BusinessLayer.Users us = new BusinessLayer.Users();
            return View(us.GetUsers());
        }

        /// <summary>
        /// Deletes a specific user from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteUser(Guid id)
        {
            BusinessLayer.Users us = new BusinessLayer.Users();
            us.DeleteUser(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns the user's details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserDetails(Guid id)
        {
            BusinessLayer.Users users = new BusinessLayer.Users();
            return View(users.GetUser(id));
        }

        /// <summary>
        /// Updates the user's details
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserDetails(CommonLayer.User User)
        {
            BusinessLayer.Users us = new BusinessLayer.Users();
            us.UpdateUser(User);
            return RedirectToAction("Index");
        }
    }
}
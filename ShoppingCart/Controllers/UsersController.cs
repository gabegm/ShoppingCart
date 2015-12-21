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
        public ActionResult DeleteUser(Guid UserID, Guid UserAccountID)
        {
            BusinessLayer.Users u = new BusinessLayer.Users();
            u.DeleteUser(UserID, UserAccountID);
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
        public ActionResult UserDetails(CommonLayer.User User, CommonLayer.UserAccount UserAccount)
        {
            BusinessLayer.Users u = new BusinessLayer.Users();
            u.UpdateUser(User, UserAccount);
            return RedirectToAction("Index");
        }
    }
}
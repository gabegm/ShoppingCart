using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(CommonLayer.User User, CommonLayer.UserAccount UserAccount, string ConfirmPassword)
        {
            BusinessLayer.Users u = new BusinessLayer.Users();
            u.RegisterUser(User, UserAccount, ConfirmPassword);
            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (TempData["LoginInvalid"] != null)
            {
                ViewBag.LoginInvalid = true;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string UserEmail, string Password)
        {
            BusinessLayer.Users us = new BusinessLayer.Users();
            if (us.Login(UserEmail, Password) == true)
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(UserEmail, true);
                CommonLayer.User user = us.GetUser(UserEmail);
                ShoppingCart.Models.UIHelpers.UserFullName = user.FirstName + " " + user.LastName;
                return RedirectToAction("Index", "Home");

            }
            TempData["LoginInvalid"] = true;
            return RedirectToAction("Login", "Account");
        }
    }
}
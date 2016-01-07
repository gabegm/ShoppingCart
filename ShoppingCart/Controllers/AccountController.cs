using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            return View("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            List<string> GenderItems = new List<string>() { "Male", "Female" };
            ViewBag.Gender = GenderItems.Select(gender => new SelectListItem { Text = gender, Value = gender });

            BusinessLayer.Users u = new BusinessLayer.Users();

            List<SelectListItem> TownItems = (from towns in u.GetUserTowns().ToList()
                                              select new SelectListItem()
                                              {
                                                  Text = towns.Name,
                                                  Value = towns.ID.ToString()
                                              }).ToList();
            ViewBag.TownName = TownItems;

            List<SelectListItem> CountryItems = (from countries in u.GetUserCountries().ToList()
                                                 select new SelectListItem()
                                                 {
                                                     Text = countries.Name,
                                                     Value = countries.ID.ToString()
                                                 }).ToList();
            ViewBag.CountryName = CountryItems;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(CommonLayer.User User, CommonLayer.UserAccount UserAccount, string ConfirmPassword)
        {
            Guid RoleID = new BusinessLayer.Roles().GetRole("USR").ID; //Default user role
            UserAccount.Active = true; //Default useraccount is enabled

            User.Active = true; //Default user is enabled

            CommonLayer.UserType UserType = new BusinessLayer.UserTypes().GetUserType("Client");
            User.UserTypeID = UserType.ID; //Default UserType Client

            new BusinessLayer.Users().RegisterUser(User, UserAccount, ConfirmPassword, null, RoleID);

            return RedirectToAction("Login", "Account");
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
        public ActionResult Login(string Email, string Password)
        {
            BusinessLayer.Users UsersBL = new BusinessLayer.Users();
            if (UsersBL.Login(Email, Password) == true)
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(Email, true);

                CommonLayer.User user = UsersBL.GetUser(Email);
                Models.UIHelpers.UserFullName = user.FirstName + " " + user.LastName;

                return RedirectToAction("Index", "Home");
            }
            TempData["LoginInvalid"] = true;
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
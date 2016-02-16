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

            List<SelectListItem> TownItems = (from towns in new BusinessLayer.Towns().GetTowns().ToList()
                                              select new SelectListItem()
                                              {
                                                  Text = towns.Name,
                                                  Value = towns.ID.ToString()
                                              }).ToList();
            ViewBag.TownName = TownItems;

            List<SelectListItem> CountryItems = (from countries in new BusinessLayer.Countries().GetCountries().ToList()
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
            new BusinessLayer.Audits().AddAudit(User.ID, "Successful Registration", "Register");

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public JsonResult isEmailAvailable(string Email)
        {
            var user = new BusinessLayer.Users().GetUser(Email);
            return Json(user == null);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (TempData["LoginInvalid"] != null)
            {
                new BusinessLayer.Audits().AddAudit(Guid.Empty, "Login Failed", "Login");
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

                CommonLayer.User User = UsersBL.GetUser(Email);

                Models.UIHelpers.UserFullName = User.FirstName + " " + User.LastName;

                new BusinessLayer.Audits().AddAudit(User.ID, "Successful Login", "Login");

                return RedirectToAction("Index", "Home");
            }
            TempData["LoginInvalid"] = true;

            return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "USR")]
        public ActionResult Logout()
        {
            CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);

            new BusinessLayer.Audits().AddAudit(User.ID, "Successful logout", "Logout");

            System.Web.Security.FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
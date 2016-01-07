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
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(CommonLayer.User User, CommonLayer.UserAccount UserAccount, string ConfirmPassword)
        {
            UserAccount.Roles.Add(new BusinessLayer.Roles().GetRole("USR")); //Default user role
            new BusinessLayer.Users().RegisterUser(User, UserAccount, ConfirmPassword, null);
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
        public ActionResult Login(string Email, string Password)
        {
            BusinessLayer.Users us = new BusinessLayer.Users();
            if (us.Login(Email, Password) == true)
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(Email, true);
                CommonLayer.User user = us.GetUser(Email);
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
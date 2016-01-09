using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(string ID)
        {
            new BusinessLayer.Audits().AddAudit(Guid.Empty, "Successful Guest Visit", "Visit");

            Models.UserTypesProductPrices UserTypesProductPrices = new Models.UserTypesProductPrices();

            if (HttpContext.User.Identity.IsAuthenticated == true)
            {
                CommonLayer.User User = new BusinessLayer.Users().GetUser(HttpContext.User.Identity.Name);
                UserTypesProductPrices.UserType = new BusinessLayer.UserTypes().GetUserType(User.UserTypeID);
            }
            else
            {
                UserTypesProductPrices.UserType = new BusinessLayer.UserTypes().GetUserType("Client");
            }

            UserTypesProductPrices.Products = new BusinessLayer.Products().GetProductsInCategory(ID);
            UserTypesProductPrices.ProductPrices = new BusinessLayer.ProductPrices().GetProductPrices();

            return View(UserTypesProductPrices);
        }
    }
}
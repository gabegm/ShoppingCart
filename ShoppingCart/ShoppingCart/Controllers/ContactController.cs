using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        // GET: Contact
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}
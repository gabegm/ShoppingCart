using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        // GET: News
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ShowAllPosts()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult GetPost()
        {
            return View();
        }
    }
}
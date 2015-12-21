using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowAllPosts()
        {
            return View();
        }

        public ActionResult GetPost()
        {
            return View();
        }
    }
}
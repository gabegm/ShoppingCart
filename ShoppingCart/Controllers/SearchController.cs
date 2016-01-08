using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string Search)
        {
            return View(new BusinessLayer.Products().Search(Search));
        }
    }
}
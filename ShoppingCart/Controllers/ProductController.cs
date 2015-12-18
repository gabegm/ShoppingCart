using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = "ADM")]
    public class ProductsController : Controller
    {
        //
        // GET: /Products/
        public ActionResult Index()
        {
            BusinessLayer.Products pr = new BusinessLayer.Products();
            return View(pr.GetProductsAsModel());
        }

        public ActionResult Detail(int id)
        {
            ShoppingCart.Models.ProductsBL prDet = new Models.ProductsBL();
            Models.UiModels.Product product = prDet.GetProduct(id);
            return View(product);
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            BusinessLayer.Products pr = new BusinessLayer.Products();
            List<SelectListItem> ProductTypeItems = (from p
                                                         in pr.GetProductTypes().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = p.Name,
                                                         Value = p.ID.ToString()

                                                     }).ToList();
            ViewBag.ProductType = ProductTypeItems;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            BusinessLayer.Products pr = new BusinessLayer.Products();
            List<SelectListItem> ProductTypeItems = (from p
                                                         in pr.GetProductTypes().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = p.Name,
                                                         Value = p.ID.ToString()

                                                     }).ToList();
            CommonLayer.Product Product = pr.GetProduct(id);
            ProductTypeItems.SingleOrDefault(p => p.Value.Equals(Product.CategoryID.ToString())).Selected = true;
            ViewBag.ProductTypeId = ProductTypeItems;
            return View(Product);
        }

        [HttpPost]
        public ActionResult CreateNew(CommonLayer.Product product)
        {
            BusinessLayer.Products pr = new BusinessLayer.Products();
            pr.AddProductToDatabase(product);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CommonLayer.Product product)
        {
            BusinessLayer.Products pr = new BusinessLayer.Products();
            pr.UpdateProduct(product);
            return RedirectToAction("Index");
        }

    }
}
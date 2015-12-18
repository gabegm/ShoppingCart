using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Login to access admin 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Returns all the users from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Users()
        {
            BusinessLayer.Users us = new BusinessLayer.Users();
            return View(us.GetUsers());
        }

        /// <summary>
        /// Displays a form with roles to add a new user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewUser()
        {
            BusinessLayer.Users u = new BusinessLayer.Users();
            List<SelectListItem> RoleItems = (from p
                                                         in u.GetUserRoles().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = p.Name,
                                                         Value = p.ID.ToString()
                                                     }).ToList();
            ViewBag.RoleName = RoleItems;
            return View();
        }

        /// <summary>
        /// Saves the new user in the database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ConfirmPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewUser(CommonLayer.User user, string ConfirmPassword)
        {
            BusinessLayer.Users u = new BusinessLayer.Users();
            u.RegisterUser(user, ConfirmPassword);
            return RedirectToAction("Users");
        }

        /// <summary>
        /// Returns the details to a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewUserDetails(Guid id)
        {
            BusinessLayer.Users users = new BusinessLayer.Users();
            return View(users.GetUser(id));
        }

        /// <summary>
        /// Saves the changes of the user to the database
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ViewUserDetails(CommonLayer.User User)
        {
            BusinessLayer.Users us = new BusinessLayer.Users();
            us.UpdateUser(User);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes a specific user from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUser(Guid id)
        {
            BusinessLayer.Users us = new BusinessLayer.Users();
            us.DeleteUser(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays all the products from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Products()
        {
            BusinessLayer.Products pr = new BusinessLayer.Products();
            return View(pr.GetProductsAsModel());
        }

        /// <summary>
        /// Returns the details of a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewProductDetail(int id) {
            ShoppingCart.Models.ProductsBL prDet = new Models.ProductsBL();
            Models.UiModels.Product product = prDet.GetProduct(id);
            return View(product);
        }

        /// <summary>
        /// Returns list of categories in order to create a new product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewProduct()
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

        /// <summary>
        /// Saves new product to database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewProduct(CommonLayer.Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpPostedFileBase imageFile = Request.Files[0];
                        if (imageFile.ContentLength > 0)
                        {
                            string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(imageFile.FileName);
                            string serverPath = Server.MapPath(@"~\content\images\");
                            imageFile.SaveAs(serverPath + fileName);
                            product.ImageURL = serverPath + fileName;
                        }
                    }
                    BusinessLayer.Products pr = new BusinessLayer.Products();
                    pr.AddProductToDatabase(product);
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Products");
        }

        /// <summary>
        /// Returns list of categories to edit a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditProduct(Guid id)
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

        /// <summary>
        /// Saves edited product to database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditProduct(CommonLayer.Product product)
        {
            BusinessLayer.Products pr = new BusinessLayer.Products();
            pr.UpdateProduct(product);
            return RedirectToAction("Products");
        }

        /// <summary>
        /// Displays all roles from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Roles()
        {
            BusinessLayer.Roles roles = new BusinessLayer.Roles();
            return View(roles.GetRoles());
        }

        /// <summary>
        /// Displays form to create new role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewRole()
        {
            return View();
        }

        /// <summary>
        /// Saves new role to database
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewRole(CommonLayer.Role role)
        {
            BusinessLayer.Roles r = new BusinessLayer.Roles();
            r.AddRoleToDatabase(role);
            return RedirectToAction("Roles");
        }

        /// <summary>
        /// Displays form to edit new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRole(CommonLayer.Role role)
        {
            BusinessLayer.Roles r = new BusinessLayer.Roles();
            r.UpdateRole(role);
            return RedirectToAction("Roles");
        }

        /// <summary>
        /// Deletes a specific role from the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteRole()
        {
            return View();
        }

        /// <summary>
        /// Displays all the categories from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Categories()
        {
            BusinessLayer.Categories categories = new BusinessLayer.Categories();
            return View(categories.GetCategories());
        }
        
        /// <summary>
        /// Returns form to create a new category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewCategory()
        {
            return View();
        }

        /// <summary>
        /// Saves newly created category to database
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewCategory(CommonLayer.Category category)
        {
            BusinessLayer.Categories c = new BusinessLayer.Categories();
            c.AddCategoryToDatabase(category);
            return RedirectToAction("Categories");
        }

        /// <summary>
        /// Returns form to edit specific category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCategory()
        {
            return View();
        }

        /// <summary>
        /// Saves edited category to database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCategory(CommonLayer.Category category)
        {
            BusinessLayer.Categories c = new BusinessLayer.Categories();
            c.UpdateCategory(category);
            return RedirectToAction("Categories");
        }

        /// <summary>
        /// Deletes a specigic category from the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCategory()
        {
            return View();
        }

        /// <summary>
        /// Displays all the countries from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Countries()
        {
            BusinessLayer.Countries countries = new BusinessLayer.Countries();
            return View(countries.GetCountries());
        }

        /// <summary>
        /// Displays a form to create a new country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewCountry()
        {
            return View();
        }

        /// <summary>
        /// Saves new country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewCountry(CommonLayer.Country country)
        {
            BusinessLayer.Countries c = new BusinessLayer.Countries();
            c.AddCountryToDatabase(country);
            return RedirectToAction("Countries");
        }

        /// <summary>
        /// Returns form to edit specific country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCountry()
        {
            return View();
        }

        /// <summary>
        /// Saves edited country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCountry(CommonLayer.Country country)
        {
            BusinessLayer.Countries c = new BusinessLayer.Countries();
            c.UpdateCountry(country);
            return RedirectToAction("Countries");
        }

        /// <summary>
        /// Deletes specific country from the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteCountry()
        {
            return RedirectToAction("Countries");
        }
    }
}
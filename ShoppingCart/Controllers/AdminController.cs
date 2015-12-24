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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Login to access admin 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
            List<SelectListItem> RoleItems = (from roles in u.GetUserRoles().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = roles.Name,
                                                         Value = roles.ID.ToString()
                                                     }).ToList();
            ViewBag.RoleName = RoleItems;
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

        /// <summary>
        /// Saves the new user in the database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ConfirmPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewUser(CommonLayer.User User, CommonLayer.UserAccount UserAccount, string ConfirmPassword)
        {
            BusinessLayer.Users u = new BusinessLayer.Users();
            u.RegisterUser(User, UserAccount, ConfirmPassword);
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
        public ActionResult ViewUserDetails(CommonLayer.User User, CommonLayer.UserAccount UserAccount)
        {
            BusinessLayer.Users u = new BusinessLayer.Users();
            u.UpdateUser(User, UserAccount);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Deletes a specific user from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUser(Guid UserID, Guid UserAccountID)
        {
            BusinessLayer.Users u = new BusinessLayer.Users();
            u.DeleteUser(UserID, UserAccountID);
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
                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {
                        HttpPostedFileBase imageFile = Request.Files[0];
                        product.ID = Guid.NewGuid();
                        string fileName = product.ID + System.IO.Path.GetExtension(imageFile.FileName);
                        string serverPath = Server.MapPath(@"~\Images\");
                        imageFile.SaveAs(serverPath + fileName);
                        product.ImageURL = @"\images\" + fileName;
                        BusinessLayer.Products p = new BusinessLayer.Products();
                        p.AddProductToDatabase(product);
                    }
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
        /// Returns the details of a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ViewProductDetails(int id) {
            ShoppingCart.Models.ProductsBL prDet = new Models.ProductsBL();
            Models.UiModels.Product product = prDet.GetProduct(id);
            return View(product);
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

        public ActionResult DeleteProduct(Guid ID)
        {
            BusinessLayer.Products p = new BusinessLayer.Products();
            p.DeleteProduct(ID);
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
        public ActionResult DeleteRole(Guid ID)
        {
            BusinessLayer.Roles r = new BusinessLayer.Roles();
            r.DeleteRole(ID);
            return RedirectToAction("Roles");
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
            BusinessLayer.Categories c = new BusinessLayer.Categories();
            List<SelectListItem> SubCategoryItems = (from category in c.GetCategories().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = category.Name,
                                                         Value = category.ID.ToString()
                                                     }).ToList();
            ViewBag.Subcategories = SubCategoryItems;
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
        public ActionResult EditCategory(string ID)
        {
            BusinessLayer.Categories c = new BusinessLayer.Categories();
            List<SelectListItem> SubcategoryItems = (from category in c.GetCategories().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = category.Name,
                                                         Value = category.ParentID.ToString()
                                                     }).ToList();
            CommonLayer.Category Category = c.GetCategory(ID);
            SubcategoryItems.SingleOrDefault(p => p.Value.Equals(Category.ParentID.ToString())).Selected = true;
            ViewBag.Subcategories = SubcategoryItems;
            return View(Category);
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
        public ActionResult DeleteCategory(string ID)
        {
            BusinessLayer.Categories c = new BusinessLayer.Categories();
            c.DeleteCategory(ID);
            return RedirectToAction("Categories");
        }

        /// <summary>
        /// Displays all the countries from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Towns()
        {
            BusinessLayer.Towns towns = new BusinessLayer.Towns();
            return View(towns.GetTownsAsModel());
        }

        /// <summary>
        /// Displays a form to create a new country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewTown()
        {
            BusinessLayer.Towns t = new BusinessLayer.Towns();
            List<SelectListItem> TownLocationItems = (from towns in t.GetCountries().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = towns.Name,
                                                         Value = towns.ID.ToString()
                                                     }).ToList();
            ViewBag.Countries = TownLocationItems;
            return View();
        }

        /// <summary>
        /// Saves new country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewTown(CommonLayer.Town town)
        {
            BusinessLayer.Towns t = new BusinessLayer.Towns();
            t.AddTownToDatabase(town);
            return RedirectToAction("Towns");
        }

        /// <summary>
        /// Returns form to edit specific country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditTown()
        {
            return View();
        }

        /// <summary>
        /// Saves edited country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditTown(CommonLayer.Town town)
        {
            BusinessLayer.Towns t = new BusinessLayer.Towns();
            t.UpdateTown(town);
            return RedirectToAction("Towns");
        }

        /// <summary>
        /// Deletes specific country from the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteTown(Guid ID)
        {
            BusinessLayer.Towns t = new BusinessLayer.Towns();
            t.DeleteTown(ID);
            return RedirectToAction("Towns");
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
        public ActionResult DeleteCountry(string name)
        {
            BusinessLayer.Countries c = new BusinessLayer.Countries();
            c.DeleteCountry(name);
            return RedirectToAction("Countries");
        }

        [HttpGet]
        public ActionResult Orders()
        {
            return View(new BusinessLayer.Orders().GetOrdersAsModel());
        }
    }
}
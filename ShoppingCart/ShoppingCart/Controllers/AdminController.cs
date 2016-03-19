using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = "ADM")]
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("Audits");
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
            return View(new BusinessLayer.Users().GetUsers());
        }

        /// <summary>
        /// Displays a form with roles to add a new user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewUser()
        {
            List<string> IsUserActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsUserActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            List<string> GenderItems = new List<string>() { "Male", "Female" };
            ViewBag.Gender = GenderItems.Select(gender => new SelectListItem { Text = gender, Value = gender });

            BusinessLayer.Users u = new BusinessLayer.Users();
            List<SelectListItem> RoleItems = (from roles in new BusinessLayer.Roles().GetRoles().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = roles.Name,
                                                         Value = roles.ID.ToString()
                                                     }).ToList();
            ViewBag.RoleName = new MultiSelectList(RoleItems, "Value", "Text");

            List<SelectListItem> UserTypeItems = (from usertypes in u.GetUserTypes().ToList()
                                              select new SelectListItem()
                                              {
                                                  Text = usertypes.Name,
                                                  Value = usertypes.ID.ToString()
                                              }).ToList();
            ViewBag.UserTypes = new MultiSelectList(UserTypeItems, "Value", "Text");

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

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="User"></param>
        /// <param name="UserAccount"></param>
        /// <param name="ConfirmPassword"></param>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewUser(CommonLayer.User User, CommonLayer.UserAccount UserAccount, string ConfirmPassword, Guid[] RoleID, string Active)
        {
            User.Active = bool.Parse(Active);
            UserAccount.Active = bool.Parse(Active);

            new BusinessLayer.Users().RegisterUser(User, UserAccount, ConfirmPassword, RoleID, Guid.Empty);

            return RedirectToAction("Users");
        }

        [HttpGet]
        public ActionResult EditUser(Guid ID)
        {
            Models.UserAccountUser UserAccountUser = new Models.UserAccountUser();

            BusinessLayer.Users u = new BusinessLayer.Users();
            CommonLayer.User User = u.GetUser(ID);
            CommonLayer.UserAccount UserAccount = u.GetUserAccount(User.UserAccountID);
            UserAccountUser.UserAccount = UserAccount;
            UserAccountUser.User = User;

            CommonLayer.Town Town = new BusinessLayer.Towns().GetTown(User.TownID);

            List<string> IsUserActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsUserActive.Select(boolean => new SelectListItem { Selected = (boolean == User.Active.ToString()), Text = boolean, Value = boolean });

            List<string> GenderItems = new List<string>() { "Male", "Female" };
            ViewBag.Gender = GenderItems.Select(gender => new SelectListItem { Selected = (gender == User.Gender), Text = gender, Value = gender });

            List<SelectListItem> RoleItems = (from roles in new BusinessLayer.Roles().GetRoles().ToList()
                                              select new SelectListItem()
                                              {
                                                  Text = roles.Name,
                                                  Value = roles.ID.ToString()
                                              }).ToList();
            ViewBag.RoleName = new MultiSelectList(RoleItems, "Value", "Text");
            foreach(CommonLayer.Role role in new BusinessLayer.Roles().GetUserRoles(UserAccount.ID))
            {
                RoleItems.SingleOrDefault(c => c.Value.Equals(role.ID.ToString())).Selected = true;
            }

            List<SelectListItem> UserTypeItems = (from usertypes in u.GetUserTypes().ToList()
                                                  select new SelectListItem()
                                                  {
                                                      Text = usertypes.Name,
                                                      Value = usertypes.ID.ToString()
                                                  }).ToList();
            ViewBag.UserTypes = UserTypeItems;
            UserTypeItems.SingleOrDefault(ut => ut.Value.Equals(User.UserTypeID.ToString())).Selected = true;

            List<SelectListItem> TownItems = (from towns in new BusinessLayer.Towns().GetTowns().ToList()
                                              select new SelectListItem()
                                              {
                                                  Text = towns.Name,
                                                  Value = towns.ID.ToString()
                                              }).ToList();
            ViewBag.TownName = TownItems;
            TownItems.SingleOrDefault(t => t.Value.Equals(User.TownID.ToString())).Selected = true;

            List<SelectListItem> CountryItems = (from countries in new BusinessLayer.Countries().GetCountries().ToList()
                                                 select new SelectListItem()
                                                 {
                                                     Text = countries.Name,
                                                     Value = countries.ID.ToString()
                                                 }).ToList();
            ViewBag.CountryName = CountryItems;
            CountryItems.SingleOrDefault(c => c.Value.Equals(Town.CountryID.ToString())).Selected = true;

            return View(UserAccountUser);
        }

        /// <summary>
        /// Saves edited product to database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUser(CommonLayer.User User, CommonLayer.UserAccount UserAccount, Guid[] RoleID, string Active)
        {
            User.Active = bool.Parse(Active);
            UserAccount.Active = bool.Parse(Active);

            new BusinessLayer.Users().UpdateUser(User, UserAccount, RoleID);   

            return RedirectToAction("Users");
        }

        /// <summary>
        /// Deletes a specific user from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteUser(Guid ID)
        {
            new BusinessLayer.Users().DeleteUser(ID);
            return RedirectToAction("Users");
        }

        /// <summary>
        /// Displays all the products from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Products()
        {
            Models.ProductsList ProductsList = new Models.ProductsList();

            ProductsList.Products = new BusinessLayer.Products().GetProductsAsModel();
            ProductsList.UserTypes = new BusinessLayer.UserTypes().GetUserTypes();
            ProductsList.ProductPrices = new BusinessLayer.ProductPrices().GetProductPrices();

            return View(ProductsList);
        }

        /// <summary>
        /// Returns list of categories in order to create a new product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewProduct()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            List<SelectListItem> CategoryItems = (from category in new BusinessLayer.Categories().GetSubCategoriesAsModel().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = category.Name,
                                                         Value = category.ID.ToString()
                                                     }).ToList();
            ViewBag.ProductType = CategoryItems;

            List<SelectListItem> SaleItems = (from sale in new BusinessLayer.Sales().GetSales().ToList()
                                                  select new SelectListItem()
                                                  {
                                                      Text = sale.Value.ToString(),
                                                      Value = sale.ID.ToString()
                                                  }).ToList();
            ViewBag.Sales = SaleItems;

            return View();
        }

        /// <summary>
        /// Saves new product to database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewProduct(CommonLayer.Product Product, Guid[] UserTypeID, float[] Price, string Active)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {
                        HttpPostedFileBase imageFile = Request.Files[0];

                        string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(imageFile.FileName);
                        string serverPath = Server.MapPath(@"~\Images\");

                        imageFile.SaveAs(serverPath + fileName);
                        Product.ImageURL = @"\images\" + fileName;
                        Product.Active = bool.Parse(Active);

                        new BusinessLayer.Products().AddProduct(Product, UserTypeID, Price);
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
        /// Returns list of categories to edit a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditProduct(Guid ID)
        {
            BusinessLayer.Products pr = new BusinessLayer.Products();
            CommonLayer.Product Product = pr.GetProduct(ID);

            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Selected = (boolean == Product.Active.ToString()), Text = boolean, Value = boolean });

            List<SelectListItem> ProductTypeItems = (from category in new BusinessLayer.Categories().GetCategories().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = category.Name,
                                                         Value = category.ID.ToString()
                                                     }).ToList();
            
            ProductTypeItems.SingleOrDefault(p => p.Value.Equals(Product.CategoryID.ToString())).Selected = true;
            ViewBag.ProductType = ProductTypeItems;

            List<SelectListItem> SaleItems = (from sale in new BusinessLayer.Sales().GetSales().ToList()
                                              select new SelectListItem()
                                              {
                                                  Text = sale.Value.ToString(),
                                                  Value = sale.ID.ToString()
                                              }).ToList();
            CommonLayer.Sale Sale = new BusinessLayer.Sales().GetSale(Product.SaleID.GetValueOrDefault());
            if (Sale != null)
            {
                SaleItems.SingleOrDefault(s => s.Value.Equals(Product.SaleID.ToString())).Selected = true;
            }
            ViewBag.Sales = SaleItems;

            Models.EditProducts EditProducts = new Models.EditProducts();
            EditProducts.Product = Product;
            EditProducts.ProductPrices = new BusinessLayer.ProductPrices().GetProductPrices();
            EditProducts.UserTypes = new BusinessLayer.UserTypes().GetUserTypes();

            return View(EditProducts);
        }

        /// <summary>
        /// Saves edited product to database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditProduct(CommonLayer.Product Product, Guid[] UserTypeID, float[] Price, string Active)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {
                        HttpPostedFileBase imageFile = Request.Files[0];
                        string fileName = Guid.NewGuid() + System.IO.Path.GetExtension(imageFile.FileName);
                        string serverPath = Server.MapPath(@"~\Images\");
                        imageFile.SaveAs(serverPath + fileName);
                        Product.ImageURL = @"\images\" + fileName;
                        Product.Active = bool.Parse(Active);

                        new BusinessLayer.Products().UpdateProduct(Product, UserTypeID, Price);
                    }
                    else
                    {
                        CommonLayer.Product OldProduct = new BusinessLayer.Products().GetProduct(Product.ID);
                        Product.ImageURL = OldProduct.ImageURL;
                        Product.Active = bool.Parse(Active);

                        new BusinessLayer.Products().UpdateProduct(Product, UserTypeID, Price);
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

        public ActionResult DeleteProduct(Guid ID)
        {
            new BusinessLayer.Products().DeleteProduct(ID);
            return RedirectToAction("Products");
        }

        /// <summary>
        /// Displays all roles from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Roles()
        {
            return View(new BusinessLayer.Roles().GetRoles());
        }

        /// <summary>
        /// Displays form to create new role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewRole()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            return View();
        }

        /// <summary>
        /// Saves new role to database
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewRole(CommonLayer.Role role, string Active)
        {
            role.Active = bool.Parse(Active);

            new BusinessLayer.Roles().AddRoleToDatabase(role);

            return RedirectToAction("Roles");
        }

        /// <summary>
        /// Returns a form to edit a specific role
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditRole(Guid ID)
        {
            CommonLayer.Role Role = new BusinessLayer.Roles().GetRole(ID);

            List<string> IsRoleActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsRoleActive.Select(boolean => new SelectListItem { Selected = (boolean == Role.Active.ToString()), Text = boolean, Value = boolean });

            return View(Role);
        }

        /// <summary>
        /// Displays form to edit new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRole(CommonLayer.Role role, string Active)
        {
            role.Active = bool.Parse(Active);

            new BusinessLayer.Roles().UpdateRole(role);

            return RedirectToAction("Roles");
        }

        /// <summary>
        /// Deletes a specific role from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteRole(Guid ID)
        {
            new BusinessLayer.Roles().DeleteRole(ID);
            return RedirectToAction("Roles");
        }

        /// <summary>
        /// Displays all the categories from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Categories()
        {
            return View(new BusinessLayer.Categories().GetCategoriesAsModel());
        }
        
        /// <summary>
        /// Returns form to create a new category
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewCategory()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            BusinessLayer.Categories c = new BusinessLayer.Categories();
            List<SelectListItem> ParentCategoryItems = (from category in c.GetParentCategoriesAsModel().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = category.Name,
                                                         Value = category.ID.ToString()
                                                     }).ToList();
            ViewBag.Subcategories = ParentCategoryItems;
            return View();
        }

        /// <summary>
        /// Saves newly created category to database
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewCategory(CommonLayer.Category category, string Active)
        {
            category.Active = bool.Parse(Active);

            new BusinessLayer.Categories().AddCategoryToDatabase(category);

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
            CommonLayer.Category Category = c.GetCategory(ID);

            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Selected = (boolean == Category.Active.ToString()), Text = boolean, Value = boolean });

            List<SelectListItem> ParentCategoryItems = (from category in c.GetParentCategoriesAsModel().ToList()
                                                        select new SelectListItem()
                                                        {
                                                            Text = category.Name,
                                                            Value = category.ID.ToString()
                                                        }).ToList();
            
            ParentCategoryItems.SingleOrDefault(p => p.Value.Equals(Category.ParentID.ToString())).Selected = true;
            ViewBag.Subcategories = ParentCategoryItems;
            return View(Category);
        }

        /// <summary>
        /// Saves edited category to database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCategory(CommonLayer.Category category, string Active)
        {
            category.Active = bool.Parse(Active);

            new BusinessLayer.Categories().UpdateCategory(category);

            return RedirectToAction("Categories");
        }

        /// <summary>
        /// Deletes a specigic category from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteCategory(string ID)
        {
            new BusinessLayer.Categories().DeleteCategory(ID);
            return RedirectToAction("Categories");
        }

        /// <summary>
        /// Displays all the countries from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Towns()
        {
            return View(new BusinessLayer.Towns().GetTownsAsModel());
        }

        /// <summary>
        /// Displays a form to create a new country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewTown()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            BusinessLayer.Towns t = new BusinessLayer.Towns();
            List<SelectListItem> TownLocationItems = (from countries in new BusinessLayer.Countries().GetCountries().ToList()
                                                     select new SelectListItem()
                                                     {
                                                         Text = countries.Name,
                                                         Value = countries.ID.ToString()
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
        public ActionResult CreateNewTown(CommonLayer.Town town, string Active)
        {
            town.Active = bool.Parse(Active);

            new BusinessLayer.Towns().AddTownToDatabase(town);

            return RedirectToAction("Towns");
        }

        /// <summary>
        /// Returns form to edit specific country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditTown(Guid ID)
        {
            CommonLayer.Town Town = new BusinessLayer.Towns().GetTown(ID);

            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Selected = (boolean == Town.Active.ToString()), Text = boolean, Value = boolean });

            return View(Town);
        }

        /// <summary>
        /// Saves edited country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditTown(CommonLayer.Town town, string Active)
        {
            town.Active = bool.Parse(Active);

            new BusinessLayer.Towns().UpdateTown(town);

            return RedirectToAction("Towns");
        }

        /// <summary>
        /// Deletes specific country from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteTown(Guid ID)
        {
            new BusinessLayer.Towns().DeleteTown(ID);
            return RedirectToAction("Towns");
        }

        /// <summary>
        /// Displays all the countries from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Countries()
        {
            return View(new BusinessLayer.Countries().GetCountries());
        }

        /// <summary>
        /// Displays a form to create a new country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewCountry()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            return View();
        }

        /// <summary>
        /// Saves new country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewCountry(CommonLayer.Country country, string Active)
        {
            country.Active = bool.Parse(Active);

            new BusinessLayer.Countries().AddCountryToDatabase(country);

            return RedirectToAction("Countries");
        }

        /// <summary>
        /// Returns form to edit specific country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCountry(Guid ID)
        {
            CommonLayer.Country Country = new BusinessLayer.Countries().GetCountry(ID);

            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Selected = (boolean == Country.Active.ToString()), Text = boolean, Value = boolean });

            return View(Country);
        }

        /// <summary>
        /// Saves edited country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCountry(CommonLayer.Country country, string Active)
        {
            country.Active = bool.Parse(Active);

            new BusinessLayer.Countries().UpdateCountry(country);

            return RedirectToAction("Countries");
        }

        /// <summary>
        /// Deletes specific country from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteCountry(Guid ID)
        {
            new BusinessLayer.Countries().DeleteCountry(ID);
            return RedirectToAction("Countries");
        }

        /// <summary>
        /// Displays all the countries from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult UserTypes()
        {
            return View(new BusinessLayer.UserTypes().GetUserTypes());
        }

        /// <summary>
        /// Displays a form to create a new country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewUserType()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            return View();
        }

        /// <summary>
        /// Saves new country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewUserType(CommonLayer.UserType UserType, string Active)
        {
            UserType.Active = bool.Parse(Active);

            new BusinessLayer.UserTypes().AddUserTypeToDatabase(UserType);

            return RedirectToAction("UserTypes");
        }

        /// <summary>
        /// Returns form to edit specific country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUserType(Guid ID)
        {
            CommonLayer.UserType UserType = new BusinessLayer.UserTypes().GetUserType(ID);

            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Selected = (boolean == UserType.Active.ToString()), Text = boolean, Value = boolean });

            return View(UserType);
        }

        /// <summary>
        /// Saves edited country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditUserType(CommonLayer.UserType UserType, string Active)
        {
            UserType.Active = bool.Parse(Active);

            new BusinessLayer.UserTypes().UpdateUserType(UserType);

            return RedirectToAction("UserTypes");
        }

        /// <summary>
        /// Deletes specific country from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteUserType(Guid ID)
        {
            new BusinessLayer.UserTypes().DeleteUserType(ID);
            return RedirectToAction("UserTypes");
        }

        /// <summary>
        /// Displays all the countries from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Menus()
        {
            return View(new BusinessLayer.Menus().GetMenus());
        }

        /// <summary>
        /// Displays a form to create a new country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewMenu()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            BusinessLayer.Menus m = new BusinessLayer.Menus();
            List<SelectListItem> ParentMenuItems = (from menu in m.GetParentMenusAsModel().ToList()
                                                        select new SelectListItem()
                                                        {
                                                            Text = menu.Name,
                                                            Value = menu.ID.ToString()
                                                        }).ToList();
            ViewBag.SubMenus = ParentMenuItems;

            return View();
        }

        /// <summary>
        /// Saves new country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewMenu(CommonLayer.Menu Menu, string Active)
        {
            Menu.Active = bool.Parse(Active);

            new BusinessLayer.Menus().AddMenuToDatabase(Menu);

            return RedirectToAction("Menus");
        }

        /// <summary>
        /// Returns form to edit specific country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditMenu(string ID)
        {
            CommonLayer.Menu Menu = new BusinessLayer.Menus().GetMenu(ID);

            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Selected = (boolean == Menu.Active.ToString()), Text = boolean, Value = boolean });

            return View(Menu);
        }

        /// <summary>
        /// Saves edited country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditMenu(CommonLayer.Menu Menu, string Active)
        {
            Menu.Active = bool.Parse(Active);

            new BusinessLayer.Menus().UpdateMenu(Menu);

            return RedirectToAction("Menus");
        }

        /// <summary>
        /// Deletes specific country from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteMenu(string ID)
        {
            new BusinessLayer.Menus().DeleteMenu(ID);
            return RedirectToAction("Menus");
        }

        /// <summary>
        /// Displays all the countries from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Reviews()
        {
            return View(new BusinessLayer.Reviews().GetReviews());
        }

        /// <summary>
        /// Returns form to edit specific country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditReview()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            return View();
        }

        /// <summary>
        /// Saves edited country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditReview(CommonLayer.Review Review, string Active)
        {
            Review.Active = bool.Parse(Active);

            new BusinessLayer.Reviews().UpdateReview(Review);

            return RedirectToAction("Reviews");
        }

        /// <summary>
        /// Deletes specific country from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteReview(Guid ID)
        {
            new BusinessLayer.Reviews().DeleteReview(ID);
            return RedirectToAction("Reviews");
        }

        /// <summary>
        /// Displays all the countries from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CarouselItems()
        {
            return View(new BusinessLayer.CarouselItems().GetCarouselItems());
        }

        /// <summary>
        /// Displays a form to create a new country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewCarouselItem()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            return View();
        }

        /// <summary>
        /// Saves new country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewCarouselItem(CommonLayer.CarouselItem CarouselItem, string Active)
        {
            CarouselItem.Active = bool.Parse(Active);

            new BusinessLayer.CarouselItems().AddCarouselItemToDatabase(CarouselItem);

            return RedirectToAction("CarouselItems");
        }

        /// <summary>
        /// Returns form to edit specific country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditCarouselItem()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            return View();
        }

        /// <summary>
        /// Saves edited country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCarouselItem(CommonLayer.CarouselItem CarouselItem, string Active)
        {
            CarouselItem.Active = bool.Parse(Active);

            new BusinessLayer.CarouselItems().UpdateCarouselItem(CarouselItem);

            return RedirectToAction("CarouselItems");
        }

        /// <summary>
        /// Deletes specific country from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteCarouselItem(Guid ID)
        {
            new BusinessLayer.CarouselItems().DeleteCarouselItem(ID);
            return RedirectToAction("CarouselItems");
        }

        /// <summary>
        /// Displays all the countries from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Sales()
        {
            return View(new BusinessLayer.Sales().GetSales());
        }

        /// <summary>
        /// Displays a form to create a new country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateNewSale()
        {
            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Text = boolean, Value = boolean });

            return View();
        }

        /// <summary>
        /// Saves new country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateNewSale(CommonLayer.Sale Sale, string Active)
        {
            Sale.Active = bool.Parse(Active);

            new BusinessLayer.Sales().AddSaleToDatabase(Sale);

            return RedirectToAction("Sales");
        }

        /// <summary>
        /// Returns form to edit specific country
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditSale(Guid ID)
        {
            CommonLayer.Sale Sale = new BusinessLayer.Sales().GetSale(ID);

            List<string> IsProductActive = new List<string>() { "True", "False" };
            ViewBag.Active = IsProductActive.Select(boolean => new SelectListItem { Selected = (boolean == Sale.Active.ToString()), Text = boolean, Value = boolean });

            return View(Sale);
        }

        /// <summary>
        /// Saves edited country to database
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditSale(CommonLayer.Sale Sale, string Active)
        {
            Sale.Active = bool.Parse(Active);

            new BusinessLayer.Sales().UpdateSale(Sale);

            return RedirectToAction("Sales");
        }

        /// <summary>
        /// Deletes specific country from the database
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteSale(Guid ID)
        {
            new BusinessLayer.Sales().DeleteSale(ID);
            return RedirectToAction("Sales");
        }

        /// <summary>
        /// Returns list of orders
        /// </summary>
        /// <returns></returns>
        public ActionResult Orders()
        {
            return View(new BusinessLayer.Orders().GetOrders());
        }

        /// <summary>
        /// Edits a specific order taking the OrderDetailsID as a paramater
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditOrder(Guid ID)
        {
            return View(new BusinessLayer.Orders().GetOrder(ID));
        }

        [HttpPost]
        public ActionResult EditOrder(CommonLayer.Order Order)
        {
            new BusinessLayer.Orders().UpdateOrder(Order);

            return RedirectToAction("Orders");
        }

        public ActionResult DeleteOrder(Guid ID)
        {
            new BusinessLayer.Orders().DeleteOrder(ID);

            return RedirectToAction("Orders", "Admin");
        }

        /// <summary>
        /// OrderID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult OrderDetails(Guid ID)
        {
            return View(new BusinessLayer.OrderDetails().GetOrderDetails(ID));
        }


        /// <summary>
        /// OrderDetailsID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditOrderDetails(Guid ID)
        {
            return View(new BusinessLayer.Orders().GetOrderDetail(ID));
        }

        [HttpPost]
        public ActionResult EditOrderDetails(CommonLayer.OrderDetail OrderDetail)
        {
            CommonLayer.Order Order = new BusinessLayer.Orders().GetOrder(OrderDetail.ID);

            new BusinessLayer.OrderDetails().UpdateOrderDetails(OrderDetail);

            return RedirectToAction("OrderDetails", "Admin", new { ID = OrderDetail.OrderID });
        }

        /// <summary>
        /// OrderDetailsID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteOrderDetails(Guid ID)
        {
            new BusinessLayer.OrderDetails().DeleteOrderDetails(ID);

            return RedirectToAction("OrderDetails");
        }

        /// <summary>
        /// Returns a list of audits
        /// </summary>
        /// <returns></returns>
        public ActionResult Audits()
        {
            return View(new BusinessLayer.Audits().GetAuditsAsModel());
        }
    }
}
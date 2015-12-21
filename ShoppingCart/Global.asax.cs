using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShoppingCart
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (Context.User != null)
            {
                if (Context.User.Identity.IsAuthenticated == true)
                {
                    BusinessLayer.Roles rl = new BusinessLayer.Roles();
                    List<CommonLayer.Role> roles = rl.GetUserRoles(Context.User.Identity.Name).ToList();
                    string[] rolesarray = new string[roles.Count];
                    int counter = 0;
                    foreach (CommonLayer.Role tmpRole in roles)
                    {
                        rolesarray[counter] = tmpRole.Code;
                        counter++;
                    }
                    System.Security.Principal.GenericPrincipal gp = new System.Security.Principal.GenericPrincipal(Context.User.Identity, rolesarray);
                    Context.User = gp;
                }
            }
        }
    }
}

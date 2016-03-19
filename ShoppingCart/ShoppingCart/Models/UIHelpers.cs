using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class UIHelpers
    {
        public static string UserFullName
        {
            get
            {
                if (HttpContext.Current.Session["FullName"] != null)
                    return HttpContext.Current.Session["FullName"].ToString();
                return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["FullName"] = value;
            }
        }
    }
}
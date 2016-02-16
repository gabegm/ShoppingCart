using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Orders
    {
        public CommonLayer.Order Order;
        public CommonLayer.OrderDetail OrderDetail;
        public CommonLayer.User User;
    }
}
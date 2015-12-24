using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer
{
    public class Orders : BLBase
    {
        public IQueryable<CommonLayer.Models.OrdersModel> GetOrdersAsModel()
        {
            return new DataLayer.DAOrders(this.Entities).GetOrdersAsModel();
        }

        public void AddOrderToDatabase(CommonLayer.Order Order)
        {
            new DataLayer.DAOrders(this.Entities).AddNewOrder(Order);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLayer.Models
{
    public class OrdersModel
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
        public string Status { get; set; }
        public Guid UserID { get; set; }
        public string UserEmail { get; set; }
        public Guid OrderDetailsID { get; set; }
        public Guid ProductID { get; set; }
        public int ProductQuantity { get; set; }
        public float ProductPrice { get; set; }
        public float ProductVATRate { get; set; }
    }
}
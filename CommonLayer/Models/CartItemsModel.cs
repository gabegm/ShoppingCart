using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLayer.Models
{
    public class CartItemsModel
    {
        public Guid ID { get; set; }
        public int Quantity { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public float ProductVATRate { get; set; }
        public string ProductImageURL { get; set; }
        public Guid UserID { get; set; }
    }
}
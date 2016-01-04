using System;

namespace CommonLayer.Models
{
    public class ProductsModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public float Price { get; set; }
        public float VATRate { get; set; }
        public int Quantity { get; set; }
        public Boolean Active { get; set; }
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Guid ReviewID { get; set; }
        public string ReviewDescription { get; set; }
        public int ReviewRating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CommonLayer.Models
{
    public class CategoriesModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ParentID { get; set; }
        public string ChildName { get; set; }
    }
}